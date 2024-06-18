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
    public class UserConfiguration : EntityConfiguration<User>
    {
        protected override void ConfigureEntity(EntityTypeBuilder<User> builder)
        {
            builder.Property(x => x.Username)
                .IsRequired().HasMaxLength(30);

            builder.HasIndex(x => x.Username).IsUnique();
            builder.Property(x => x.FirstName)
                .IsRequired().HasMaxLength(30);
            builder.Property(x => x.LastName)
                .IsRequired().HasMaxLength(30);
            builder.Property(x => x.Email).IsRequired();
            builder.Property(x=>x.Password).IsRequired();

            builder.HasOne(x=>x.ProfileImage)
                .WithMany()
                .HasForeignKey(x=>x.ProfileImageId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Address)
                .WithMany()
                .HasForeignKey(x => x.AddressId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.Wishlists)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.AuditLogs)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
