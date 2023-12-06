using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlogProject.Data.Migrations
{
    public partial class updatePassTokenConfiguration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UserId1",
                table: "Token",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("cbcf8873-71a9-4fd2-b0d3-d16243a77ce8"),
                column: "ConcurrencyStamp",
                value: "87a0391a-af7e-493f-b44c-1082c4821b41");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("e208aeb8-558d-4796-bb3a-b010a6504c4f"),
                column: "ConcurrencyStamp",
                value: "61a1386d-0986-40ab-8e28-dd916aa976d2");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("f76f9568-c479-4b92-958d-b0a8dbe8241e"),
                column: "ConcurrencyStamp",
                value: "43e56821-3516-44cb-823e-e06fd8f9e5cb");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("c8c8ba75-93dc-4e6e-8dc2-aff296f3baea"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "a633ca37-8519-45f9-aa80-3e5619212fd7", "AQAAAAEAACcQAAAAEB/hSHdcESrZbxIeEPGupRtLJsEHOgX6dLGhvnjk2yhb67dRS4fizTB1tScikuet6Q==" });

            migrationBuilder.CreateIndex(
                name: "IX_Token_UserId1",
                table: "Token",
                column: "UserId1",
                unique: true,
                filter: "[UserId1] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Token_AspNetUsers_UserId1",
                table: "Token",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Token_AspNetUsers_UserId1",
                table: "Token");

            migrationBuilder.DropIndex(
                name: "IX_Token_UserId1",
                table: "Token");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "Token");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("cbcf8873-71a9-4fd2-b0d3-d16243a77ce8"),
                column: "ConcurrencyStamp",
                value: "b47d7a68-48bb-48c6-acca-6f5182ddf9e9");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("e208aeb8-558d-4796-bb3a-b010a6504c4f"),
                column: "ConcurrencyStamp",
                value: "a78f561f-f7bf-4e25-9b48-0fc273ee4505");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("f76f9568-c479-4b92-958d-b0a8dbe8241e"),
                column: "ConcurrencyStamp",
                value: "895d3447-c963-4fb8-8310-42a4d8294eb4");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("c8c8ba75-93dc-4e6e-8dc2-aff296f3baea"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "228e5647-49f3-46a4-b791-4fe9a00b4965", "AQAAAAEAACcQAAAAEJVhTOryI488Li+3awPbu5Qr0dsx7vdOtqJtMvQrBr/yb/NVfPfPAGxpXq4QXMB0AA==" });
        }
    }
}
