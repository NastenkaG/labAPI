using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace labAPI.Migrations
{
    public partial class DataBase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    CompanyId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 60, nullable: false),
                    Address = table.Column<string>(maxLength: 60, nullable: false),
                    Country = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.CompanyId);
                });

            migrationBuilder.CreateTable(
                name: "Garden",
                columns: table => new
                {
                    GardenId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 60, nullable: false),
                    Country = table.Column<string>(maxLength: 60, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Garden", x => x.GardenId);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    EmployeeId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 30, nullable: false),
                    Age = table.Column<int>(nullable: false),
                    Position = table.Column<string>(maxLength: 20, nullable: false),
                    CompanyId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.EmployeeId);
                    table.ForeignKey(
                        name: "FK_Employees_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "CompanyId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Plant",
                columns: table => new
                {
                    PlantId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 60, nullable: false),
                    Position = table.Column<string>(maxLength: 60, nullable: false),
                    GardenId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plant", x => x.PlantId);
                    table.ForeignKey(
                        name: "FK_Plant_Garden_GardenId",
                        column: x => x.GardenId,
                        principalTable: "Garden",
                        principalColumn: "GardenId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "CompanyId", "Address", "Country", "Name" },
                values: new object[,]
                {
                    { new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"), "583 Wall Dr. Gwynn Oak, MD 21207", "USA", "IT_Solutions Ltd" },
                    { new Guid("3d490a70-94ce-4d15-9494-5248280c2ce3"), "312 Forest Avenue, BF 923", "USA", "Admin_Solutions Ltd" }
                });

            migrationBuilder.InsertData(
                table: "Garden",
                columns: new[] { "GardenId", "Country", "Name" },
                values: new object[,]
                {
                    { new Guid("81abbca8-664d-4b20-b5de-024705497d4a"), "USA", "Tropics" },
                    { new Guid("80abbca8-664d-4b20-b9de-024705497d4a"), "USA", "Desert" }
                });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "EmployeeId", "Age", "CompanyId", "Name", "Position" },
                values: new object[,]
                {
                    { new Guid("80abbca8-664d-4b20-b5de-024705497d4a"), 26, new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"), "Sam Raiden", "Software developer" },
                    { new Guid("86dba8c0-d178-41e7-938c-ed49778fb52a"), 30, new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"), "Jana McLeaf", "Software developer" },
                    { new Guid("021ca3c1-0deb-4afd-ae94-2159a8479811"), 35, new Guid("3d490a70-94ce-4d15-9494-5248280c2ce3"), "Kane Miller", "Administrator" }
                });

            migrationBuilder.InsertData(
                table: "Plant",
                columns: new[] { "PlantId", "GardenId", "Name", "Position" },
                values: new object[,]
                {
                    { new Guid("88acbca8-684d-4b20-b5de-024705497d4a"), new Guid("81abbca8-664d-4b20-b5de-024705497d4a"), "Derevo", "Zemlia" },
                    { new Guid("86dba8c0-d158-41f7-938c-ed49778cb52a"), new Guid("81abbca8-664d-4b20-b5de-024705497d4a"), "Flower", "Trava" },
                    { new Guid("021ca3c1-0deb-4bbd-ae94-2159a9679811"), new Guid("80abbca8-664d-4b20-b9de-024705497d4a"), "Kaktus", "Pesok" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Employees_CompanyId",
                table: "Employees",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Plant_GardenId",
                table: "Plant",
                column: "GardenId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Plant");

            migrationBuilder.DropTable(
                name: "Companies");

            migrationBuilder.DropTable(
                name: "Garden");
        }
    }
}
