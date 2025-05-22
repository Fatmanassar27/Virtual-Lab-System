using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Virtual_Lab_System.Migrations
{
    /// <inheritdoc />
    public partial class addGrade : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Experiments_AspNetUsers_TeacherId",
                table: "Experiments");

            migrationBuilder.AlterColumn<string>(
                name: "ResultData",
                table: "Reports",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "Comments",
                table: "Reports",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "Grade",
                table: "Reports",
                type: "real",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "TeacherId",
                table: "Experiments",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_Experiments_AspNetUsers_TeacherId",
                table: "Experiments",
                column: "TeacherId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Experiments_AspNetUsers_TeacherId",
                table: "Experiments");

            migrationBuilder.DropColumn(
                name: "Comments",
                table: "Reports");

            migrationBuilder.DropColumn(
                name: "Grade",
                table: "Reports");

            migrationBuilder.AlterColumn<string>(
                name: "ResultData",
                table: "Reports",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "TeacherId",
                table: "Experiments",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Experiments_AspNetUsers_TeacherId",
                table: "Experiments",
                column: "TeacherId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
