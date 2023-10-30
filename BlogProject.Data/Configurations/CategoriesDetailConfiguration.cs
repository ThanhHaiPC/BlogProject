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
    public class CategoriesDetailConfiguration : IEntityTypeConfiguration<CategoriesDetail>
    {
        public void Configure(EntityTypeBuilder<CategoriesDetail> builder)
        {
            builder.ToTable("CategoriesDetails");

            builder.HasKey(x => new { x.CategoriesID, x.PostID });
            builder.Property(x => x.Description).IsRequired(false).HasMaxLength(250);


            // RelationShip 1 -n
            builder.HasOne(x => x.Category).WithMany(x => x.CategoriesDetail).HasForeignKey(x => x.CategoriesID);
            builder.HasOne(x => x.Post).WithMany(x => x.CategoriesDetail).HasForeignKey(x => x.PostID);

        }
    }
}
