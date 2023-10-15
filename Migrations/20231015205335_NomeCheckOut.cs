using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PousadaIdentity.Migrations
{
    /// <inheritdoc />
    public partial class NomeCheckOut : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CheckUp",
                table: "Reserva",
                newName: "CheckOut");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CheckOut",
                table: "Reserva",
                newName: "CheckUp");
        }
    }
}
