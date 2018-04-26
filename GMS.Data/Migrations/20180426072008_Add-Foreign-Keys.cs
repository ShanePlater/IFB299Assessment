using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace GMS.Data.Migrations
{
    public partial class AddForeignKeys : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lesson_Instrument_InstrumentID",
                table: "Lesson");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Lesson",
                table: "Lesson");

            migrationBuilder.DropIndex(
                name: "IX_Lesson_TeacherId",
                table: "Lesson");

            migrationBuilder.DropColumn(
                name: "LessonId",
                table: "Lesson");

            migrationBuilder.AlterColumn<Guid>(
                name: "InstrumentID",
                table: "Lesson",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "InstrumentID",
                table: "Instrument",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Lesson",
                table: "Lesson",
                columns: new[] { "TeacherId", "StudentId", "DateTime" });

            migrationBuilder.AddForeignKey(
                name: "FK_Lesson_Instrument_InstrumentID",
                table: "Lesson",
                column: "InstrumentID",
                principalTable: "Instrument",
                principalColumn: "InstrumentID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lesson_Instrument_InstrumentID",
                table: "Lesson");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Lesson",
                table: "Lesson");

            migrationBuilder.AlterColumn<string>(
                name: "InstrumentID",
                table: "Lesson",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AddColumn<Guid>(
                name: "LessonId",
                table: "Lesson",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<string>(
                name: "InstrumentID",
                table: "Instrument",
                nullable: false,
                oldClrType: typeof(Guid));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Lesson",
                table: "Lesson",
                column: "LessonId");

            migrationBuilder.CreateIndex(
                name: "IX_Lesson_TeacherId",
                table: "Lesson",
                column: "TeacherId");

            migrationBuilder.AddForeignKey(
                name: "FK_Lesson_Instrument_InstrumentID",
                table: "Lesson",
                column: "InstrumentID",
                principalTable: "Instrument",
                principalColumn: "InstrumentID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
