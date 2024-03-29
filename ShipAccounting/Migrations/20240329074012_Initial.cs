using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShipAccounting.Migrations;

/// <inheritdoc />
public partial class Initial : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Battles",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Date = table.Column<DateTime>(type: "datetime2", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Battles", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Classes",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                NumGuns = table.Column<short>(type: "smallint", nullable: true),
                Bore = table.Column<short>(type: "smallint", nullable: true),
                Displacement = table.Column<int>(type: "int", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Classes", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Ships",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Launched = table.Column<short>(type: "smallint", nullable: true),
                ClassId = table.Column<int>(type: "int", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Ships", x => x.Id);
                table.ForeignKey(
                    name: "FK_Ships_Classes_ClassId",
                    column: x => x.ClassId,
                    principalTable: "Classes",
                    principalColumn: "Id");
            });

        migrationBuilder.CreateTable(
            name: "Outcomes",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                ShipId = table.Column<int>(type: "int", nullable: false),
                BattleId = table.Column<int>(type: "int", nullable: false),
                Result = table.Column<string>(type: "nvarchar(max)", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Outcomes", x => x.Id);
                table.ForeignKey(
                    name: "FK_Outcomes_Battles_BattleId",
                    column: x => x.BattleId,
                    principalTable: "Battles",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_Outcomes_Ships_ShipId",
                    column: x => x.ShipId,
                    principalTable: "Ships",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "IX_Outcomes_BattleId",
            table: "Outcomes",
            column: "BattleId");

        migrationBuilder.CreateIndex(
            name: "IX_Outcomes_ShipId",
            table: "Outcomes",
            column: "ShipId");

        migrationBuilder.CreateIndex(
            name: "IX_Ships_ClassId",
            table: "Ships",
            column: "ClassId");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "Outcomes");

        migrationBuilder.DropTable(
            name: "Battles");

        migrationBuilder.DropTable(
            name: "Ships");

        migrationBuilder.DropTable(
            name: "Classes");
    }
}
