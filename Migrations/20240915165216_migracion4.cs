using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PizzeriaWeb3._1.Migrations
{
    /// <inheritdoc />
    public partial class migracion4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Mesas_Usuarios_UsuarioId",
                table: "Mesas");

            migrationBuilder.DropIndex(
                name: "IX_Mesas_UsuarioId",
                table: "Mesas");

            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "Mesas");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UsuarioId",
                table: "Mesas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Mesas_UsuarioId",
                table: "Mesas",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Mesas_Usuarios_UsuarioId",
                table: "Mesas",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "IdUsuario",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
