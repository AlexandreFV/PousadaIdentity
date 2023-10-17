using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PousadaIdentity.Migrations
{
    /// <inheritdoc />
    public partial class addTokenPessoa : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Token",
                table: "Pessoa",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Token",
                table: "Pessoa");
        }
    }
}
