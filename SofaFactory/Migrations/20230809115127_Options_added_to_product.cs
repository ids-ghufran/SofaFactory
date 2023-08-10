using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SofaFactory.Migrations
{
    public partial class Options_added_to_product : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SeatingCapacity",
                table: "Products",
                newName: "StorageTypeId");

            migrationBuilder.AddColumn<int>(
                name: "MaterialId",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SeatingCapacityId",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SizeId",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Materials",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Label = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Materials", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SeatingCapacities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Label = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SeatingCapacities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Size",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Label = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Size", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StorageType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Label = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StorageType", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3f67700c-232a-4bc1-b3b5-f8aef4630b1e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "47e7e76e-5e6c-4715-8851-98199cc7565b", "AQAAAAEAACcQAAAAEFR1EyzhNeHhl0uLrqcCqcNg8recxZI9Z7IIAR5vgpXNz699II7OK6SJzu6u/IjRQA==", "e9e074f2-b4b0-4f92-9a20-a2a1db399007" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "4f6fede0-7e17-4c44-af0b-ad0b9c1c98fa",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ce7da24d-cf06-4fcb-b76a-5a0ce89453e8", "AQAAAAEAACcQAAAAEP59yon4DAuTy93B0qpovCgAAqsoO6nEdbYKXCSD3K4zUfdm9faoYM09ma1MQr2xmQ==", "b4d88da6-445f-44ee-9214-c52f4b21a736" });

            migrationBuilder.CreateIndex(
                name: "IX_Products_MaterialId",
                table: "Products",
                column: "MaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_SeatingCapacityId",
                table: "Products",
                column: "SeatingCapacityId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_SizeId",
                table: "Products",
                column: "SizeId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_StorageTypeId",
                table: "Products",
                column: "StorageTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Materials_MaterialId",
                table: "Products",
                column: "MaterialId",
                principalTable: "Materials",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_SeatingCapacities_SeatingCapacityId",
                table: "Products",
                column: "SeatingCapacityId",
                principalTable: "SeatingCapacities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Size_SizeId",
                table: "Products",
                column: "SizeId",
                principalTable: "Size",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_StorageType_StorageTypeId",
                table: "Products",
                column: "StorageTypeId",
                principalTable: "StorageType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Materials_MaterialId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_SeatingCapacities_SeatingCapacityId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Size_SizeId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_StorageType_StorageTypeId",
                table: "Products");

            migrationBuilder.DropTable(
                name: "Materials");

            migrationBuilder.DropTable(
                name: "SeatingCapacities");

            migrationBuilder.DropTable(
                name: "Size");

            migrationBuilder.DropTable(
                name: "StorageType");

            migrationBuilder.DropIndex(
                name: "IX_Products_MaterialId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_SeatingCapacityId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_SizeId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_StorageTypeId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "MaterialId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "SeatingCapacityId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "SizeId",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "StorageTypeId",
                table: "Products",
                newName: "SeatingCapacity");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3f67700c-232a-4bc1-b3b5-f8aef4630b1e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "75fa4ecf-2950-46d4-afaf-aa69f9a08224", "AQAAAAIAAYagAAAAEKZWe48enuHJVgAD6a3R8Z4MMz1xZy0w8MRC6EcXqD+ehGWOQD1oZgcvXmoeqRpTDA==", "c57f3b41-2337-458d-bfbe-bc48618113d5" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "4f6fede0-7e17-4c44-af0b-ad0b9c1c98fa",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c8c78b1d-a5ff-4e58-ba93-7fc74274b91f", "AQAAAAIAAYagAAAAEExKzYx7kfsu0pdUv/zNytfy9zRxQkE+XXmDstLFQcU+7cZ5PDlRLUS7qqlCPaVn+g==", "cd78a9de-ca20-4e98-8fed-c640de3da363" });
        }
    }
}
