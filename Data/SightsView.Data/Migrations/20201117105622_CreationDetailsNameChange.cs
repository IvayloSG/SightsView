using Microsoft.EntityFrameworkCore.Migrations;

namespace SightsView.Data.Migrations
{
    public partial class CreationDetailsNameChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Creaions_Details_DetailId",
                table: "Creaions");

            migrationBuilder.DropIndex(
                name: "IX_Creaions_DetailId",
                table: "Creaions");

            migrationBuilder.DropColumn(
                name: "DetailId",
                table: "Creaions");

            migrationBuilder.AddColumn<int>(
                name: "DetailsId",
                table: "Creaions",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Creaions_DetailsId",
                table: "Creaions",
                column: "DetailsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Creaions_Details_DetailsId",
                table: "Creaions",
                column: "DetailsId",
                principalTable: "Details",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Creaions_Details_DetailsId",
                table: "Creaions");

            migrationBuilder.DropIndex(
                name: "IX_Creaions_DetailsId",
                table: "Creaions");

            migrationBuilder.DropColumn(
                name: "DetailsId",
                table: "Creaions");

            migrationBuilder.AddColumn<int>(
                name: "DetailId",
                table: "Creaions",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Creaions_DetailId",
                table: "Creaions",
                column: "DetailId");

            migrationBuilder.AddForeignKey(
                name: "FK_Creaions_Details_DetailId",
                table: "Creaions",
                column: "DetailId",
                principalTable: "Details",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
