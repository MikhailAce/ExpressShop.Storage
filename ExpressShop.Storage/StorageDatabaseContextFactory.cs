using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressShop.Storage
{
    public class StorageDatabaseContextFactory
        : IDbContextFactory<StorageDatabaseContext>
    {
        public StorageDatabaseContext Create()
        {
            return new StorageDatabaseContext(LocalConfiguration.Instance.ConnectionString);
        }
    }
}