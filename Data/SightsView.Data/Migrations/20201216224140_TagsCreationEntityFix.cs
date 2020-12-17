using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SightsView.Data.Migrations
{
    public partial class TagsCreationEntityFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "TagCreations",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "TagCreations",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "TagCreations",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "TagCreations",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOn",
                table: "TagCreations",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TagCreations_IsDeleted",
                table: "TagCreations",
                column: "IsDeleted");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TagCreations_IsDeleted",
                table: "TagCreations");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "TagCreations");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "TagCreations");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "TagCreations");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "TagCreations");

            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                table: "TagCreations");
        }
    }
}
