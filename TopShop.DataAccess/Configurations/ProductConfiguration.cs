using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopShop.Domain.Entities;
using System.Security.Cryptography.X509Certificates;

namespace TopShop.DataAccess.Configurations
{
    public class ProductConfiguration : EntityConfiguration<Product>
    {
        protected override void ConfigureEntity(EntityTypeBuilder<Product> builder)
        {
            builder.HasIndex(x => x.Name);
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.Description).IsRequired();
            builder.Property(x => x.Price).IsRequired();
            builder.Property(x => x.StockQuantity).IsRequired();

            builder.HasOne(x => x.ProductImage)
                .WithMany()
                .HasForeignKey(x => x.ProductImageId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.Reviews)
                .WithOne(x => x.Product)
                .HasForeignKey(x => x.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.Wishlists)
                .WithOne(x => x.Product)
                .HasForeignKey(x => x.ProductId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
