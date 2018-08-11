using AutoMapper;
using ExpressShop.Storage.Entities;
using ExpressShop.Storage.Models;
using System;
using ExpressShop.Storage.Extensions;
using System.Linq;
using System.Data.Entity;
using System.Transactions;
using System.Threading;
using System.Collections.Generic;
using ExpressShop.Storage.Dto;

namespace ExpressShop.Storage.Services
{
    public class StorageService : IStorageService
    {
        #region Ctors

        public StorageService()
        {
            Db = new StorageDatabaseContextFactory().Create();
        }

        static StorageService()
        {
            Mapper = new MapperConfiguration(cfg => cfg.CreateMap<EntReservation, Reservation>())
                .CreateMapper();
        }

        #endregion

        #region Private properties

        private StorageDatabaseContext Db { get; }
        private static IMapper Mapper { get; }

        #endregion

        #region Mapping

        private Reservation Map(EntReservation src)
        {
            return Mapper.Map<EntReservation, Reservation>(src);
        }

        private IEnumerable<Reservation> Map(IEnumerable<EntReservation> src)
        {
            return Mapper.Map<IEnumerable<EntReservation>, IEnumerable<Reservation>>(src);
        }

        #endregion

        #region IStorageService implementation

        public Reservation GetById(EntityGetByIdReq req)
        {
            return req.IsNotNull()
                ? Map(Db.Reservations.FirstOrDefault(x => x.Id == req.Id))
                : null;
        }

        public IEnumerable<Reservation> GetAll()
        {
            return Map(Db.Reservations.ToList());
        }

        public void CreateReservation(Reservation src)
        {
            if (src.IsNull())
                throw new ArgumentNullException(nameof(src));
            if (src.TotalPrice.CheckDecimalValue().Not())
                throw new ArgumentException(nameof(src.TotalPrice));
            if (src.Quantity <= 0)
                throw new ArgumentException(nameof(src.Quantity));
            if (src.ProductId.IsEmpty())
                throw new ArgumentException(nameof(src.ProductId));

            var product = new ProductService()
                .GetById(new EntityGetByIdReq
                {
                    Id = src.ProductId
                });

            if (product.IsNull())
                throw new InvalidOperationException("Товар не найден.");

            var item = new EntReservation
            {
                Id = src.Id.IsNotEmpty()
                    ? src.Id
                    : Guid.NewGuid(),
                Characteristics = src.Characteristics,
                TotalPrice = src.Quantity * product.Price,
                Quantity = src.Quantity
            };

            Db.Reservations.Add(item);

            Db.SaveChanges();
        }

        public ReserveResultResp Reserve(ReserveReq req)
        {
            if (req.IsNull())
                throw new ArgumentNullException(nameof(req));
            if (req.ProductId.IsEmpty())
                throw new ArgumentException(nameof(req.ProductId));
            if (req.Quantity <= 0)
                throw new ArgumentException(nameof(req.Quantity));

            var reserveResult = new ReserveResultResp
            {
                Status = ReserveStatus.Fail
            };

            try
            {
                TransactionOptions transOpt = new TransactionOptions
                {
                    IsolationLevel = IsolationLevel.Serializable
                };

                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew, transOpt))
                {
                    using (var context = new StorageDatabaseContextFactory().Create())
                    {
                        string sqlQuery = $"SELECT * FROM [ExpressShop.Storage].[dbo].[EntProducts] WITH (UPDLOCK) WHERE Id = '{req.ProductId}'";

                        var product = context.Products
                            .SqlQuery(sqlQuery)
                            .FirstOrDefault();

                        Console.WriteLine($"{Thread.CurrentThread.Name}: Получил продукт. Остаток: {product.TotalQuantity}");

                        if (product.TotalQuantity == 0)
                        {
                            scope.Complete();

                            reserveResult.Message = $"Данного товара нет в наличии.";
                            reserveResult.Status = ReserveStatus.EmptyStorage;
                        }
                        else
                        {
                            product.TotalQuantity -= req.Quantity;

                            var reservation = new EntReservation
                            {
                                Id = Guid.NewGuid(),
                                Characteristics = product.Characteristics,
                                ProductId = product.Id,
                                Quantity = req.Quantity
                            };

                            context.Reservations.Add(reservation);

                            context.SaveChanges();

                            Console.WriteLine($"{Thread.CurrentThread.Name}: Создал бронь (кол-во товаров: {reservation.Quantity}).");
                            Console.WriteLine($"{Thread.CurrentThread.Name}: Сохранил продукт. Остаток: {product.TotalQuantity}");
                        }
                    }

                    scope.Complete();
                }

                reserveResult.Message = $"Бронирование прошло успешно.";
                reserveResult.Status = ReserveStatus.Success;
            }
            catch(Exception ex)
            {
                reserveResult.Message = $"Произошла ошибка при бронировании товара: {ex.Message}";
                reserveResult.Status = ReserveStatus.EmptyStorage;
            }

            return reserveResult;
        }

        #endregion
    }
}
