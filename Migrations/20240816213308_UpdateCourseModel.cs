using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LearningPlatform.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCourseModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_AspNetUsers_ProfessorId",
                table: "Courses");

            migrationBuilder.RenameColumn(
                name: "ProfessorId",
                table: "Courses",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Courses_ProfessorId",
                table: "Courses",
                newName: "IX_Courses_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_AspNetUsers_UserId",
                table: "Courses",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_AspNetUsers_UserId",
                table: "Courses");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Courses",
                newName: "ProfessorId");

            migrationBuilder.RenameIndex(
                name: "IX_Courses_UserId",
                table: "Courses",
                newName: "IX_Courses_ProfessorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_AspNetUsers_ProfessorId",
                table: "Courses",
                column: "ProfessorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
