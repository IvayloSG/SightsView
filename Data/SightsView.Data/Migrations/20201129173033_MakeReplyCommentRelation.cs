using Microsoft.EntityFrameworkCore.Migrations;

namespace SightsView.Data.Migrations
{
    public partial class MakeReplyCommentRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Replies_Messages_MessageId1",
                table: "Replies");

            migrationBuilder.AlterColumn<int>(
                name: "MessageId",
                table: "Replies",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "CommentId",
                table: "Replies",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Notes",
                table: "Equipment",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TipAndTricks",
                table: "Details",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Replies_MessageId",
                table: "Replies",
                column: "MessageId");

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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "CommentId",
                table: "Replies");

            migrationBuilder.DropColumn(
                name: "Notes",
                table: "Equipment");

            migrationBuilder.DropColumn(
                name: "TipAndTricks",
                table: "Details");

            migrationBuilder.AlterColumn<string>(
                name: "MessageId",
                table: "Replies",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Replies_Messages_MessageId1",
                table: "Replies",
                column: "MessageId1",
                principalTable: "Messages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
