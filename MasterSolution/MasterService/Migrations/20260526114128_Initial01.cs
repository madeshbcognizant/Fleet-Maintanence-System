using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MasterService.Migrations
{
    /// <inheritdoc />
    public partial class Initial01 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Masters",
                columns: table => new
                {
                    Type = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    NameOfService = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FreqDays = table.Column<int>(type: "int", nullable: false),
                    FreqKm = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PollutionCertificateRenewal = table.Column<int>(type: "int", nullable: false),
                    InsuranceRenewal = table.Column<int>(type: "int", nullable: false),
                    FCRenewal = table.Column<int>(type: "int", nullable: false),
                    PermitRenewal = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Masters", x => x.Type);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Masters");
        }
    }
}
