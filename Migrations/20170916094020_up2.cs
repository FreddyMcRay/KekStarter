using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace KekStarter.Migrations
{
    public partial class up2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SumCurrent",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "SumRequired",
                table: "Project");

            migrationBuilder.AddColumn<int>(
                name: "currentSum",
                table: "Project",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "requiredSum",
                table: "Project",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "currentSum",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "requiredSum",
                table: "Project");

            migrationBuilder.AddColumn<int>(
                name: "SumCurrent",
                table: "Project",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SumRequired",
                table: "Project",
                nullable: false,
                defaultValue: 0);
        }
    }
}
