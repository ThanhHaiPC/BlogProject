using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlogProject.Data.Migrations
{
    public partial class addActiveVideo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Active",
                table: "Videos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("cbcf8873-71a9-4fd2-b0d3-d16243a77ce8"),
                column: "ConcurrencyStamp",
                value: "b467ac98-e2ab-4f04-9bdb-683735416618");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("e208aeb8-558d-4796-bb3a-b010a6504c4f"),
                column: "ConcurrencyStamp",
                value: "b7043360-56c1-4898-8efd-e949fc9a6edf");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("f76f9568-c479-4b92-958d-b0a8dbe8241e"),
                column: "ConcurrencyStamp",
                value: "eca5e24d-e7c5-4c11-99ea-f359350f7f76");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("c8c8ba75-93dc-4e6e-8dc2-aff296f3baea"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "5f8392bd-d4a0-48b3-84ef-1e7779339ec6", "AQAAAAEAACcQAAAAEIU4AS09BEyibnP9mUSzViwmLPv1TIpdR+FuWNWohK5DpQ8gn2MAIkD0389cxOWTwQ==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Active",
                table: "Videos");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("cbcf8873-71a9-4fd2-b0d3-d16243a77ce8"),
                column: "ConcurrencyStamp",
                value: "bbdafefe-9ed1-4e94-8521-3314b723b566");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("e208aeb8-558d-4796-bb3a-b010a6504c4f"),
                column: "ConcurrencyStamp",
                value: "e7ee504b-ea51-4355-b0f7-42585e3c0c5a");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("f76f9568-c479-4b92-958d-b0a8dbe8241e"),
                column: "ConcurrencyStamp",
                value: "2fd7f8de-94d8-4e1b-8cd6-39cf216bd229");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("c8c8ba75-93dc-4e6e-8dc2-aff296f3baea"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "b1b6142d-0ec7-49e2-9df3-f9ce4f8deff6", "AQAAAAEAACcQAAAAEI418EaTeCnX/sUTQK9R1DMzgrondLQxEWRgCDmo06B4KyHY9v56upcq/ViqCcD1nw==" });
        }
    }
}
