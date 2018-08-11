namespace ExpressShop.Storage.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<StorageDatabaseContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ExpressShop.Storage.StorageDatabaseContext context)
        {

        }
    }
}
