using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace KekStarter.Migrations
{
    public partial class up : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PreviewImageUrl",
                table: "Project");

            migrationBuilder.AddColumn<string>(
                name: "urlImage",
                table: "Project",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "urlImage",
                table: "Project");

            migrationBuilder.AddColumn<string>(
                name: "PreviewImageUrl",
                table: "Project",
                nullable: true);
        }
    }
}
