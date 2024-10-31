using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PizzeriaWeb3._1.Migrations
{
    /// <inheritdoc />
    public partial class migracion3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Productos_Usuarios_UsuarioId",
                table: "Productos");

            migrationBuilder.DropIndex(
                name: "IX_Productos_UsuarioId",
                table: "Productos");

            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "Productos");

            migrationBuilder.AddColumn<int>(
                name: "UsuariosIdUsuario",
                table: "Productos",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Productos_UsuariosIdUsuario",
                table: "Productos",
                column: "UsuariosIdUsuario");

            migrationBuilder.AddForeignKey(
                name: "FK_Productos_Usuarios_UsuariosIdUsuario",
                table: "Productos",
                column: "UsuariosIdUsuario",
                principalTable: "Usuarios",
                principalColumn: "IdUsuario");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Productos_Usuarios_UsuariosIdUsuario",
                table: "Productos");

            migrationBuilder.DropIndex(
                name: "IX_Productos_UsuariosIdUsuario",
                table: "Productos");

            migrationBuilder.DropColumn(
                name: "UsuariosIdUsuario",
                table: "Productos");

            migrationBuilder.AddColumn<int>(
                name: "UsuarioId",
                table: "Productos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Productos_UsuarioId",
                table: "Productos",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Productos_Usuarios_UsuarioId",
                table: "Productos",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "IdUsuario",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
