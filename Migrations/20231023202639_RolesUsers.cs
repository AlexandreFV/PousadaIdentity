using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PousadaIdentity.Migrations
{
    /// <inheritdoc />
    public partial class RolesUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
            table: "AspNetRoles",
            columns: new[] { "Id", "Name", "NormalizedName", "ConcurrencyStamp" },
            values: new object[] { "1", "Cliente", "CLIENT", null });

            migrationBuilder.InsertData(
          table: "AspNetRoles",
          columns: new[] { "Id", "Name", "NormalizedName", "ConcurrencyStamp" },
          values: new object[] { "2", "Funcionario", "FUNCI", null });
        

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
