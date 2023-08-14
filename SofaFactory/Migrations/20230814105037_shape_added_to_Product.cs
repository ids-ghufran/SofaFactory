using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SofaFactory.Migrations
{
    public partial class shape_added_to_Product : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Brands_BrandId",
                table: "Products");

            migrationBuilder.AlterColumn<int>(
                name: "BrandId",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ShapeId",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Shapes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Label = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shapes", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3f67700c-232a-4bc1-b3b5-f8aef4630b1e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "685aa32e-55ff-450a-984c-e05adcedea04", "AQAAAAEAACcQAAAAEGBG1QPi0Wn0wXhBr3TMcvesHUGwnz/Jxh2c4LltsfbVPsAaQfFWBPLiYj8kF4dzSg==", "251911ee-bd01-49a6-9e1c-d7545682e4db" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "4f6fede0-7e17-4c44-af0b-ad0b9c1c98fa",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ee97c16e-e70b-439e-8c38-45762aee651d", "AQAAAAEAACcQAAAAEBeA+ZfXY1BENddFLTpoAT9YScC5ES1xJHPWiAijWzaEzuFvjREWUX7Hgj087r1Xaw==", "798093ad-0288-42c7-b981-48ccc11d04c0" });

            migrationBuilder.CreateIndex(
                name: "IX_Products_ShapeId",
                table: "Products",
                column: "ShapeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Brands_BrandId",
                table: "Products",
                column: "BrandId",
                principalTable: "Brands",
                principalColumn: "BrandId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Shapes_ShapeId",
                table: "Products",
                column: "ShapeId",
                principalTable: "Shapes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Brands_BrandId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Shapes_ShapeId",
                table: "Products");

            migrationBuilder.DropTable(
                name: "Shapes");

            migrationBuilder.DropIndex(
                name: "IX_Products_ShapeId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ShapeId",
                table: "Products");

            migrationBuilder.AlterColumn<int>(
                name: "BrandId",
                table: "Products",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3f67700c-232a-4bc1-b3b5-f8aef4630b1e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "328bd444-8e90-4a90-b5bf-4b9e2b9f68d7", "AQAAAAIAAYagAAAAEEgbcQMP0OH4I3RzLFM746oKkPOHFPLoPsSM4vc1Zpzc465IpVLLXA5M6Zz1DsvCqw==", "39d8efb2-7676-42f7-8261-5d37d9e05bc3" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "4f6fede0-7e17-4c44-af0b-ad0b9c1c98fa",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "98ccbdb0-3853-4127-b248-417abaad9800", "AQAAAAIAAYagAAAAEFOVVVIsC59hJrO6DJ4ddkNZbUUYdWgHg+VU+QKecJ3rKic7nBRdS7d+rYRhk7rRAw==", "b2f791c4-388e-4194-a81b-1560367ba4b4" });

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Brands_BrandId",
                table: "Products",
                column: "BrandId",
                principalTable: "Brands",
                principalColumn: "BrandId");
        }
    }
}
