using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlogProject.Data.Migrations
{
    public partial class UpdateDatabaseTag1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tag_Posts_PostID",
                table: "Tag");

            migrationBuilder.DropIndex(
                name: "IX_Tag_PostID",
                table: "Tag");

            migrationBuilder.CreateTable(
                name: "PostsTag",
                columns: table => new
                {
                    PostID = table.Column<int>(type: "int", nullable: false),
                    TagID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostsTag", x => new { x.PostID, x.TagID });
                    table.ForeignKey(
                        name: "FK_PostsTag_Posts_PostID",
                        column: x => x.PostID,
                        principalTable: "Posts",
                        principalColumn: "PostID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PostsTag_Tag_TagID",
                        column: x => x.TagID,
                        principalTable: "Tag",
                        principalColumn: "TagID",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_PostsTag_TagID",
                table: "PostsTag",
                column: "TagID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PostsTag");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("cbcf8873-71a9-4fd2-b0d3-d16243a77ce8"),
                column: "ConcurrencyStamp",
                value: "63baf2a2-d83d-4559-bace-2a203c00726a");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("e208aeb8-558d-4796-bb3a-b010a6504c4f"),
                column: "ConcurrencyStamp",
                value: "f894f68f-665a-4f36-a872-3efef1257f6e");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("c8c8ba75-93dc-4e6e-8dc2-aff296f3baea"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "05c011d7-ddd0-4e49-982f-85d6c163cf50", "AQAAAAEAACcQAAAAEOSv0XZ/NZizigPBKO0oBoWiyOLL/6Ld/5j3jrHnahGu+QKQ+LTd8SRYcw8vvqcs2g==" });

            migrationBuilder.CreateIndex(
                name: "IX_Tag_PostID",
                table: "Tag",
                column: "PostID");

            migrationBuilder.AddForeignKey(
                name: "FK_Tag_Posts_PostID",
                table: "Tag",
                column: "PostID",
                principalTable: "Posts",
                principalColumn: "PostID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
