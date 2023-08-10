using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SofaFactory.Migrations
{
    public partial class options : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Size_SizeId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_StorageType_StorageTypeId",
                table: "Products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StorageType",
                table: "StorageType");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Size",
                table: "Size");

            migrationBuilder.RenameTable(
                name: "StorageType",
                newName: "StorageTypes");

            migrationBuilder.RenameTable(
                name: "Size",
                newName: "Sizes");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StorageTypes",
                table: "StorageTypes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Sizes",
                table: "Sizes",
                column: "Id");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Sizes_SizeId",
                table: "Products",
                column: "SizeId",
                principalTable: "Sizes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_StorageTypes_StorageTypeId",
                table: "Products",
                column: "StorageTypeId",
                principalTable: "StorageTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Sizes_SizeId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_StorageTypes_StorageTypeId",
                table: "Products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StorageTypes",
                table: "StorageTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Sizes",
                table: "Sizes");

            migrationBuilder.RenameTable(
                name: "StorageTypes",
                newName: "StorageType");

            migrationBuilder.RenameTable(
                name: "Sizes",
                newName: "Size");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StorageType",
                table: "StorageType",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Size",
                table: "Size",
                column: "Id");

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
    }
}
