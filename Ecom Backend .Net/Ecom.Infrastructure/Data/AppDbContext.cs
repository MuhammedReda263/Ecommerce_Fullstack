using Ecom.Core.Entities.Product;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Photo> Photos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            // --- Categories ---
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Technology", Description = "All tech gadgets and devices" },
                new Category { Id = 2, Name = "Fashion", Description = "Trendy clothes and accessories" },
                new Category { Id = 3, Name = "Books & Stationery", Description = "Novels, study materials, and office items" }
            );

            // --- Products ---
            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = 1,
                    Name = "Laptop Pro X14",
                    Description = "Powerful laptop with 16GB RAM and 512GB SSD",
                    NewPrice = 1499.99m,
                    OldPrice = 1699.99m,
                    CategoryId = 1
                },
                new Product
                {
                    Id = 2,
                    Name = "Bluetooth Speaker Mini",
                    Description = "Portable speaker with deep bass and 10h battery life",
                    NewPrice = 79.99m,
                    OldPrice = 99.99m,
                    CategoryId = 1
                },
                new Product
                {
                    Id = 3,
                    Name = "Denim Jacket",
                    Description = "Unisex denim jacket, perfect for winter style",
                    NewPrice = 59.99m,
                    OldPrice = 79.99m,
                    CategoryId = 2
                },
                new Product
                {
                    Id = 4,
                    Name = "Office Notebook Set",
                    Description = "Pack of 3 ruled notebooks with hardcover design",
                    NewPrice = 24.99m,
                    OldPrice = 29.99m,
                    CategoryId = 3
                }
            );

            // Optional: add precision to avoid the decimal warning
            modelBuilder.Entity<Product>()
                .Property(p => p.NewPrice)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Product>()
                .Property(p => p.OldPrice)
                .HasPrecision(18, 2);
        }
    }
}
