using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WeatherAPI.Migrations
{
    /// <inheritdoc />
    public partial class Update1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sensors_Users_OwnerId",
                table: "Sensors");

            migrationBuilder.DropIndex(
                name: "IX_Sensors_OwnerId",
                table: "Sensors");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Sensors");

            migrationBuilder.AlterColumn<double>(
                name: "Lon",
                table: "Sensors",
                type: "float",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<double>(
                name: "Lat",
                table: "Sensors",
                type: "float",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AddColumn<string>(
                name: "SerialNumber",
                table: "Sensors",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Sensors",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Sensors_UserId",
                table: "Sensors",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Sensors_Users_UserId",
                table: "Sensors",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sensors_Users_UserId",
                table: "Sensors");

            migrationBuilder.DropIndex(
                name: "IX_Sensors_UserId",
                table: "Sensors");

            migrationBuilder.DropColumn(
                name: "SerialNumber",
                table: "Sensors");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Sensors");

            migrationBuilder.AlterColumn<double>(
                name: "Lon",
                table: "Sensors",
                type: "float",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "Lat",
                table: "Sensors",
                type: "float",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OwnerId",
                table: "Sensors",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Sensors_OwnerId",
                table: "Sensors",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Sensors_Users_OwnerId",
                table: "Sensors",
                column: "OwnerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
