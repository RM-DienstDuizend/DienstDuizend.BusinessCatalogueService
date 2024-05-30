using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DienstDuizend.BusinessCatalogueService.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Businesses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    KvkNumber = table.Column<string>(type: "text", nullable: false),
                    BusinessEmail = table.Column<string>(type: "text", nullable: true),
                    WebsiteUri = table.Column<string>(type: "text", nullable: true),
                    LogoUri = table.Column<string>(type: "text", nullable: true),
                    BannerUri = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Businesses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Services",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    BusinessId = table.Column<Guid>(type: "uuid", nullable: false),
                    Price = table.Column<decimal>(type: "numeric", nullable: false),
                    EstimatedDurationInMinutes = table.Column<int>(type: "integer", nullable: false),
                    IsHomeService = table.Column<bool>(type: "boolean", nullable: false),
                    IsPubliclyVisible = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Services", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Services_Businesses_BusinessId",
                        column: x => x.BusinessId,
                        principalTable: "Businesses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Services_BusinessId",
                table: "Services",
                column: "BusinessId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Services");

            migrationBuilder.DropTable(
                name: "Businesses");
        }
    }
}
