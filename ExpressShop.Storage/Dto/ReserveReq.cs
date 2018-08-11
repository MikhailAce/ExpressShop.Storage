using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressShop.Storage.Dto
{
    public class ReserveReq
    {
        /// <summary>
        /// Идентификатор товара
        /// </summary>
        public Guid ProductId { get; set; }
        /// <summary>
        /// Количество товара для бронирования
        /// </summary>
        public int Quantity { get; set; }
    }
}
