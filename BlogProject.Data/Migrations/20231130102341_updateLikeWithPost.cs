using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlogProject.Data.Migrations
{
    public partial class updateLikeWithPost : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "View",
                table: "Tag",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("cbcf8873-71a9-4fd2-b0d3-d16243a77ce8"),
                column: "ConcurrencyStamp",
                value: "09d02f72-6775-4873-8f11-1befde025d94");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("e208aeb8-558d-4796-bb3a-b010a6504c4f"),
                column: "ConcurrencyStamp",
                value: "8a23e094-f1fa-428d-8012-ea4e64e31fe6");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("f76f9568-c479-4b92-958d-b0a8dbe8241e"),
                column: "ConcurrencyStamp",
                value: "d64a3f67-df02-4cf4-a569-fcaaea32a5f6");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("c8c8ba75-93dc-4e6e-8dc2-aff296f3baea"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "f8efd35a-08f1-47c5-8ad6-c7578aff55d9", "AQAAAAEAACcQAAAAEAbdPdyr4LbaHc9nFz96fh1PDkif+keTOVSsRCa/yNEPKx9FmmT2hWkJ7e9FAaQeIg==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "View",
                table: "Tag");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("cbcf8873-71a9-4fd2-b0d3-d16243a77ce8"),
                column: "ConcurrencyStamp",
                value: "fe56ad1b-9bf9-478e-b8da-af04bf2ef89e");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("e208aeb8-558d-4796-bb3a-b010a6504c4f"),
                column: "ConcurrencyStamp",
                value: "fccb041d-6f51-47f4-bdbf-cb432b2cf3fe");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("f76f9568-c479-4b92-958d-b0a8dbe8241e"),
                column: "ConcurrencyStamp",
                value: "41373863-21f0-4c81-af5f-663b5d991a14");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("c8c8ba75-93dc-4e6e-8dc2-aff296f3baea"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "0517b808-3889-4c56-888a-6502428e9848", "AQAAAAEAACcQAAAAEKiHfiXLGPYAXYHlLwfZ5NmVqZwSeU1FrCewe70woTNOQRx6bHEOn3BbCR1uXw8XLg==" });
        }
    }
}
