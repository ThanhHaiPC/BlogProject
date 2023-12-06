using BlogProject.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.Data.Configurations
{
    public class PasswordResetTokenConfiguration : IEntityTypeConfiguration<PasswordResetToken>
    {
        public void Configure(EntityTypeBuilder<PasswordResetToken> builder)
        {
            builder.ToTable("Token");
            builder.HasKey(x=> x.Id);


            // Cấu hình các thuộc tính
            builder.Property(t => t.UserId)
                .IsRequired();

            builder.Property(t => t.Token)
                .IsRequired();

            builder.Property(t => t.CreationTime)
                .IsRequired();

            builder.Property(t => t.ExpireTime)
                .IsRequired();

            // Thiết lập mối quan hệ một-đến-một với bảng ApplicationUser
            builder.HasOne(t => t.User)
                .WithMany() // Tùy thuộc vào cấu trúc của bạn, có thể là WithOne() nếu có quan hệ một-đến-một rõ ràng
                .HasForeignKey(t => t.UserId)
                .IsRequired();


        }
    }
}
