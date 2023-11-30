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
                value: "e59c4881-829e-4ad8-9147-cdae6dcde0ab");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("e208aeb8-558d-4796-bb3a-b010a6504c4f"),
                column: "ConcurrencyStamp",
                value: "e54a6075-8ce7-4811-ad40-049805571dfc");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Description", "Name", "NormalizedName" },
                values: new object[] { new Guid("f76f9568-c479-4b92-958d-b0a8dbe8241e"), "24442a9e-5579-475c-81b5-fbf27941662b", "Author Role", "author", "author" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("c8c8ba75-93dc-4e6e-8dc2-aff296f3baea"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "0c43ce9e-894f-40db-b5ac-df6ba3bb6276", "AQAAAAEAACcQAAAAEIfQmIiVRDpnxqhKe1AKEFC5Skjnh7eZfzkyFZIsL+jkrG1jOLc0VDUSE0eYq9kyAw==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("f76f9568-c479-4b92-958d-b0a8dbe8241e"));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("cbcf8873-71a9-4fd2-b0d3-d16243a77ce8"),
                column: "ConcurrencyStamp",
                value: "3899ccdf-64c0-48f8-aae4-bbe631bb7ec5");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("e208aeb8-558d-4796-bb3a-b010a6504c4f"),
                column: "ConcurrencyStamp",
                value: "083bd11d-b1ee-4e49-86e0-d1ebc0c41a0f");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("c8c8ba75-93dc-4e6e-8dc2-aff296f3baea"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "4c8ea487-d7a7-499a-9c8f-0a0b7d5681f6", "AQAAAAEAACcQAAAAEN3wFT/h6NnjI6jSB//Huhopkq9cN8lh76ijVg4KahHBql/9b3/L3aEp0wVtC0yVhA==" });
        }
    }
}
