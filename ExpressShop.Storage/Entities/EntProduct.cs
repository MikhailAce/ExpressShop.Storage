using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ExpressShop.Storage.Entities
{
    public class EntProduct : EntityBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Characteristics { get; set; }
        public decimal Price { get; set; }
        public int TotalQuantity { get; set; }

        public virtual ICollection<EntReservation> Reservations { get; set; }
    }
}
