using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SysMaTra.Migrations
{
    /// <inheritdoc />
    public partial class UpAttrib : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Couleur_Configuration_ConfigurationId",
                table: "Couleur");

            migrationBuilder.AlterColumn<int>(
                name: "ConfigurationId",
                table: "Couleur",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Couleur_Configuration_ConfigurationId",
                table: "Couleur",
                column: "ConfigurationId",
                principalTable: "Configuration",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Couleur_Configuration_ConfigurationId",
                table: "Couleur");

            migrationBuilder.AlterColumn<int>(
                name: "ConfigurationId",
                table: "Couleur",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Couleur_Configuration_ConfigurationId",
                table: "Couleur",
                column: "ConfigurationId",
                principalTable: "Configuration",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
