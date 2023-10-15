using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PousadaIdentity.Migrations
{
    /// <inheritdoc />
    public partial class PrecoQuarto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ValorTotalQuarto",
                table: "Quarto");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ValorTotalQuarto",
                table: "Quarto",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
