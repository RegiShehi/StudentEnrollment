using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace StudentEnrollment.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "identity",
                table: "Roles",
                keyColumn: "Id",
                keyValue: "698a1961-842d-4fe9-b4e6-7b4ac13c3c23");

            migrationBuilder.DeleteData(
                schema: "identity",
                table: "Roles",
                keyColumn: "Id",
                keyValue: "7eff6120-e03f-46c8-a105-78810dd29325");

            migrationBuilder.InsertData(
                schema: "identity",
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "6b0c7956-171c-4c71-808d-52aaf1e024a7", null, "User", "USER" },
                    { "7a40a1b6-00dc-4095-8bcd-88f6e2209559", null, "Administrator", "ADMINISTRATOR" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "identity",
                table: "Roles",
                keyColumn: "Id",
                keyValue: "6b0c7956-171c-4c71-808d-52aaf1e024a7");

            migrationBuilder.DeleteData(
                schema: "identity",
                table: "Roles",
                keyColumn: "Id",
                keyValue: "7a40a1b6-00dc-4095-8bcd-88f6e2209559");

            migrationBuilder.InsertData(
                schema: "identity",
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "698a1961-842d-4fe9-b4e6-7b4ac13c3c23", null, "Administrator", "ADMINISTRATOR" },
                    { "7eff6120-e03f-46c8-a105-78810dd29325", null, "User", "USER" }
                });
        }
    }
}
