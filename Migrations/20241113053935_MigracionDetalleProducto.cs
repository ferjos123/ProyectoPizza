using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PizzeriaWeb3._1.Migrations
{
    /// <inheritdoc />
    public partial class MigracionDetalleProducto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PedidoProductos_Pedidos_PedidoId",
                table: "PedidoProductos");

            migrationBuilder.DropForeignKey(
                name: "FK_PedidoProductos_Productos_ProductoId",
                table: "PedidoProductos");

            migrationBuilder.AddColumn<string>(
                name: "Detalle",
                table: "Productos",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PedidoProductos_Pedidos_PedidoId",
                table: "PedidoProductos",
                column: "PedidoId",
                principalTable: "Pedidos",
                principalColumn: "IdPedidos",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PedidoProductos_Productos_ProductoId",
                table: "PedidoProductos",
                column: "ProductoId",
                principalTable: "Productos",
                principalColumn: "IdProducto",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PedidoProductos_Pedidos_PedidoId",
                table: "PedidoProductos");

            migrationBuilder.DropForeignKey(
                name: "FK_PedidoProductos_Productos_ProductoId",
                table: "PedidoProductos");

            migrationBuilder.DropColumn(
                name: "Detalle",
                table: "Productos");

            migrationBuilder.AddForeignKey(
                name: "FK_PedidoProductos_Pedidos_PedidoId",
                table: "PedidoProductos",
                column: "PedidoId",
                principalTable: "Pedidos",
                principalColumn: "IdPedidos",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PedidoProductos_Productos_ProductoId",
                table: "PedidoProductos",
                column: "ProductoId",
                principalTable: "Productos",
                principalColumn: "IdProducto",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
