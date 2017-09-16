using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace KekStarter.Migrations
{
    public partial class up3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "leftOver",
                table: "Project",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "progress",
                table: "Project",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "leftOver",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "progress",
                table: "Project");
        }
    }
}
