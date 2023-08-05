using System;
using Microsoft.EntityFrameworkCore;
using FoodDelivery.Core.Domain;

namespace FoodDelivery.Core.Adaptors.Db
{
	public class FoodDeliveryDbContext : DbContext
	{
		public FoodDeliveryDbContext()
		{
		}

		public FoodDeliveryDbContext(DbContextOptions<FoodDeliveryDbContext> options) : base(options)
		{
		}

		public DbSet<DeliveryDriver> DeliveryDrivers { get; set; }
		public DbSet<FoodOrder> FoodOrders { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
			if (!optionsBuilder.IsConfigured)
				optionsBuilder.UseSqlServer(
					@"Server=(localdb)\mssqllocaldb;Database=EFProviders.InMemory;Trusted_Connection=True;ConnectRetryCount=0");

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
			modelBuilder.Entity<FoodOrder>()
				.Property(field => field.Id)
				.ValueGeneratedOnAdd();
			modelBuilder.Entity<FoodOrder>()
				.HasKey(field => field.Id);
			modelBuilder.Entity<DeliveryDriver>()
				.Property(field => field.Id)
				.ValueGeneratedOnAdd();
			modelBuilder.Entity<DeliveryDriver>()
				.HasKey(field => field.Id);

        }

    }
}

