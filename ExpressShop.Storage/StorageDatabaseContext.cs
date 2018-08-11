using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpressShop.Storage.Entities;

namespace ExpressShop.Storage
{
    public class StorageDatabaseContext : DbContext
    {
        private const int ShortStringLength = 100;
        private const int BasicStringLength = 500;
        private const int MediumStringLength = 1000;
        private const int LongStringLength = 2000;

        public StorageDatabaseContext(string connectionString) 
            : base(connectionString)
        {
            //Database.SetInitializer(new MigrateDatabaseToLatestVersion<StorageDatabaseContext, Migrations.Configuration>());
            Configuration.LazyLoadingEnabled = false;
        }

        public DbSet<EntReservation> Reservations { get; set; }
        public DbSet<EntProduct> Products { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Настройка, чтобы для свойств модели c типом DateTime использовался sql тип datetime2(3)
            modelBuilder.Properties<DateTime>()
                .Configure(p => p.HasColumnType("datetime2").HasPrecision(3));

            // Настройка, чтобы для свойств модели c типом DateTimeOffset использовался sql тип datetimeoffset(3)
            modelBuilder.Properties<DateTimeOffset>()
                .Configure(p => p.HasColumnType("datetimeoffset").HasPrecision(3));

            #region Reservation

            var reservationEntity = modelBuilder.Entity<EntReservation>();

            reservationEntity.HasKey(x => x.Id);
            reservationEntity.Property(x => x.Characteristics).HasMaxLength(LongStringLength);
            reservationEntity.Property(x => x.OwnerId).IsRequired();

            reservationEntity.HasRequired(x => x.Product)
                .WithMany(x => x.Reservations)
                .HasForeignKey(x => x.ProductId)
                .WillCascadeOnDelete(false);

            #endregion

            #region Product

            var productEntity = modelBuilder.Entity<EntProduct>();

            productEntity.HasKey(x => x.Id);
            productEntity.Property(x => x.Name).HasMaxLength(BasicStringLength);
            productEntity.Property(x => x.Description).HasMaxLength(MediumStringLength);
            productEntity.Property(x => x.Characteristics).HasMaxLength(LongStringLength);

            #endregion

            base.OnModelCreating(modelBuilder);
        }
    }
}
