using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopShop.Domain.Entities;

namespace TopShop.DataAccess.Configurations
{
    public  class OrderItemConfiguration : EntityConfiguration<OrderItem>
    {
        protected override void ConfigureEntity(EntityTypeBuilder<OrderItem> builder)
        {
            builder.Property(x => x.Quantity).IsRequired();
            builder.Property(x => x.UnitPrice).IsRequired();
            builder.Property(x => x.ProductName).IsRequired();

            builder.HasOne(x => x.Product)
                .WithMany(x => x.OrderItems)
                .HasForeignKey(x => x.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x=>x.Order)
                .WithMany(x=>x.OrderItems)
                .HasForeignKey(x=>x.OrderId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
