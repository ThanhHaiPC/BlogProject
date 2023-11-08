using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlogProject.Data.Migrations
{
    public partial class UpdateReply : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("cbcf8873-71a9-4fd2-b0d3-d16243a77ce8"),
                column: "ConcurrencyStamp",
                value: "be91e20b-af52-4956-b0c9-d0efccd6651c");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("e208aeb8-558d-4796-bb3a-b010a6504c4f"),
                column: "ConcurrencyStamp",
                value: "ed3af328-5107-4afa-b62e-b5babfe35e1e");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("c8c8ba75-93dc-4e6e-8dc2-aff296f3baea"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "9bc68e6d-a0b9-4388-ba3a-1dfe191f9642", "AQAAAAEAACcQAAAAEJctxnR6A0WplpdcTtqZXxVG9Jc2YZsO+eI8vWU3TiOo0PkAzkNlHrKg0wqigR4dTw==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("cbcf8873-71a9-4fd2-b0d3-d16243a77ce8"),
                column: "ConcurrencyStamp",
                value: "b4e66282-6678-4b1c-ba5a-ff128a557444");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("e208aeb8-558d-4796-bb3a-b010a6504c4f"),
                column: "ConcurrencyStamp",
                value: "d77b05f7-d31e-48bd-b101-60bef34e434d");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("c8c8ba75-93dc-4e6e-8dc2-aff296f3baea"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "4ce9ea59-df90-43d9-8498-edc30358dff1", "AQAAAAEAACcQAAAAEBR9a+ouWyCq+Mz4h5BvMHvxD8MtnbBgXwG7Hop5Qrj27LojfXEviTtyj3MsY6NCRQ==" });
        }
    }
}
