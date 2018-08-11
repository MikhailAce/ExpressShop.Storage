using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ExpressShop.Storage.Entities
{
    /// <summary>
    /// Товар
    /// </summary>
    public class EntProduct : EntityBase
    {
        /// <summary>
        /// Наименование товара
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Описание товара
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Характеристики товара
        /// </summary>
        public string Characteristics { get; set; }
        /// <summary>
        /// Цена за единицу товара
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// Количество товара на складе
        /// </summary>
        public int TotalQuantity { get; set; }

        public virtual ICollection<EntReservation> Reservations { get; set; }
    }
}
