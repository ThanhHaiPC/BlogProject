using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlogProject.Data.Migrations
{
    public partial class updateUserCreatedAt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("cbcf8873-71a9-4fd2-b0d3-d16243a77ce8"),
                column: "ConcurrencyStamp",
                value: "c27374f8-b24b-4153-94cf-00723c2abb7a");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("e208aeb8-558d-4796-bb3a-b010a6504c4f"),
                column: "ConcurrencyStamp",
                value: "70e52edc-155d-456d-bb53-df211e0f646f");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("f76f9568-c479-4b92-958d-b0a8dbe8241e"),
                column: "ConcurrencyStamp",
                value: "cb53a263-f664-4fe7-b60e-f3b392efa2f2");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("c8c8ba75-93dc-4e6e-8dc2-aff296f3baea"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "a0d28fbf-c980-4b05-9e38-a8a05744f399", "AQAAAAEAACcQAAAAEMbGnjwRUjrjd9Nd8OPF7IgWWFTIIVYyN1A+ydFDN/Ik6uWxeF4KbzaygTe4cjD0Vg==" });

            
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            

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
    }
}
