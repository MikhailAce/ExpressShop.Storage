using ExpressShop.Storage.Models;

namespace ExpressShop.Storage.Dto
{
    public class ReserveResultResp
    {
        public string Message { get; set; }

        public ReserveStatus Status { get; set; }
    }
}
