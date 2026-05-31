using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OdometerService.Migrations
{
    /// <inheritdoc />
    public partial class first : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Odometers",
                columns: table => new
                {
                    ReadingId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RegId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Current_Kilometer = table.Column<int>(type: "int", nullable: false),
                    TimeStamp = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Odometers", x => x.ReadingId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Odometers");
        }
    }
}
