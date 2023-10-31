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
    public class PostConfiguration : IEntityTypeConfiguration<Posts>
    {
        public void Configure(EntityTypeBuilder<Posts> builder)
        {
            builder.ToTable("Posts");

            builder.HasKey(x => x.PostID);
            builder.Property(x => x.PostID).UseIdentityColumn();
            builder.Property(x => x.Title).IsRequired().HasMaxLength(250);
            builder.Property(x => x.Desprition).IsRequired().HasMaxLength(500);
            builder.Property(x => x.View).HasDefaultValue(0);
            builder.Property(x => x.Content).IsRequired().HasMaxLength(5000);

        }
    }
}
