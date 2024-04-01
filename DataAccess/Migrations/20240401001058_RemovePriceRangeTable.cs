using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class RemovePriceRangeTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PropertyNightlyPrices_PriceRanges_PriceRangeId",
                table: "PropertyNightlyPrices");

            migrationBuilder.DropTable(
                name: "PriceRanges");

            migrationBuilder.DropIndex(
                name: "IX_PropertyNightlyPrices_PriceRangeId",
                table: "PropertyNightlyPrices");

            migrationBuilder.DropColumn(
                name: "PriceRangeId",
                table: "PropertyNightlyPrices");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "PropertyNightlyPrices",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "PropertyNightlyPrices",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "PropertyNightlyPrices");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "PropertyNightlyPrices");

            migrationBuilder.AddColumn<int>(
                name: "PriceRangeId",
                table: "PropertyNightlyPrices",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "PriceRanges",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PriceRanges", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PropertyNightlyPrices_PriceRangeId",
                table: "PropertyNightlyPrices",
                column: "PriceRangeId");

            migrationBuilder.AddForeignKey(
                name: "FK_PropertyNightlyPrices_PriceRanges_PriceRangeId",
                table: "PropertyNightlyPrices",
                column: "PriceRangeId",
                principalTable: "PriceRanges",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
