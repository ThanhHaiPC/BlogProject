using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlogProject.Data.Migrations
{
    public partial class dataseeing : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AppConfigs",
                columns: new[] { "Key", "Value" },
                values: new object[,]
                {
                    { "HomeDescription", "Đây là mô tả của Web_Blog" },
                    { "HomeKeyWord", "Đây là từ khóa của Web_Blog" },
                    { "HomeTitle", "Đây là trang chủ của Web_Blog" }
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Description", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("cbcf8873-71a9-4fd2-b0d3-d16243a77ce8"), null, "User Role", "user", "user" },
                    { new Guid("e208aeb8-558d-4796-bb3a-b010a6504c4f"), null, "Administrator Role", "admin", "admin" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Address", "ConcurrencyStamp", "DateOfBir", "Description", "Email", "EmailConfirmed", "FirstName", "Image", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("c8c8ba75-93dc-4e6e-8dc2-aff296f3baea"), 0, "Biên Hòa Đồng Nai", "92bdf249-2144-42b9-a2cb-bd03d0a2ed92", new DateTime(2002, 3, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "abcd@gmail.com", true, "Hai", "", "Pham", false, null, "abcd@gmail.com", "admin", "AQAAAAIAAYagAAAAEIZTHdZCjAK/+rAKsE9Zb5HZMdFTZwlvAoUVppzzgxeH34Gy0Ej/LTtQu9RFuSFvRw==", null, false, "", false, "admin" });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoriesID", "Name" },
                values: new object[,]
                {
                    { 1, "BÓNG ĐÁ" },
                    { 2, "THẾ GIỚI" },
                    { 3, "XÃ HỘI" },
                    { 4, "VĂN HÓA" },
                    { 5, "KINH TẾ" },
                    { 6, "GIÁO DỤC" },
                    { 7, "THỂ THAO" },
                    { 8, "GIẢI TRÍ" },
                    { 9, "PHÁP LUẬT" },
                    { 10, "CÔNG NGHỆ" },
                    { 11, "KHOA HỌC" },
                    { 12, "ĐỜI SỐNG " },
                    { 13, "XE CỘ" },
                    { 14, "NHÀ ĐẤT" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { new Guid("e208aeb8-558d-4796-bb3a-b010a6504c4f"), new Guid("c8c8ba75-93dc-4e6e-8dc2-aff296f3baea") });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AppConfigs",
                keyColumn: "Key",
                keyValue: "HomeDescription");

            migrationBuilder.DeleteData(
                table: "AppConfigs",
                keyColumn: "Key",
                keyValue: "HomeKeyWord");

            migrationBuilder.DeleteData(
                table: "AppConfigs",
                keyColumn: "Key",
                keyValue: "HomeTitle");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("cbcf8873-71a9-4fd2-b0d3-d16243a77ce8"));

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("e208aeb8-558d-4796-bb3a-b010a6504c4f"), new Guid("c8c8ba75-93dc-4e6e-8dc2-aff296f3baea") });

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoriesID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoriesID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoriesID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoriesID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoriesID",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoriesID",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoriesID",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoriesID",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoriesID",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoriesID",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoriesID",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoriesID",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoriesID",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoriesID",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("e208aeb8-558d-4796-bb3a-b010a6504c4f"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("c8c8ba75-93dc-4e6e-8dc2-aff296f3baea"));
        }
    }
}
