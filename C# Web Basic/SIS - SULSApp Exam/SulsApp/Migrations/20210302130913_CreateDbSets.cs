using Microsoft.EntityFrameworkCore.Migrations;

namespace SulsApp.Migrations
{
    public partial class CreateDbSets : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Submission_Problem_ProblemId",
                table: "Submission");

            migrationBuilder.DropForeignKey(
                name: "FK_Submission_User_UserId",
                table: "Submission");

            migrationBuilder.DropPrimaryKey(
                name: "PK_User",
                table: "User");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Submission",
                table: "Submission");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Problem",
                table: "Problem");

            migrationBuilder.RenameTable(
                name: "User",
                newName: "Users");

            migrationBuilder.RenameTable(
                name: "Submission",
                newName: "Submissions");

            migrationBuilder.RenameTable(
                name: "Problem",
                newName: "Problems");

            migrationBuilder.RenameIndex(
                name: "IX_Submission_UserId",
                table: "Submissions",
                newName: "IX_Submissions_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Submission_ProblemId",
                table: "Submissions",
                newName: "IX_Submissions_ProblemId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Submissions",
                table: "Submissions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Problems",
                table: "Problems",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Submissions_Problems_ProblemId",
                table: "Submissions",
                column: "ProblemId",
                principalTable: "Problems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Submissions_Users_UserId",
                table: "Submissions",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Submissions_Problems_ProblemId",
                table: "Submissions");

            migrationBuilder.DropForeignKey(
                name: "FK_Submissions_Users_UserId",
                table: "Submissions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Submissions",
                table: "Submissions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Problems",
                table: "Problems");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "User");

            migrationBuilder.RenameTable(
                name: "Submissions",
                newName: "Submission");

            migrationBuilder.RenameTable(
                name: "Problems",
                newName: "Problem");

            migrationBuilder.RenameIndex(
                name: "IX_Submissions_UserId",
                table: "Submission",
                newName: "IX_Submission_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Submissions_ProblemId",
                table: "Submission",
                newName: "IX_Submission_ProblemId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_User",
                table: "User",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Submission",
                table: "Submission",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Problem",
                table: "Problem",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Submission_Problem_ProblemId",
                table: "Submission",
                column: "ProblemId",
                principalTable: "Problem",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Submission_User_UserId",
                table: "Submission",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
