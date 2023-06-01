using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebAppExam.Migrations.Identity
{
    /// <inheritdoc />
    public partial class RemovesAddress : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "66e3a3df-163c-4ff1-8f8c-e276148791bf");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6fb5c3fc-72f9-40eb-a9e9-2af0a2c1d1ab");

            migrationBuilder.DropColumn(
                name: "CompanyName",
                table: "UserProfiles");

            migrationBuilder.DropColumn(
                name: "ProfileImage",
                table: "UserProfiles");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "5acf3e8b-bab9-4c9b-a765-56ccf5e476ce", null, "user", "USER" },
                    { "8cb62b1c-5980-4aca-b093-27fc5bd12edf", null, "admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5acf3e8b-bab9-4c9b-a765-56ccf5e476ce");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8cb62b1c-5980-4aca-b093-27fc5bd12edf");

            migrationBuilder.AddColumn<string>(
                name: "CompanyName",
                table: "UserProfiles",
                type: "nvarchar(50)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProfileImage",
                table: "UserProfiles",
                type: "varchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "66e3a3df-163c-4ff1-8f8c-e276148791bf", null, "user", "USER" },
                    { "6fb5c3fc-72f9-40eb-a9e9-2af0a2c1d1ab", null, "admin", "ADMIN" }
                });
        }
    }
}
