using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopShop.Domain.Entities;

namespace TopShop.DataAccess.Configurations
{
    public class OrderConfiguration : EntityConfiguration<Order>
    {
        protected override void ConfigureEntity(EntityTypeBuilder<Order> builder)
        {
            builder.Property(x => x.TotalAmount).IsRequired();

            builder.HasOne(x=>x.User)
                .WithMany(x=>x.Orders)
                .HasForeignKey(x=>x.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.ShippingAddress)
                .WithMany()
                .HasForeignKey(x=>x.ShippingAddressId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
