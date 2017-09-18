using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace KekStarter.Migrations
{
    public partial class ExportKek : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Commentary_Instruction_ProjectId",
                table: "Commentary");

            migrationBuilder.DropForeignKey(
                name: "FK_Instruction_UserProfile_UserProfileId",
                table: "Instruction");

            migrationBuilder.DropForeignKey(
                name: "FK_InstructionTag_Instruction_ProjectId",
                table: "InstructionTag");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectNew_Instruction_ProjectId",
                table: "ProjectNew");

            migrationBuilder.DropForeignKey(
                name: "FK_Step_Instruction_ProjectId",
                table: "Step");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Instruction",
                table: "Instruction");

            migrationBuilder.RenameTable(
                name: "Instruction",
                newName: "Project");

            migrationBuilder.RenameIndex(
                name: "IX_Instruction_UserProfileId",
                table: "Project",
                newName: "IX_Project_UserProfileId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Project",
                table: "Project",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Commentary_Project_ProjectId",
                table: "Commentary",
                column: "ProjectId",
                principalTable: "Project",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_InstructionTag_Project_ProjectId",
                table: "InstructionTag",
                column: "ProjectId",
                principalTable: "Project",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Project_UserProfile_UserProfileId",
                table: "Project",
                column: "UserProfileId",
                principalTable: "UserProfile",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectNew_Project_ProjectId",
                table: "ProjectNew",
                column: "ProjectId",
                principalTable: "Project",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Step_Project_ProjectId",
                table: "Step",
                column: "ProjectId",
                principalTable: "Project",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Commentary_Project_ProjectId",
                table: "Commentary");

            migrationBuilder.DropForeignKey(
                name: "FK_InstructionTag_Project_ProjectId",
                table: "InstructionTag");

            migrationBuilder.DropForeignKey(
                name: "FK_Project_UserProfile_UserProfileId",
                table: "Project");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectNew_Project_ProjectId",
                table: "ProjectNew");

            migrationBuilder.DropForeignKey(
                name: "FK_Step_Project_ProjectId",
                table: "Step");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Project",
                table: "Project");

            migrationBuilder.RenameTable(
                name: "Project",
                newName: "Instruction");

            migrationBuilder.RenameIndex(
                name: "IX_Project_UserProfileId",
                table: "Instruction",
                newName: "IX_Instruction_UserProfileId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Instruction",
                table: "Instruction",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Commentary_Instruction_ProjectId",
                table: "Commentary",
                column: "ProjectId",
                principalTable: "Instruction",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Instruction_UserProfile_UserProfileId",
                table: "Instruction",
                column: "UserProfileId",
                principalTable: "UserProfile",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_InstructionTag_Instruction_ProjectId",
                table: "InstructionTag",
                column: "ProjectId",
                principalTable: "Instruction",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectNew_Instruction_ProjectId",
                table: "ProjectNew",
                column: "ProjectId",
                principalTable: "Instruction",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Step_Instruction_ProjectId",
                table: "Step",
                column: "ProjectId",
                principalTable: "Instruction",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
