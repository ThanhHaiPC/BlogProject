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
    public class TagConfiguration : IEntityTypeConfiguration<Tag>
    {
        public void Configure(EntityTypeBuilder<Tag> builder)
        {
            builder.ToTable("Tag");

            builder.HasKey(x => x.TagID);
            builder.Property(x => x.TagID).UseIdentityColumn();

            builder.Property(x => x.TagName).IsRequired();
            builder.Property(x => x.View).HasDefaultValue(0);

            // Relationship
            builder.HasOne(x => x.Post).WithMany(x => x.Tag).HasForeignKey(x => x.PostID);
        }
    }
}
