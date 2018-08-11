using System;

namespace ExpressShop.Storage.Entities
{
    /// <summary>
    /// Бронирование
    /// </summary>
    public class EntReservation : EntityBase
    {
        /// <summary>
        /// Идентификатор владельца брони
        /// </summary>
        public Guid OwnerId { get; set; }
        /// <summary>
        /// Характеристики забронированного товара
        /// </summary>
        public string Characteristics { get; set; }
        /// <summary>
        /// Количество товаров в бронировании
        /// </summary>
        public int Quantity { get; set; }
        /// <summary>
        /// Общая сумма заказа
        /// </summary>
        public decimal TotalPrice { get; set; }

        /// <summary>
        /// Идентификатор товара
        /// </summary>
        public Guid ProductId { get; set; }
        public virtual EntProduct Product { get; set; }
    }
}
