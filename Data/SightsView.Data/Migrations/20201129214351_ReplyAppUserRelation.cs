using Microsoft.EntityFrameworkCore.Migrations;

namespace SightsView.Data.Migrations
{
    public partial class ReplyAppUserRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Replies",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Replies_ApplicationUserId",
                table: "Replies",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Replies_AspNetUsers_ApplicationUserId",
                table: "Replies",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Replies_AspNetUsers_ApplicationUserId",
                table: "Replies");

            migrationBuilder.DropIndex(
                name: "IX_Replies_ApplicationUserId",
                table: "Replies");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Replies");
        }
    }
}
