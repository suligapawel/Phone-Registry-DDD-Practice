using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PhoneRegistryDDD.Warehouse.Core.DAL.EntityFramework.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "warehouse");

            migrationBuilder.CreateTable(
                name: "SimCards",
                schema: "warehouse",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Number = table.Column<string>(type: "character varying(9)", maxLength: 9, nullable: false),
                    Pin = table.Column<string>(type: "character varying(12)", maxLength: 12, nullable: false),
                    Puk = table.Column<string>(type: "character varying(8)", maxLength: 8, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SimCards", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Smartphones",
                schema: "warehouse",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Imei = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    Brand = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    Model = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Smartphones", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SimCards",
                schema: "warehouse");

            migrationBuilder.DropTable(
                name: "Smartphones",
                schema: "warehouse");
        }
    }
}
