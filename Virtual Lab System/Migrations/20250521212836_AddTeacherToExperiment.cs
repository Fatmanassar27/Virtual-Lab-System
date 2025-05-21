using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Virtual_Lab_System.Migrations
{
    /// <inheritdoc />
    public partial class AddTeacherToExperiment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TeacherId",
                table: "Experiments",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Experiments_TeacherId",
                table: "Experiments",
                column: "TeacherId");

            migrationBuilder.AddForeignKey(
                name: "FK_Experiments_AspNetUsers_TeacherId",
                table: "Experiments",
                column: "TeacherId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Experiments_AspNetUsers_TeacherId",
                table: "Experiments");

            migrationBuilder.DropIndex(
                name: "IX_Experiments_TeacherId",
                table: "Experiments");

            migrationBuilder.DropColumn(
                name: "TeacherId",
                table: "Experiments");
        }
    }
}
