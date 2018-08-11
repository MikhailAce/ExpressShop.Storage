using System;

namespace ExpressShop.Storage.Entities
{
    public class EntReservation : EntityBase
    {
        public Guid OwnerId { get; set; }
        public string Characteristics { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }

        public Guid ProductId { get; set; }
        public virtual EntProduct Product { get; set; }
    }
}
