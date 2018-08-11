using ExpressShop.Storage.Dto;
using ExpressShop.Storage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressShop.Storage.Services
{
    public interface IProductService
    {
        Product GetById(EntityGetByIdReq req);

        IEnumerable<Product> GetAll();

        void DeleteAll();

        void Create(Product src);
    }
}
