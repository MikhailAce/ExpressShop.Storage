using AutoMapper;
using ExpressShop.Storage.Dto;
using ExpressShop.Storage.Entities;
using ExpressShop.Storage.Extensions;
using ExpressShop.Storage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;

namespace ExpressShop.Storage.Services
{
    public class ProductService : IProductService
    {
        #region Ctors

        public ProductService()
        {
            Db = new StorageDatabaseContextFactory().Create();
        }

        static ProductService()
        {
            Mapper = new MapperConfiguration(cfg => cfg.CreateMap<EntProduct, Product>())
                .CreateMapper();
        }

        #endregion

        #region Private properties

        private StorageDatabaseContext Db { get; }
        private static IMapper Mapper { get; }

        #endregion

        #region Mapping

        private Product Map(EntProduct src)
        {
            return Mapper.Map<EntProduct, Product>(src);
        }

        private IEnumerable<Product> Map(IEnumerable<EntProduct> src)
        {
            return Mapper.Map<IEnumerable<EntProduct>, IEnumerable<Product>>(src);
        }

        #endregion

        #region IProductService implementation

        public Product GetById(EntityGetByIdReq req)
        {
            return req.IsNotNull()
                ? Map(Db.Products.FirstOrDefault(x => x.Id == req.Id))
                : null;
        }

        public IEnumerable<Product> GetAll()
        {
            return Map(Db.Products.ToList());
        }

        public void DeleteAll()
        {
            Db.Products.RemoveRange(Db.Products.ToList());
            Db.SaveChanges();
        }

        public void Create(Product src)
        {
            if (src.IsNull())
                throw new ArgumentNullException(nameof(src));
            if (src.Name.IsEmpty())
                throw new ArgumentException(nameof(src.Name));
            if (src.Price.CheckDecimalValue().Not())
                throw new ArgumentException(nameof(src.Price));
            if (src.TotalQuantity <= 0)
                throw new ArgumentException(nameof(src.TotalQuantity));

            var item = new EntProduct
            {
                Id = src.Id.IsNotEmpty()
                    ? src.Id
                    : Guid.NewGuid(),
                Name = src.Name,
                Description = src.Description,
                Characteristics = src.Characteristics,
                Price = src.Price,
                TotalQuantity = src.TotalQuantity
            };

            Db.Products.Add(item);
            Db.SaveChanges();
        }

        #endregion
    }
}
