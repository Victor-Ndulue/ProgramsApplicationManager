using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Application.Migrations
{
    /// <inheritdoc />
    public partial class changedDeleteBehaviour : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Choices_CustomQuestions_QuestionId",
                table: "Choices");

            migrationBuilder.AddForeignKey(
                name: "FK_Choices_CustomQuestions_QuestionId",
                table: "Choices",
                column: "QuestionId",
                principalTable: "CustomQuestions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Choices_CustomQuestions_QuestionId",
                table: "Choices");

            migrationBuilder.AddForeignKey(
                name: "FK_Choices_CustomQuestions_QuestionId",
                table: "Choices",
                column: "QuestionId",
                principalTable: "CustomQuestions",
                principalColumn: "Id");
        }
    }
}
