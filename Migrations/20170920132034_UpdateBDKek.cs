using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace KekStarter.Migrations
{
    public partial class UpdateBDKek : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Project_Tag_TagId",
                table: "Project");

            migrationBuilder.DropIndex(
                name: "IX_Project_TagId",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "TagId",
                table: "Project");

            migrationBuilder.AddColumn<string>(
                name: "content",
                table: "Project",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "content",
                table: "Project");

            migrationBuilder.AddColumn<int>(
                name: "TagId",
                table: "Project",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Project_TagId",
                table: "Project",
                column: "TagId");

            migrationBuilder.AddForeignKey(
                name: "FK_Project_Tag_TagId",
                table: "Project",
                column: "TagId",
                principalTable: "Tag",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
