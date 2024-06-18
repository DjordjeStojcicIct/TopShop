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
    public class ReviewConfiguration : EntityConfiguration<Review>
    {
        protected override void ConfigureEntity(EntityTypeBuilder<Review> builder)
        {
            builder.Property(x => x.Rating).IsRequired();
            builder.Property(x => x.Comment).IsRequired();

            builder.HasOne(x => x.User)
                .WithMany(x => x.Reviews)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
