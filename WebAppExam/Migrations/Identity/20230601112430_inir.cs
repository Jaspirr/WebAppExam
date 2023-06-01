using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebAppExam.Migrations.Identity
{
    /// <inheritdoc />
    public partial class inir : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0e720096-5c02-49ee-b5ea-d88766bd456b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6e78bb8d-70ab-4d66-b423-1517214f08ae");

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

            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StreetName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "821a2f30-9cfc-4233-aefb-e286bdd17beb", null, "admin", "ADMIN" },
                    { "943e4f12-21f2-419e-9ea6-59ca609a3fef", null, "user", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "821a2f30-9cfc-4233-aefb-e286bdd17beb");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "943e4f12-21f2-419e-9ea6-59ca609a3fef");

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
                    { "0e720096-5c02-49ee-b5ea-d88766bd456b", null, "admin", "ADMIN" },
                    { "6e78bb8d-70ab-4d66-b423-1517214f08ae", null, "user", "USER" }
                });
        }
    }
}
