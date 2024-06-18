using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopShop.Domain.Entities;

namespace TopShop.DataAccess.Configurations
{
    public class FileConfiguration : EntityConfiguration<FileT>
    {
        protected override void ConfigureEntity(EntityTypeBuilder<FileT> builder)
        {
            builder.Property(x => x.Path)
                .IsRequired().HasMaxLength(250);
        }
    }
}
