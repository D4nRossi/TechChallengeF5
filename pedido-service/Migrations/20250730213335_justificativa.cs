using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace pedido_service.Migrations
{
    /// <inheritdoc />
    public partial class justificativa : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "JustificativaCancelamento",
                table: "Pedidos",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "JustificativaCancelamento",
                table: "Pedidos");
        }
    }
}
