using Microsoft.EntityFrameworkCore.Migrations;

namespace labAPI.Migrations
{
    public partial class AddedRolesToDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "ea81e984-e333-4920-94d8-7581baec935e", "fdfa12b6-a476-40c3-8bdc-d9a924bd8fb6", "Manager", "MANAGER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "105fd895-3799-4156-9c3d-4bc60905a379", "51522b37-96a3-4f29-a1d8-381eb6cd2f14", "Administrator", "ADMINISTRATOR" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "105fd895-3799-4156-9c3d-4bc60905a379");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ea81e984-e333-4920-94d8-7581baec935e");
        }
    }
}
