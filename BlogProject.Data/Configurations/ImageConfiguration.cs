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
    public class ImageConfiguration : IEntityTypeConfiguration<Images>
    {
        public void Configure(EntityTypeBuilder<Images> builder)
        {
            builder.ToTable("Images");


            builder.HasKey(x => x.ImageID);
            builder.Property(x => x.ImageID).UseIdentityColumn();

            // Relationship
            builder.HasOne(x => x.Post).WithMany(x => x.Images).HasForeignKey(x => x.PostID);
        }
    }
}
