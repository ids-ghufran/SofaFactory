using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SofaFactory.Migrations
{
    public partial class parentIdOptionalInCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "ParentId",
                table: "Categories",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3f67700c-232a-4bc1-b3b5-f8aef4630b1e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4bf2d197-fb2f-4832-8ebf-10df12ec7f82", "AQAAAAIAAYagAAAAEMQ6zhUb5b4M3+A6vfYhe9yeGD5Jac+9QwuRog09Ucs3IV/++nMjYPWSfEA6GDULzA==", "0c6de552-49a3-4af9-b03c-c65fcf6ed38e" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "4f6fede0-7e17-4c44-af0b-ad0b9c1c98fa",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "cf2964da-4c50-4c4f-b24f-6a262eb9f721", "AQAAAAIAAYagAAAAEEsETxyB3PCgbjFAQAgc9wSpVbleEorG2aU3BCcn558TIcOnjcjXpULXtUrZsvowXQ==", "3a3af890-786e-456c-ab48-ca48d723ec5a" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "ParentId",
                table: "Categories",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

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
        }
    }
}
