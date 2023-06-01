using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebAppExam.Migrations.Identity
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "821a2f30-9cfc-4233-aefb-e286bdd17beb");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "943e4f12-21f2-419e-9ea6-59ca609a3fef");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "66e3a3df-163c-4ff1-8f8c-e276148791bf", null, "user", "USER" },
                    { "6fb5c3fc-72f9-40eb-a9e9-2af0a2c1d1ab", null, "admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "66e3a3df-163c-4ff1-8f8c-e276148791bf");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6fb5c3fc-72f9-40eb-a9e9-2af0a2c1d1ab");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "821a2f30-9cfc-4233-aefb-e286bdd17beb", null, "admin", "ADMIN" },
                    { "943e4f12-21f2-419e-9ea6-59ca609a3fef", null, "user", "USER" }
                });
        }
    }
}
