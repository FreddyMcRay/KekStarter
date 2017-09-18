using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace KekStarter.Migrations
{
    public partial class fixtype : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InstructionTag_InstructionTag_TagId",
                table: "InstructionTag");

            migrationBuilder.DropForeignKey(
                name: "FK_InstructionTag_Tag_TagId1",
                table: "InstructionTag");

            migrationBuilder.DropIndex(
                name: "IX_InstructionTag_TagId1",
                table: "InstructionTag");

            migrationBuilder.DropColumn(
                name: "TagId1",
                table: "InstructionTag");

            migrationBuilder.AddColumn<int>(
                name: "TagId",
                table: "Project",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Project_TagId",
                table: "Project",
                column: "TagId");

            migrationBuilder.AddForeignKey(
                name: "FK_InstructionTag_Tag_TagId",
                table: "InstructionTag",
                column: "TagId",
                principalTable: "Tag",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Project_Tag_TagId",
                table: "Project",
                column: "TagId",
                principalTable: "Tag",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InstructionTag_Tag_TagId",
                table: "InstructionTag");

            migrationBuilder.DropForeignKey(
                name: "FK_Project_Tag_TagId",
                table: "Project");

            migrationBuilder.DropIndex(
                name: "IX_Project_TagId",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "TagId",
                table: "Project");

            migrationBuilder.AddColumn<int>(
                name: "TagId1",
                table: "InstructionTag",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_InstructionTag_TagId1",
                table: "InstructionTag",
                column: "TagId1");

            migrationBuilder.AddForeignKey(
                name: "FK_InstructionTag_InstructionTag_TagId",
                table: "InstructionTag",
                column: "TagId",
                principalTable: "InstructionTag",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_InstructionTag_Tag_TagId1",
                table: "InstructionTag",
                column: "TagId1",
                principalTable: "Tag",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
