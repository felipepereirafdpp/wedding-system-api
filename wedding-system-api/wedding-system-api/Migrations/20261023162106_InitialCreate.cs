using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace wedding_system_api.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "presentsModel",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NamePresent = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PricePresent = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DescriptionPresent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UrlFoto = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_presentsModel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PresentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    MercadoPagoStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MercadoPagoReferenceId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MercadoPagoPaymentId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Payments_presentsModel_PresentId",
                        column: x => x.PresentId,
                        principalTable: "presentsModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Payments_MercadoPagoPaymentId",
                table: "Payments",
                column: "MercadoPagoPaymentId",
                unique: true,
                filter: "[MercadoPagoPaymentId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_PresentId",
                table: "Payments",
                column: "PresentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "presentsModel");
        }
    }
}
