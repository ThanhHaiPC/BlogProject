using BlogProject.Data.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.Data.Configurations
{
    public class RatingConfiguration : IEntityTypeConfiguration<Rating>
    {
        public void Configure(EntityTypeBuilder<Rating> builder)
        {
            builder.ToTable("RatingPost");

            builder.HasKey(x => x.RatingID);
            builder.Property(x => x.RatingID).UseIdentityColumn();
            builder.Property(x => x.RatingValue).IsRequired();

        }
    }
}
