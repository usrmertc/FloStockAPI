using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FloAPI.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "barcodes",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    value = table.Column<long>(type: "bigint", maxLength: 16, nullable: false),
                    number_of_decrease = table.Column<int>(type: "integer", nullable: false),
                    material_id = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_barcodes", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "records",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    count = table.Column<int>(type: "integer", nullable: false),
                    operation_type = table.Column<bool>(type: "boolean", nullable: false),
                    date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    material_id = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_records", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "materials",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    count = table.Column<int>(type: "integer", nullable: false),
                    threshold_limit = table.Column<int>(type: "integer", nullable: false),
                    RecordId = table.Column<int>(type: "integer", nullable: true),
                    BarcodeId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_materials", x => x.id);
                    table.ForeignKey(
                        name: "FK_materials_barcodes_BarcodeId",
                        column: x => x.BarcodeId,
                        principalTable: "barcodes",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_materials_records_RecordId",
                        column: x => x.RecordId,
                        principalTable: "records",
                        principalColumn: "id");
                });

            migrationBuilder.InsertData(
                table: "barcodes",
                columns: new[] { "id", "material_id", "number_of_decrease", "value" },
                values: new object[,]
                {
                    { 1, 1, 1, 1000000000000000L },
                    { 2, 2, 1, 1000000000000001L },
                    { 3, 3, 1, 1000000000000002L },
                    { 4, 4, 1, 1000000000000003L },
                    { 5, 5, 1, 1000000000000004L },
                    { 6, 5, 5, 1000000000000005L }
                });

            migrationBuilder.InsertData(
                table: "materials",
                columns: new[] { "id", "BarcodeId", "count", "name", "RecordId", "threshold_limit" },
                values: new object[,]
                {
                    { 1, null, 85, "Leather", null, 10 },
                    { 2, null, 40, "Rubber", null, 10 },
                    { 3, null, 30, "Textiles", null, 10 },
                    { 4, null, 35, "Synthetics", null, 10 },
                    { 5, null, 35, "Foam", null, 10 }
                });

            migrationBuilder.InsertData(
                table: "records",
                columns: new[] { "id", "count", "date", "material_id", "operation_type" },
                values: new object[,]
                {
                    { 1, 120, new DateTime(2024, 9, 29, 3, 37, 30, 799, DateTimeKind.Utc).AddTicks(7277), 1, true },
                    { 2, 30, new DateTime(2024, 9, 29, 3, 37, 30, 799, DateTimeKind.Utc).AddTicks(7280), 1, false },
                    { 3, 10, new DateTime(2024, 9, 29, 3, 37, 30, 799, DateTimeKind.Utc).AddTicks(7281), 1, false },
                    { 4, 5, new DateTime(2024, 9, 29, 3, 37, 30, 799, DateTimeKind.Utc).AddTicks(7282), 1, true },
                    { 5, 40, new DateTime(2024, 9, 29, 3, 37, 30, 799, DateTimeKind.Utc).AddTicks(7283), 2, true },
                    { 6, 55, new DateTime(2024, 9, 29, 3, 37, 30, 799, DateTimeKind.Utc).AddTicks(7284), 3, true },
                    { 7, 25, new DateTime(2024, 9, 29, 3, 37, 30, 799, DateTimeKind.Utc).AddTicks(7285), 3, false },
                    { 8, 55, new DateTime(2024, 9, 29, 3, 37, 30, 799, DateTimeKind.Utc).AddTicks(7286), 4, true },
                    { 9, 15, new DateTime(2024, 9, 29, 3, 37, 30, 799, DateTimeKind.Utc).AddTicks(7286), 4, true },
                    { 10, 25, new DateTime(2024, 9, 29, 3, 37, 30, 799, DateTimeKind.Utc).AddTicks(7287), 4, false },
                    { 11, 35, new DateTime(2024, 9, 29, 3, 37, 30, 799, DateTimeKind.Utc).AddTicks(7288), 5, true }
                });

            migrationBuilder.CreateIndex(
                name: "IX_materials_BarcodeId",
                table: "materials",
                column: "BarcodeId");

            migrationBuilder.CreateIndex(
                name: "IX_materials_RecordId",
                table: "materials",
                column: "RecordId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "materials");

            migrationBuilder.DropTable(
                name: "barcodes");

            migrationBuilder.DropTable(
                name: "records");
        }
    }
}
