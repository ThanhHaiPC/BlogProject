using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlogProject.Data.Migrations
{
    public partial class UpdateVideos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "OrderNo",
                table: "Videos",
                newName: "CategoriesID");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "Videos",
                newName: "UpDate");

            migrationBuilder.AddColumn<int>(
                name: "VideoID",
                table: "Likes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "VideoID",
                table: "Comments",
                type: "int",
                nullable: false,
                defaultValue: 0);

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

            migrationBuilder.CreateIndex(
                name: "IX_Videos_CategoriesID",
                table: "Videos",
                column: "CategoriesID");

            migrationBuilder.CreateIndex(
                name: "IX_Likes_VideoID",
                table: "Likes",
                column: "VideoID");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_VideoID",
                table: "Comments",
                column: "VideoID");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_Comments_Videos_VideoID",
            //    table: "Comments",
            //    column: "VideoID",
            //    principalTable: "Videos",
            //    principalColumn: "VideoID",
            //    onDelete: ReferentialAction.NoAction);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_Likes_Videos_VideoID",
            //    table: "Likes",
            //    column: "VideoID",
            //    principalTable: "Videos",
            //    principalColumn: "VideoID",
            //    onDelete: ReferentialAction.NoAction);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_Videos_Categories_CategoriesID",
            //    table: "Videos",
            //    column: "CategoriesID",
            //    principalTable: "Categories",
            //    principalColumn: "CategoriesID",
            //    onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Videos_VideoID",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Likes_Videos_VideoID",
                table: "Likes");

            migrationBuilder.DropForeignKey(
                name: "FK_Videos_Categories_CategoriesID",
                table: "Videos");

            migrationBuilder.DropIndex(
                name: "IX_Videos_CategoriesID",
                table: "Videos");

            migrationBuilder.DropIndex(
                name: "IX_Likes_VideoID",
                table: "Likes");

            migrationBuilder.DropIndex(
                name: "IX_Comments_VideoID",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "VideoID",
                table: "Likes");

            migrationBuilder.DropColumn(
                name: "VideoID",
                table: "Comments");

            migrationBuilder.RenameColumn(
                name: "UpDate",
                table: "Videos",
                newName: "Date");

            migrationBuilder.RenameColumn(
                name: "CategoriesID",
                table: "Videos",
                newName: "OrderNo");

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
        }
    }
}
