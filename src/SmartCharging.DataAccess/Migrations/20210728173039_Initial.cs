using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SmartCharging.DataAccess.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Groups",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    Amps = table.Column<float>(type: "real", nullable: false),
                    UsedAmps = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Groups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ChargeStation",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChargeStation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChargeStation_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Connector",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    ChargeStationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Amps = table.Column<float>(type: "real", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Connector", x => new { x.Id, x.ChargeStationId });
                    table.ForeignKey(
                        name: "FK_Connector_ChargeStation_ChargeStationId",
                        column: x => x.ChargeStationId,
                        principalTable: "ChargeStation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChargeStation_GroupId",
                table: "ChargeStation",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Connector_ChargeStationId",
                table: "Connector",
                column: "ChargeStationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Connector");

            migrationBuilder.DropTable(
                name: "ChargeStation");

            migrationBuilder.DropTable(
                name: "Groups");
        }
    }
}
