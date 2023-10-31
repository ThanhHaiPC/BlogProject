using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlogProject.Data.Migrations
{
    public partial class dropImage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Posts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("cbcf8873-71a9-4fd2-b0d3-d16243a77ce8"),
                column: "ConcurrencyStamp",
                value: "882b7c9b-6280-489a-a091-4d891a3fdb90");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("e208aeb8-558d-4796-bb3a-b010a6504c4f"),
                column: "ConcurrencyStamp",
                value: "a6463423-5e66-4627-8703-dfc3f9d2e989");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("c8c8ba75-93dc-4e6e-8dc2-aff296f3baea"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "8a577016-82be-4503-bd0b-7b8be50da7e5", "AQAAAAEAACcQAAAAEEIwzZ04jVmGzUTC+vG0x+mjgrNylsVK92R+QOqFElC53ncLLCQi4utx2lG5ApuQ6g==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Posts");

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    ImageID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PostID = table.Column<int>(type: "int", nullable: false),
                    Active = table.Column<int>(type: "int", nullable: false),
                    Des = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.ImageID);
                    table.ForeignKey(
                        name: "FK_Images_Posts_PostID",
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
                value: "df28e2c3-27d9-4d76-b5db-d8d8b03763a0");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("e208aeb8-558d-4796-bb3a-b010a6504c4f"),
                column: "ConcurrencyStamp",
                value: "325ba1fe-8711-4939-9d5f-f0ab6a67441e");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("c8c8ba75-93dc-4e6e-8dc2-aff296f3baea"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "b1e49ed9-3e5b-494e-83d4-8ff2d2ed2a1c", "AQAAAAEAACcQAAAAEIPFxYAVTiE7oNE2qZWZKztov/dJM7ET2tZCpdlFA0BDy+JG6Cqay9ulDnKFeP1Nrw==" });

            migrationBuilder.CreateIndex(
                name: "IX_Images_PostID",
                table: "Images",
                column: "PostID");
        }
    }
}
