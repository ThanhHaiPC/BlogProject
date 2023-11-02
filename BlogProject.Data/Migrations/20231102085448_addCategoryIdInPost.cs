using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlogProject.Data.Migrations
{
    public partial class addCategoryIdInPost : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CategoriesDetails");

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Posts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "CategoryPosts",
                columns: table => new
                {
                    CategoriesID = table.Column<int>(type: "int", nullable: false),
                    PostsPostID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryPosts", x => new { x.CategoriesID, x.PostsPostID });
                    table.ForeignKey(
                        name: "FK_CategoryPosts_Categories_CategoriesID",
                        column: x => x.CategoriesID,
                        principalTable: "Categories",
                        principalColumn: "CategoriesID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CategoryPosts_Posts_PostsPostID",
                        column: x => x.PostsPostID,
                        principalTable: "Posts",
                        principalColumn: "PostID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("cbcf8873-71a9-4fd2-b0d3-d16243a77ce8"),
                column: "ConcurrencyStamp",
                value: "0eaf044a-c740-48f2-8fc8-87fee3f43fb0");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("e208aeb8-558d-4796-bb3a-b010a6504c4f"),
                column: "ConcurrencyStamp",
                value: "3fea6b84-7e52-48d0-8fee-f620469392cf");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("c8c8ba75-93dc-4e6e-8dc2-aff296f3baea"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "b30f0247-3671-4ae2-bd54-d8c13c3999ac", "AQAAAAEAACcQAAAAEHVGohmD8FvNoYvafORaw0p5i16kJ5xZh13sm3KQ60M+KA8Db4jPKOxp2oYfLTmOpQ==" });

            migrationBuilder.CreateIndex(
                name: "IX_CategoryPosts_PostsPostID",
                table: "CategoryPosts",
                column: "PostsPostID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CategoryPosts");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Posts");

            migrationBuilder.CreateTable(
                name: "CategoriesDetails",
                columns: table => new
                {
                    CategoriesID = table.Column<int>(type: "int", nullable: false),
                    PostID = table.Column<int>(type: "int", nullable: false),
                    CategoriesDetailID = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoriesDetails", x => new { x.CategoriesID, x.PostID });
                    table.ForeignKey(
                        name: "FK_CategoriesDetails_Categories_CategoriesID",
                        column: x => x.CategoriesID,
                        principalTable: "Categories",
                        principalColumn: "CategoriesID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CategoriesDetails_Posts_PostID",
                        column: x => x.PostID,
                        principalTable: "Posts",
                        principalColumn: "PostID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("cbcf8873-71a9-4fd2-b0d3-d16243a77ce8"),
                column: "ConcurrencyStamp",
                value: "1c0688ce-ef92-42c7-a429-d9204705e490");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("e208aeb8-558d-4796-bb3a-b010a6504c4f"),
                column: "ConcurrencyStamp",
                value: "6505d803-5154-45e5-a19e-8d76a1ce79c6");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("c8c8ba75-93dc-4e6e-8dc2-aff296f3baea"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "35be0df0-ef38-44ec-abbc-9a7243dd492e", "AQAAAAEAACcQAAAAELSF+nCHW2+PqDHdO6YDEzAJQAEwmCZ/zM7PPer21sUwSf3ZI0KrRn97jQ7c6dz0iQ==" });

            migrationBuilder.CreateIndex(
                name: "IX_CategoriesDetails_PostID",
                table: "CategoriesDetails",
                column: "PostID");
        }
    }
}
