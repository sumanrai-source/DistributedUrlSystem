using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Assigner.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class initmig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Slugs",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Value = table.Column<string>(type: "TEXT", maxLength: 10, nullable: false),
                    Status = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    AssignedAt = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Slugs", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Slugs_Value",
                table: "Slugs",
                column: "Value",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Slugs");
        }
    }
}
