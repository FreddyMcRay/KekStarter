using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace KekStarter.Migrations
{
    public partial class UpdateType2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InstructionTag_Project_ProjectId",
                table: "InstructionTag");

            migrationBuilder.AlterColumn<int>(
                name: "ProjectId",
                table: "InstructionTag",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_InstructionTag_Project_ProjectId",
                table: "InstructionTag",
                column: "ProjectId",
                principalTable: "Project",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InstructionTag_Project_ProjectId",
                table: "InstructionTag");

            migrationBuilder.AlterColumn<int>(
                name: "ProjectId",
                table: "InstructionTag",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_InstructionTag_Project_ProjectId",
                table: "InstructionTag",
                column: "ProjectId",
                principalTable: "Project",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
