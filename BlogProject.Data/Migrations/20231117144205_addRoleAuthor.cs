using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlogProject.Data.Migrations
{
    public partial class addRoleAuthor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("cbcf8873-71a9-4fd2-b0d3-d16243a77ce8"),
                column: "ConcurrencyStamp",
                value: "4acc181e-5447-40f7-84db-c0d5edabd83e");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("e208aeb8-558d-4796-bb3a-b010a6504c4f"),
                column: "ConcurrencyStamp",
                value: "1a35f033-3b8d-407b-99bd-61a7c9443aea");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Description", "Name", "NormalizedName" },
                values: new object[] { new Guid("d6364d4d-f2de-4585-9672-7d2b41ac7a2d"), "ce183304-86f8-4dcf-9a7d-0c3cf81028ee", "Author Role", "author", "author" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("c8c8ba75-93dc-4e6e-8dc2-aff296f3baea"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "e70184c7-cecd-401b-8668-893bbd540bb1", "AQAAAAEAACcQAAAAEM8g7amBOfoOzqxvL/TeNTVuB1nV80i13CFOllXyJ79PItz7VIIPVZapwqWwiZFuFQ==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("d6364d4d-f2de-4585-9672-7d2b41ac7a2d"));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("cbcf8873-71a9-4fd2-b0d3-d16243a77ce8"),
                column: "ConcurrencyStamp",
                value: "651c6ab8-63ab-470c-8e52-ce66ae0380c2");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("e208aeb8-558d-4796-bb3a-b010a6504c4f"),
                column: "ConcurrencyStamp",
                value: "9b33ad8f-be76-4d22-ad26-ca9956739450");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("c8c8ba75-93dc-4e6e-8dc2-aff296f3baea"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "22bbe8d0-7dc3-4613-8b8b-682605bdf753", "AQAAAAEAACcQAAAAEKTG4oajafxKVg04mXDWLlwoEKLoOFambH14CbTg8kJEh4pb9Mv9Ji7UskwEMe+4Ow==" });
        }
    }
}
