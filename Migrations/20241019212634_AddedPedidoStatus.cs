using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrderManagementAPI.Migrations
{
    public partial class AddedPedidoStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Pedidos",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Pedidos");
        }
    }
}
