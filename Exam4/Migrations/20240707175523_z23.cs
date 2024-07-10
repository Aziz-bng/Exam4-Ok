using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Exam4.Migrations
{
    /// <inheritdoc />
    public partial class z23 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalQuestions",
                table: "Exams");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TotalQuestions",
                table: "Exams",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
