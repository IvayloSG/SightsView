using Microsoft.EntityFrameworkCore.Migrations;

namespace SightsView.Data.Migrations
{
    public partial class FixReplyCommentId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Replies_Comments_CommentId1",
                table: "Replies");

            migrationBuilder.DropIndex(
                name: "IX_Replies_CommentId1",
                table: "Replies");

            migrationBuilder.DropColumn(
                name: "CommentId1",
                table: "Replies");

            migrationBuilder.AlterColumn<int>(
                name: "CommentId",
                table: "Replies",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Replies_CommentId",
                table: "Replies",
                column: "CommentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Replies_Comments_CommentId",
                table: "Replies",
                column: "CommentId",
                principalTable: "Comments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Replies_Comments_CommentId",
                table: "Replies");

            migrationBuilder.DropIndex(
                name: "IX_Replies_CommentId",
                table: "Replies");

            migrationBuilder.AlterColumn<string>(
                name: "CommentId",
                table: "Replies",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "CommentId1",
                table: "Replies",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Replies_CommentId1",
                table: "Replies",
                column: "CommentId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Replies_Comments_CommentId1",
                table: "Replies",
                column: "CommentId1",
                principalTable: "Comments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
