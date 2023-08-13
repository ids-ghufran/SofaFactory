using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SofaFactory.Migrations
{
    public partial class slider_model : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Slider",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BtnLink = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Slider", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Slider_Images_ImageId",
                        column: x => x.ImageId,
                        principalTable: "Images",
                        principalColumn: "ImageId",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_Slider_ImageId",
                table: "Slider",
                column: "ImageId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Slider");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3f67700c-232a-4bc1-b3b5-f8aef4630b1e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "815b5894-96cb-4420-8478-adcd2b52c90b", "AQAAAAEAACcQAAAAEBjH87v0GItKCsTtHLqNngFusb4MpfqJyrYkuPwBEtsupw4lQJz8OlTmSBNkoFQIeQ==", "690cd35e-73f6-4056-8c2f-2c2437a3b2c5" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "4f6fede0-7e17-4c44-af0b-ad0b9c1c98fa",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6a4cef5d-a959-428c-abd4-dc4515fab8ff", "AQAAAAEAACcQAAAAEByGrjZDFb0Cs21+AmvT9+otiwLd4KdkF5H6mZfIj7hkXjPPbNCKRunmXEr6PPdvQg==", "3b2d4aa0-1fef-4565-9415-8f97c9ab0eb7" });
        }
    }
}
