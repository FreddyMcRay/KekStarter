using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace KekStarter.Migrations
{
    public partial class Update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Step");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Step");

            migrationBuilder.AddColumn<string>(
                name: "cost",
                table: "Step",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "title",
                table: "Step",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "cost",
                table: "Step");

            migrationBuilder.DropColumn(
                name: "title",
                table: "Step");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Step",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Price",
                table: "Step",
                nullable: true);
        }
    }
}
