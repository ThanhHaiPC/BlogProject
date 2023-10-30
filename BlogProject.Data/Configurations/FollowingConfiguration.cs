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
    public class FollowingConfiguration : IEntityTypeConfiguration<Follow>
    {
        public void Configure(EntityTypeBuilder<Follow> builder)
        {
            builder.ToTable("Followings");

            builder.HasKey(x => new { x.FollowerId, x.FolloweeId });


            // RelationShip 1 -n
            builder.HasOne(x => x.Follower).WithMany(x => x.Followee).HasForeignKey(x => x.FollowerId);
        }
    }
}
