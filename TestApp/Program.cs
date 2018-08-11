using ExpressShop.Storage;
using ExpressShop.Storage.Dto;
using ExpressShop.Storage.Models;
using ExpressShop.Storage.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var shopStorageTest = new ShopStorageTest();
            shopStorageTest.RunReserveTest();
        }

        public class ShopStorageTest
        {
            private IProductService ProductService { get; set; }
            private IStorageService StorageService { get; set; }

            private Guid ProductId { get; } = Guid.NewGuid();

            /// <summary>
            /// Количество потоков в тесте
            /// </summary>
            private const int ThreadCount = 10;
            /// <summary>
            /// Начальный остаток товара
            /// </summary>
            private const int ProductTotalQuantity = 10000;
            /// <summary>
            /// Число итераций для каждого потока
            /// </summary>
            private const int IterationCount = 1000;

            /// <summary>
            /// Выполнение необходимых операций перед запуском теста
            /// </summary>
            public void BeforeTest()
            {
                // Создание сервисов
                ProductService = new ProductService();
                StorageService = new StorageService();

                // Чистка таблиц в базе данных
                StorageService.DeleteAll();
                ProductService.DeleteAll();

                // Чистка файла логов
                Logger.ClearLogs();
            }

            /// <summary>
            /// Запуск теста на бронирование
            /// </summary>
            public void RunReserveTest()
            {
                // Выполнение необходимых предварительных операций
                BeforeTest();

                // Создание продукта
                CreateProduct();

                // Создание и запуск потоков
                for (int i = 0; i < ThreadCount; i++)
                {
                    Thread thread = new Thread(ThreadProcess);
                    thread.Name = "Польователь " + i.ToString();
                    thread.Start();
                }

                //Console.ReadLine();
            }

            #region Вспомогательные методы для теста

            /// <summary>
            /// Создать тетовый товар
            /// </summary>
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

            /// <summary>
            /// Процесс работы потока
            /// </summary>
            private void ThreadProcess()
            {
                for (int i = 0; i < IterationCount; i++)
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
}
