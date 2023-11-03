using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlogProject.Data.Migrations
{
    public partial class UpdateAndDeletePosts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           

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
                value: "a2f1f37c-a82b-4aca-8c39-ffea9c7bcd1b");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("e208aeb8-558d-4796-bb3a-b010a6504c4f"),
                column: "ConcurrencyStamp",
                value: "3aae5716-48a5-48fd-941d-5fad8bef20b9");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("c8c8ba75-93dc-4e6e-8dc2-aff296f3baea"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "8b1ff63f-8e26-409a-b15a-7d7c85f95c69", "AQAAAAEAACcQAAAAELUCHX5FRS11wnRT7hevOR7QmNMuu6d9zLpnvT7ZN+RZFW07uy8OG8ZlQj0D9GBnwQ==" });

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
                value: "f7457a0d-6bbf-42a5-8fc7-c741235d5b8e");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("e208aeb8-558d-4796-bb3a-b010a6504c4f"),
                column: "ConcurrencyStamp",
                value: "b1817654-f99b-4d2a-a436-ac7bdef9db77");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("c8c8ba75-93dc-4e6e-8dc2-aff296f3baea"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "e3a0f614-21b9-4281-9089-d90737f37a79", "AQAAAAEAACcQAAAAEKKBMIIa4p6Pgk0TcxnfIY4H3Iys+JQPlUPo76EwQ9MZOusaDxROgGJLftJwb4zE3g==" });

            migrationBuilder.CreateIndex(
                name: "IX_CategoriesDetails_PostID",
                table: "CategoriesDetails",
                column: "PostID");
        }
    }
}
