using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrderManagementAPI.Migrations
{
    public partial class CorrectingForeignKeys : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Produtos_Categorias_IdCategoria",
                table: "Produtos");

            migrationBuilder.DropIndex(
                name: "IX_Produtos_IdCategoria",
                table: "Produtos");

            migrationBuilder.AddColumn<int>(
                name: "CategoriaIdCategoria",
                table: "Produtos",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IdCategoria",
                table: "PedidoProdutos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Produtos_CategoriaIdCategoria",
                table: "Produtos",
                column: "CategoriaIdCategoria");

            migrationBuilder.AddForeignKey(
                name: "FK_Produtos_Categorias_CategoriaIdCategoria",
                table: "Produtos",
                column: "CategoriaIdCategoria",
                principalTable: "Categorias",
                principalColumn: "IdCategoria");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Produtos_Categorias_CategoriaIdCategoria",
                table: "Produtos");

            migrationBuilder.DropIndex(
                name: "IX_Produtos_CategoriaIdCategoria",
                table: "Produtos");

            migrationBuilder.DropColumn(
                name: "CategoriaIdCategoria",
                table: "Produtos");

            migrationBuilder.DropColumn(
                name: "IdCategoria",
                table: "PedidoProdutos");

            migrationBuilder.CreateIndex(
                name: "IX_Produtos_IdCategoria",
                table: "Produtos",
                column: "IdCategoria");

            migrationBuilder.AddForeignKey(
                name: "FK_Produtos_Categorias_IdCategoria",
                table: "Produtos",
                column: "IdCategoria",
                principalTable: "Categorias",
                principalColumn: "IdCategoria",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
