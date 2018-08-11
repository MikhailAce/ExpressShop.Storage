using ExpressShop.Storage.Dto;
using ExpressShop.Storage.Models;
using ExpressShop.Storage.Services;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ExpressShop.Storage.Test
{
    [TestFixture]
    public class ShopStorageTests
    {
        private IProductService ProductService { get; set; }
        private IStorageService StorageService { get; set; }

        private Guid ProductId { get; } = Guid.NewGuid();

        private const int ThreadCount = 10;
        private const int ProductTotalQuantity = 100;

        [SetUp]
        public void BeforeApiTest()
        {
            ProductService = new ProductService();
            StorageService = new StorageService();

            StorageService.DeleteAll();
            ProductService.DeleteAll();
        }

        [TearDown]
        public void AfterTest()
        {
            //ProductService.DeleteAll();
            //StorageService.DeleteAll();
        }

        [Test]
        public void ReserveTest()
        {
            CreateProduct();

            for (int i = 0; i < 1; i++)
            {
                Thread thread = new Thread(ThreadProcess);
                thread.Name = "Польователь " + i.ToString();
                thread.Start();
            }

            //Console.ReadLine();
        }

        #region Вспомогательные методы для теста

        private void CreateProduct()
        {
            ProductService.Create(new Product
            {
                Id = ProductId,
                Name = "Товар",
                Description = "Описание товара",
                Characteristics = "Характеристики товара",
                Price = 10m,
                TotalQuantity = ProductTotalQuantity
            });
        }

        private void ThreadProcess()
        {
            for (int i = 0; i < 5; i++)
            {
                StorageService.Reserve(new ReserveReq
                {
                    ProductId = ProductId,
                    Quantity = new Random().Next(1, 4)
                });
            }
        }

        #endregion
    }
}
