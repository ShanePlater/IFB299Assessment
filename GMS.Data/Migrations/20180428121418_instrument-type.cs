using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace GMS.Data.Migrations
{
    public partial class instrumenttype : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "InstumentTypeType",
                table: "AspNetUsers",
                nullable: true);

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_InstumentType_InstumentTypeType",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_InstumentTypeType",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "InstumentTypeType",
                table: "AspNetUsers");
        }
    }
}
