using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopShop.DataAccess.Configurations;
using TopShop.Domain.Entities;

namespace TopShop.DataAccess
{
    public class TopShopContext : DbContext
    {
        public TopShopContext() { }

        public TopShopContext(DbContextOptions opt) : base(opt) 
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AddressConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new OrderConfiguration());
            modelBuilder.ApplyConfiguration(new OrderItemConfiguration());
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new ReviewConfiguration());
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new FileConfiguration());

            modelBuilder.Entity<Role>().HasData(
                new Role { Id = 1, Name = "Admin", IsDefault = false },
                new Role { Id = 2, Name = "User", IsDefault = true }
                );
            modelBuilder.Entity<RoleUseCase>().HasData(InitialData.getRoleUseCases());
            modelBuilder.Entity<Address>().HasData(InitialData.GetAddresses());
            modelBuilder.Entity<User>().HasData(InitialData.getUsers());
            modelBuilder.Entity<Category>().HasData(InitialData.getCategories());
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=DESKTOP-EJJNGE1\\MSSQL2022;Initial Catalog=TopShop1;Integrated Security=True;TrustServerCertificate=True");
        }

        public DbSet<Address> Addresses { get; set; }
        public DbSet<AuditLog> AuditLogs { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<RoleUseCase> RoleUseCases { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Wishlist> Wishlists { get; set; }
        public DbSet<FileT> Files { get; set; }

    }
}
