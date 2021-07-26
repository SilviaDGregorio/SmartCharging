using Microsoft.EntityFrameworkCore.Migrations;

namespace SmartCharging.DataAccess.Migrations
{
    public partial class Connection : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "UsedAmps",
                table: "Groups",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.CreateTable(
                name: "Connector",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ChargeStationId = table.Column<int>(type: "int", nullable: false),
                    Amps = table.Column<float>(type: "real", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Connector", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Connector_ChargeStation_ChargeStationId",
                        column: x => x.ChargeStationId,
                        principalTable: "ChargeStation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Connector_ChargeStationId",
                table: "Connector",
                column: "ChargeStationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Connector");

            migrationBuilder.DropColumn(
                name: "UsedAmps",
                table: "Groups");
        }
    }
}
