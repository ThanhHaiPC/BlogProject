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
    public class LikeConfiguration : IEntityTypeConfiguration<Like>
    {
        public void Configure(EntityTypeBuilder<Like> builder)
        {
            builder.ToTable("Likes");

            builder.HasKey(x => x.LikeID);
            builder.Property(x => x.LikeID).UseIdentityColumn();
            builder.Property(x => x.PostID).HasDefaultValue(0);

            builder.HasOne(x => x.User).WithMany(x => x.Like).HasForeignKey(x => x.UserId);

            builder.HasOne(x => x.Post).WithMany(x => x.Likes).HasForeignKey(x=>x.PostID);
        }
    }
}
