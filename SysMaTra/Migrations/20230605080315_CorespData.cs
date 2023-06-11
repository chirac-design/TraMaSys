using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SysMaTra.Migrations
{
    /// <inheritdoc />
    public partial class CorespData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Tarif",
                table: "Trajet",
                type: "float",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<float>(
                name: "Tarif",
                table: "Trajet",
                type: "real",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float",
                oldMaxLength: 100);
        }
    }
}
