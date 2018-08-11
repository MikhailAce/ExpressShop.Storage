using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressShop.Storage.Dto
{
    public class ReserveReq
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
