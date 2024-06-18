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
    public class AddressConfiguration : EntityConfiguration<Address>
    {
        protected override void ConfigureEntity(EntityTypeBuilder<Address> builder)
        {
            builder.Property(x => x.Street).IsRequired();
            builder.Property(x => x.City).IsRequired();
            builder.Property(x => x.PostalCode).IsRequired();
            builder.Property(x => x.Country).IsRequired();
        }

    }
}
