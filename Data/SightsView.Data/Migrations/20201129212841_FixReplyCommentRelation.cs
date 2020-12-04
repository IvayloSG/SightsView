using Microsoft.EntityFrameworkCore.Migrations;

namespace SightsView.Data.Migrations
{
    public partial class FixReplyCommentRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Replies_Messages_MessageId",
                table: "Replies");

            migrationBuilder.DropForeignKey(
                name: "FK_Replies_Comments_MessageId1",
                table: "Replies");

            migrationBuilder.DropIndex(
                name: "IX_Replies_MessageId",
                table: "Replies");

            migrationBuilder.DropIndex(
                name: "IX_Replies_MessageId1",
                table: "Replies");

            migrationBuilder.DropColumn(
                name: "MessageId",
                table: "Replies");

            migrationBuilder.DropColumn(
                name: "MessageId1",
                table: "Replies");

            migrationBuilder.AddColumn<int>(
                name: "CommentId1",
                table: "Replies",
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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<int>(
                name: "MessageId",
                table: "Replies",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MessageId1",
                table: "Replies",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Replies_MessageId",
                table: "Replies",
                column: "MessageId");

            migrationBuilder.CreateIndex(
                name: "IX_Replies_MessageId1",
                table: "Replies",
                column: "MessageId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Replies_Messages_MessageId",
                table: "Replies",
                column: "MessageId",
                principalTable: "Messages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Replies_Comments_MessageId1",
                table: "Replies",
                column: "MessageId1",
                principalTable: "Comments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
