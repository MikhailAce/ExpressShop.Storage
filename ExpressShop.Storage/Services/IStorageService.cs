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
        Reservation GetById(EntityGetByIdReq req);

        IEnumerable<Reservation> GetAll();

        void CreateReservation(Reservation src);

        ReserveResultResp Reserve(ReserveReq req);
    }
}
