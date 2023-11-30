using BlogProject.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.Data.EF
{
    public class SeedData
    {

        private readonly ModelBuilder modelBuilder;

        public SeedData(ModelBuilder modelBuilder)
        {
            this.modelBuilder = modelBuilder;
        }
        public void Seed()
        {
            //      AppConfig
            modelBuilder.Entity<AppConfig>().HasData(
               new AppConfig() { Key = "HomeTitle", Value = "Đây là trang chủ của Web_Blog" },
               new AppConfig() { Key = "HomeKeyWord", Value = "Đây là từ khóa của Web_Blog" },
               new AppConfig() { Key = "HomeDescription", Value = "Đây là mô tả của Web_Blog" }
               );
            //      Category
            modelBuilder.Entity<Category>().HasData(
              new Category()
              {
                  CategoriesID = 1,
                  Name = "BÓNG ĐÁ",
              },
              new Category()
              {
                  CategoriesID = 2,
                  Name = "THẾ GIỚI",
              },
              new Category()
              {
                  CategoriesID = 3,
                  Name = "XÃ HỘI",
              },
              new Category()
              {
                  CategoriesID = 4,
                  Name = "VĂN HÓA",
              },
              new Category()
              {
                  CategoriesID = 5,
                  Name = "KINH TẾ",
              },
              new Category()
              {
                  CategoriesID = 6,
                  Name = "GIÁO DỤC",
              },
              new Category()
              {
                  CategoriesID = 7,
                  Name = "THỂ THAO",
              },
              new Category()
              {
                  CategoriesID = 8,
                  Name = "GIẢI TRÍ",
              },
              new Category()
              {
                  CategoriesID = 9,
                  Name = "PHÁP LUẬT",
              },
              new Category()
              {
                  CategoriesID = 10,
                  Name = "CÔNG NGHỆ",
              },
              new Category()
              {
                  CategoriesID = 11,
                  Name = "KHOA HỌC",
              },
              new Category()
              {
                  CategoriesID = 12,
                  Name = "ĐỜI SỐNG ",
              },
              new Category()
              {
                  CategoriesID = 13,
                  Name = "XE CỘ",
              },
              new Category()
              {
                  CategoriesID = 14,
                  Name = "NHÀ ĐẤT",
              });
            //      ADMINISTRATOR
            var roleId = new Guid("E208AEB8-558D-4796-BB3A-B010A6504C4F");
            var roleId1 = new Guid("CBCF8873-71A9-4FD2-B0D3-D16243A77CE8");
            var roleId2 = new Guid("F76F9568-C479-4B92-958D-B0A8DBE8241E");
            var adminId = new Guid("C8C8BA75-93DC-4E6E-8DC2-AFF296F3BAEA");

			
			modelBuilder.Entity<Role>().HasData(
                new Role
                {
                    Id = roleId,
                    Name = "admin",
                    NormalizedName = "admin",
                    Description = "Administrator Role"
                },
                new Role
                {
                    Id = roleId1,
                    Name = "user",
                    NormalizedName = "user",
                    Description = "User Role"
                },
				 new Role
				 {
					 Id = roleId2,
					 Name = "author",
					 NormalizedName = "author",
					 Description = "Author Role"
				 });


			var hasher = new PasswordHasher<User>();
            modelBuilder.Entity<User>().HasData(new User
            {
                Id = adminId,
                UserName = "admin",
                FirstName ="Hai",
                LastName = "Pham",
                NormalizedUserName = "admin",
                Email = "abcd@gmail.com",
                NormalizedEmail = "abcd@gmail.com",
                EmailConfirmed = true,
                PasswordHash = hasher.HashPassword(null, "Aa@123"),
                SecurityStamp = string.Empty,
                Image = "",
                DateOfBir = new DateTime(2002, 03, 18),
                Address = "Biên Hòa Đồng Nai"
            });

            modelBuilder.Entity<IdentityUserRole<Guid>>().HasData(new IdentityUserRole<Guid>
            {
                RoleId = roleId,
                UserId = adminId
            });
        }
    }
}
