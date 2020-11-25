using Microsoft.EntityFrameworkCore.Migrations;

namespace SightsView.Data.Migrations
{
    public partial class FixCreationsTableName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Creaions_CreationId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Creaions_Categories_CategoryId",
                table: "Creaions");

            migrationBuilder.DropForeignKey(
                name: "FK_Creaions_Countries_CountryId",
                table: "Creaions");

            migrationBuilder.DropForeignKey(
                name: "FK_Creaions_AspNetUsers_CreatorId",
                table: "Creaions");

            migrationBuilder.DropForeignKey(
                name: "FK_Creaions_Details_DetailsId",
                table: "Creaions");

            migrationBuilder.DropForeignKey(
                name: "FK_Creaions_Equipment_EquipmentId",
                table: "Creaions");

            migrationBuilder.DropForeignKey(
                name: "FK_Likes_Creaions_CreationId",
                table: "Likes");

            migrationBuilder.DropForeignKey(
                name: "FK_TagCreations_Creaions_CreationId",
                table: "TagCreations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Creaions",
                table: "Creaions");

            migrationBuilder.RenameTable(
                name: "Creaions",
                newName: "Creations");

            migrationBuilder.RenameIndex(
                name: "IX_Creaions_IsDeleted",
                table: "Creations",
                newName: "IX_Creations_IsDeleted");

            migrationBuilder.RenameIndex(
                name: "IX_Creaions_EquipmentId",
                table: "Creations",
                newName: "IX_Creations_EquipmentId");

            migrationBuilder.RenameIndex(
                name: "IX_Creaions_DetailsId",
                table: "Creations",
                newName: "IX_Creations_DetailsId");

            migrationBuilder.RenameIndex(
                name: "IX_Creaions_CreatorId",
                table: "Creations",
                newName: "IX_Creations_CreatorId");

            migrationBuilder.RenameIndex(
                name: "IX_Creaions_CountryId",
                table: "Creations",
                newName: "IX_Creations_CountryId");

            migrationBuilder.RenameIndex(
                name: "IX_Creaions_CategoryId",
                table: "Creations",
                newName: "IX_Creations_CategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Creations",
                table: "Creations",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Creations_CreationId",
                table: "Comments",
                column: "CreationId",
                principalTable: "Creations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Creations_Categories_CategoryId",
                table: "Creations",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Creations_Countries_CountryId",
                table: "Creations",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Creations_AspNetUsers_CreatorId",
                table: "Creations",
                column: "CreatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Creations_Details_DetailsId",
                table: "Creations",
                column: "DetailsId",
                principalTable: "Details",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Creations_Equipment_EquipmentId",
                table: "Creations",
                column: "EquipmentId",
                principalTable: "Equipment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Likes_Creations_CreationId",
                table: "Likes",
                column: "CreationId",
                principalTable: "Creations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TagCreations_Creations_CreationId",
                table: "TagCreations",
                column: "CreationId",
                principalTable: "Creations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Creations_CreationId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Creations_Categories_CategoryId",
                table: "Creations");

            migrationBuilder.DropForeignKey(
                name: "FK_Creations_Countries_CountryId",
                table: "Creations");

            migrationBuilder.DropForeignKey(
                name: "FK_Creations_AspNetUsers_CreatorId",
                table: "Creations");

            migrationBuilder.DropForeignKey(
                name: "FK_Creations_Details_DetailsId",
                table: "Creations");

            migrationBuilder.DropForeignKey(
                name: "FK_Creations_Equipment_EquipmentId",
                table: "Creations");

            migrationBuilder.DropForeignKey(
                name: "FK_Likes_Creations_CreationId",
                table: "Likes");

            migrationBuilder.DropForeignKey(
                name: "FK_TagCreations_Creations_CreationId",
                table: "TagCreations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Creations",
                table: "Creations");

            migrationBuilder.RenameTable(
                name: "Creations",
                newName: "Creaions");

            migrationBuilder.RenameIndex(
                name: "IX_Creations_IsDeleted",
                table: "Creaions",
                newName: "IX_Creaions_IsDeleted");

            migrationBuilder.RenameIndex(
                name: "IX_Creations_EquipmentId",
                table: "Creaions",
                newName: "IX_Creaions_EquipmentId");

            migrationBuilder.RenameIndex(
                name: "IX_Creations_DetailsId",
                table: "Creaions",
                newName: "IX_Creaions_DetailsId");

            migrationBuilder.RenameIndex(
                name: "IX_Creations_CreatorId",
                table: "Creaions",
                newName: "IX_Creaions_CreatorId");

            migrationBuilder.RenameIndex(
                name: "IX_Creations_CountryId",
                table: "Creaions",
                newName: "IX_Creaions_CountryId");

            migrationBuilder.RenameIndex(
                name: "IX_Creations_CategoryId",
                table: "Creaions",
                newName: "IX_Creaions_CategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Creaions",
                table: "Creaions",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Creaions_CreationId",
                table: "Comments",
                column: "CreationId",
                principalTable: "Creaions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Creaions_Categories_CategoryId",
                table: "Creaions",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Creaions_Countries_CountryId",
                table: "Creaions",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Creaions_AspNetUsers_CreatorId",
                table: "Creaions",
                column: "CreatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Creaions_Details_DetailsId",
                table: "Creaions",
                column: "DetailsId",
                principalTable: "Details",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Creaions_Equipment_EquipmentId",
                table: "Creaions",
                column: "EquipmentId",
                principalTable: "Equipment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Likes_Creaions_CreationId",
                table: "Likes",
                column: "CreationId",
                principalTable: "Creaions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TagCreations_Creaions_CreationId",
                table: "TagCreations",
                column: "CreationId",
                principalTable: "Creaions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
