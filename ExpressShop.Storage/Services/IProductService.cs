using ExpressShop.Storage.Dto;
using ExpressShop.Storage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressShop.Storage.Services
{
    public interface IProductService
    {
        /// <summary>
        /// Получить товар по идентификатору
        /// </summary>
        Product GetById(EntityGetByIdReq req);

        /// <summary>
        /// Получить все товары
        /// </summary>
        IEnumerable<Product> GetAll();

        /// <summary>
        /// Удалить все товары из базы данных
        /// </summary>
        void DeleteAll();

        /// <summary>
        /// Создать новый товар
        /// </summary>
        void Create(Product src);
    }
}
