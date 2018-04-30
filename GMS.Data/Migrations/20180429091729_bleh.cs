using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace GMS.Data.Migrations
{
    public partial class bleh : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_InstumentType_InstumentTypeType",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_InstumentType_AspNetUsers_AppUserId",
                table: "InstumentType");

            migrationBuilder.DropPrimaryKey(
                name: "PK_InstumentType",
                table: "InstumentType");

            migrationBuilder.DropIndex(
                name: "IX_InstumentType_AppUserId",
                table: "InstumentType");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_InstumentTypeType",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "InstumentType");

            migrationBuilder.DropColumn(
                name: "InstumentTypeType",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "InstumentType",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddUniqueConstraint(
                name: "AK_InstumentType_Type",
                table: "InstumentType",
                column: "Type");

            migrationBuilder.AddPrimaryKey(
                name: "PK_InstumentType",
                table: "InstumentType",
                columns: new[] { "Type", "UserId" });

            migrationBuilder.CreateIndex(
                name: "IX_InstumentType_UserId",
                table: "InstumentType",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_InstumentType_AspNetUsers_UserId",
                table: "InstumentType",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InstumentType_AspNetUsers_UserId",
                table: "InstumentType");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_InstumentType_Type",
                table: "InstumentType");

            migrationBuilder.DropPrimaryKey(
                name: "PK_InstumentType",
                table: "InstumentType");

            migrationBuilder.DropIndex(
                name: "IX_InstumentType_UserId",
                table: "InstumentType");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "InstumentType");

            migrationBuilder.AddColumn<Guid>(
                name: "AppUserId",
                table: "InstumentType",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InstumentTypeType",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_InstumentType",
                table: "InstumentType",
                column: "Type");

            migrationBuilder.CreateIndex(
                name: "IX_InstumentType_AppUserId",
                table: "InstumentType",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_InstumentTypeType",
                table: "AspNetUsers",
                column: "InstumentTypeType");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_InstumentType_InstumentTypeType",
                table: "AspNetUsers",
                column: "InstumentTypeType",
                principalTable: "InstumentType",
                principalColumn: "Type",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_InstumentType_AspNetUsers_AppUserId",
                table: "InstumentType",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
