using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PhoneRegistryDDD.Helpdesk.Infrastructure.EntityFramework.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "helpdesk");

            migrationBuilder.CreateTable(
                name: "Employees",
                schema: "helpdesk",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Simcards",
                schema: "helpdesk",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    EmployeeId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Simcards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Simcards_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalSchema: "helpdesk",
                        principalTable: "Employees",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Devices",
                schema: "helpdesk",
                columns: table => new
                {
                    SimCardId = table.Column<Guid>(type: "uuid", nullable: false),
                    Id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Devices", x => x.SimCardId);
                    table.ForeignKey(
                        name: "FK_Devices_Simcards_SimCardId",
                        column: x => x.SimCardId,
                        principalSchema: "helpdesk",
                        principalTable: "Simcards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Simcards_EmployeeId",
                schema: "helpdesk",
                table: "Simcards",
                column: "EmployeeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Devices",
                schema: "helpdesk");

            migrationBuilder.DropTable(
                name: "Simcards",
                schema: "helpdesk");

            migrationBuilder.DropTable(
                name: "Employees",
                schema: "helpdesk");
        }
    }
}
