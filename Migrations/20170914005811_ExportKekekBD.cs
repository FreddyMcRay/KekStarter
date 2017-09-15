using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace KekStarter.Migrations
{
    public partial class ExportKekekBD : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserProfile_UserRole_UserRoleId",
                table: "UserProfile");

            migrationBuilder.DropTable(
                name: "UserLike");

            migrationBuilder.DropTable(
                name: "UserRole");

            migrationBuilder.DropIndex(
                name: "IX_UserProfile_UserRoleId",
                table: "UserProfile");

            migrationBuilder.DropColumn(
                name: "AboutMySelf",
                table: "UserProfile");

            migrationBuilder.DropColumn(
                name: "City",
                table: "UserProfile");

            migrationBuilder.DropColumn(
                name: "Country",
                table: "UserProfile");

            migrationBuilder.DropColumn(
                name: "DataOfBirth",
                table: "UserProfile");

            migrationBuilder.DropColumn(
                name: "Rating",
                table: "UserProfile");

            migrationBuilder.DropColumn(
                name: "UserRoleId",
                table: "UserProfile");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Instruction");

            migrationBuilder.AddColumn<int>(
                name: "Sponsors",
                table: "Instruction",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Instruction",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Sponsors",
                table: "Instruction");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Instruction");

            migrationBuilder.AddColumn<string>(
                name: "AboutMySelf",
                table: "UserProfile",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "UserProfile",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "UserProfile",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DataOfBirth",
                table: "UserProfile",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Rating",
                table: "UserProfile",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserRoleId",
                table: "UserProfile",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Instruction",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "UserLike",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ProjectId = table.Column<int>(nullable: false),
                    UserProfileId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLike", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserLike_Instruction_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Instruction",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserLike_UserProfile_UserProfileId",
                        column: x => x.UserProfileId,
                        principalTable: "UserProfile",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserRole",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Role = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRole", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserProfile_UserRoleId",
                table: "UserProfile",
                column: "UserRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserLike_ProjectId",
                table: "UserLike",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_UserLike_UserProfileId",
                table: "UserLike",
                column: "UserProfileId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserProfile_UserRole_UserRoleId",
                table: "UserProfile",
                column: "UserRoleId",
                principalTable: "UserRole",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
