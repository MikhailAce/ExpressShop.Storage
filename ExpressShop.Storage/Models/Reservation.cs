using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressShop.Storage.Models
{
    public class Reservation : EntityBase
    {
        public string Characteristics { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }

        public Guid ProductId { get; set; }
    }
}
