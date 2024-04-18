using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace OgWeb.Migrations
{
    /// <inheritdoc />
    public partial class AddingIdentityRoleValues : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "8bd4df54-9ddc-4873-89f5-a4c7ffc09f0a", null, "admin", "admin" },
                    { "aed1c889-720b-4aa9-b046-02d24994668f", null, "client", "client" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8bd4df54-9ddc-4873-89f5-a4c7ffc09f0a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "aed1c889-720b-4aa9-b046-02d24994668f");
        }
    }
}
