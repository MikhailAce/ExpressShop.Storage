using ExpressShop.Storage.Dto;
using ExpressShop.Storage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressShop.Storage.Services
{
    public interface IStorageService
    {
        /// <summary>
        /// Получить бронирование по идентификатору
        /// </summary>
        Reservation GetById(EntityGetByIdReq req);

        /// <summary>
        /// Получить все бронирования
        /// </summary>
        IEnumerable<Reservation> GetAll();

        /// <summary>
        /// Удалить все бронирования из базы данных
        /// </summary>
        void DeleteAll();

        /// <summary>
        /// Создать бронирование
        /// </summary>
        void CreateReservation(Reservation src);

        /// <summary>
        /// Операция бронирования товара
        /// </summary>
        void Reserve(ReserveReq req);
    }
}
