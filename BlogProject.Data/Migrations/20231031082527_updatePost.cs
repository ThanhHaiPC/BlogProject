using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlogProject.Data.Migrations
{
    public partial class updatePost : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AuthorID",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "ImageID",
                table: "Posts");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("cbcf8873-71a9-4fd2-b0d3-d16243a77ce8"),
                column: "ConcurrencyStamp",
                value: "df28e2c3-27d9-4d76-b5db-d8d8b03763a0");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("e208aeb8-558d-4796-bb3a-b010a6504c4f"),
                column: "ConcurrencyStamp",
                value: "325ba1fe-8711-4939-9d5f-f0ab6a67441e");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("c8c8ba75-93dc-4e6e-8dc2-aff296f3baea"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "b1e49ed9-3e5b-494e-83d4-8ff2d2ed2a1c", "AQAAAAEAACcQAAAAEIPFxYAVTiE7oNE2qZWZKztov/dJM7ET2tZCpdlFA0BDy+JG6Cqay9ulDnKFeP1Nrw==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AuthorID",
                table: "Posts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ImageID",
                table: "Posts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("cbcf8873-71a9-4fd2-b0d3-d16243a77ce8"),
                column: "ConcurrencyStamp",
                value: null);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("e208aeb8-558d-4796-bb3a-b010a6504c4f"),
                column: "ConcurrencyStamp",
                value: null);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("c8c8ba75-93dc-4e6e-8dc2-aff296f3baea"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "92bdf249-2144-42b9-a2cb-bd03d0a2ed92", "AQAAAAIAAYagAAAAEIZTHdZCjAK/+rAKsE9Zb5HZMdFTZwlvAoUVppzzgxeH34Gy0Ej/LTtQu9RFuSFvRw==" });
        }
    }
}
