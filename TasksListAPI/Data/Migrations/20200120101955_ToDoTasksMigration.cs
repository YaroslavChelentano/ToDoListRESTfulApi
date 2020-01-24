using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TasksListAPI.Data.Migrations
{
    public partial class ToDoTasksMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CustomTasks",
                columns: table => new
                {
                    Title = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Importance = table.Column<string>(nullable: true),
                    DueDate = table.Column<DateTime>(nullable: false),
                    IsCompleted = table.Column<bool>(nullable: false),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomTasks", x => x.Title);
                    table.ForeignKey(
                        name: "FK_CustomTasks_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SmartTasks",
                columns: table => new
                {
                    Title = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Importance = table.Column<string>(nullable: true),
                    DueDate = table.Column<DateTime>(nullable: false),
                    IsCompleted = table.Column<bool>(nullable: false),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SmartTasks", x => x.Title);
                    table.ForeignKey(
                        name: "FK_SmartTasks_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CustomTasks_UserId",
                table: "CustomTasks",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_SmartTasks_UserId",
                table: "SmartTasks",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomTasks");

            migrationBuilder.DropTable(
                name: "SmartTasks");
        }
    }
}
