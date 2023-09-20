using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TotalSprReport.Migrations
{
    /// <inheritdoc />
    public partial class SprReport : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Material",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Material = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TipeMaterial = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Material", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Proyek",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NamaProyek = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LokasiProyek = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Proyek", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Header_SPR",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProyekId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Peminta = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TanggalMinta = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LokasiPeminta = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Header_SPR", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Header_SPR_Proyek_ProyekId",
                        column: x => x.ProyekId,
                        principalTable: "Proyek",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Detil_SPR",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdRef = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MaterialId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TanggalRencanaDiterima = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StatusDisetujui = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Detil_SPR", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Detil_SPR_Header_SPR_IdRef",
                        column: x => x.IdRef,
                        principalTable: "Header_SPR",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Detil_SPR_Material_MaterialId",
                        column: x => x.MaterialId,
                        principalTable: "Material",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Detil_SPR_IdRef",
                table: "Detil_SPR",
                column: "IdRef");

            migrationBuilder.CreateIndex(
                name: "IX_Detil_SPR_MaterialId",
                table: "Detil_SPR",
                column: "MaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_Header_SPR_ProyekId",
                table: "Header_SPR",
                column: "ProyekId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Detil_SPR");

            migrationBuilder.DropTable(
                name: "Header_SPR");

            migrationBuilder.DropTable(
                name: "Material");

            migrationBuilder.DropTable(
                name: "Proyek");
        }
    }
}
