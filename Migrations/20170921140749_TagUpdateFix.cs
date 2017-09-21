using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace KekStarter.Migrations
{
    public partial class TagUpdateFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Step_Project_ProjectId",
                table: "Step");

            migrationBuilder.RenameColumn(
                name: "ProjectId",
                table: "Step",
                newName: "projectId");

            migrationBuilder.RenameIndex(
                name: "IX_Step_ProjectId",
                table: "Step",
                newName: "IX_Step_projectId");

            migrationBuilder.AddForeignKey(
                name: "FK_Step_Project_projectId",
                table: "Step",
                column: "projectId",
                principalTable: "Project",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Step_Project_projectId",
                table: "Step");

            migrationBuilder.RenameColumn(
                name: "projectId",
                table: "Step",
                newName: "ProjectId");

            migrationBuilder.RenameIndex(
                name: "IX_Step_projectId",
                table: "Step",
                newName: "IX_Step_ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_Step_Project_ProjectId",
                table: "Step",
                column: "ProjectId",
                principalTable: "Project",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
