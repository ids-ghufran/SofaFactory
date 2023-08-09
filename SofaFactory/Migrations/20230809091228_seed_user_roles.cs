using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SofaFactory.Migrations
{
    public partial class seed_user_roles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "8883fbd9-4420-4444-84ca-ca31e8221cc7", "ca31e8221cc7", "User", "USER" },
                    { "d6c200c6-ff1f-4bd8-a1a3-92dc234d254b", "92dc234d254b", "Admin", "ADMIN" },
                    { "df129349-c335-46e1-86e6-22a179d808d3", "22a179d808d3", "SubAdmin", "SUBADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "3f67700c-232a-4bc1-b3b5-f8aef4630b1e", 0, "75fa4ecf-2950-46d4-afaf-aa69f9a08224", "AppUser", null, false, false, null, null, "SUBADMIN", "AQAAAAIAAYagAAAAEKZWe48enuHJVgAD6a3R8Z4MMz1xZy0w8MRC6EcXqD+ehGWOQD1oZgcvXmoeqRpTDA==", null, false, "c57f3b41-2337-458d-bfbe-bc48618113d5", false, "subadmin" },
                    { "4f6fede0-7e17-4c44-af0b-ad0b9c1c98fa", 0, "c8c78b1d-a5ff-4e58-ba93-7fc74274b91f", "AppUser", null, false, false, null, null, "ADMIN", "AQAAAAIAAYagAAAAEExKzYx7kfsu0pdUv/zNytfy9zRxQkE+XXmDstLFQcU+7cZ5PDlRLUS7qqlCPaVn+g==", null, false, "cd78a9de-ca20-4e98-8fed-c640de3da363", false, "admin" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "df129349-c335-46e1-86e6-22a179d808d3", "3f67700c-232a-4bc1-b3b5-f8aef4630b1e" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "d6c200c6-ff1f-4bd8-a1a3-92dc234d254b", "4f6fede0-7e17-4c44-af0b-ad0b9c1c98fa" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8883fbd9-4420-4444-84ca-ca31e8221cc7");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "df129349-c335-46e1-86e6-22a179d808d3", "3f67700c-232a-4bc1-b3b5-f8aef4630b1e" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "d6c200c6-ff1f-4bd8-a1a3-92dc234d254b", "4f6fede0-7e17-4c44-af0b-ad0b9c1c98fa" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d6c200c6-ff1f-4bd8-a1a3-92dc234d254b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "df129349-c335-46e1-86e6-22a179d808d3");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3f67700c-232a-4bc1-b3b5-f8aef4630b1e");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "4f6fede0-7e17-4c44-af0b-ad0b9c1c98fa");
        }
    }
}
