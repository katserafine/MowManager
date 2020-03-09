using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MowManager.Migrations.Service
{
    public partial class ServiceModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Crew",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    NumCrewMembers = table.Column<int>(nullable: false),
                    LeadContactCell = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Crew", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Pricing",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Rate = table.Column<decimal>(nullable: false),
                    RateTaxIncluded = table.Column<decimal>(nullable: false),
                    TaxValue = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pricing", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ServiceItems",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Address = table.Column<string>(nullable: true),
                    PricingID = table.Column<int>(nullable: true),
                    Frequency = table.Column<string>(nullable: true),
                    Day = table.Column<string>(nullable: true),
                    CrewID = table.Column<int>(nullable: true),
                    Market = table.Column<string>(nullable: true),
                    AreaToMow = table.Column<string>(nullable: true),
                    BagMow = table.Column<bool>(nullable: false),
                    Restarts = table.Column<int>(nullable: false),
                    Skips = table.Column<int>(nullable: false),
                    DogRedos = table.Column<int>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false),
                    SendScheduleWeekNum = table.Column<int>(nullable: false),
                    FutureSkipWeekNum = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceItems", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ServiceItems_Crew_CrewID",
                        column: x => x.CrewID,
                        principalTable: "Crew",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ServiceItems_Pricing_PricingID",
                        column: x => x.PricingID,
                        principalTable: "Pricing",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ServiceItems_CrewID",
                table: "ServiceItems",
                column: "CrewID");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceItems_PricingID",
                table: "ServiceItems",
                column: "PricingID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ServiceItems");

            migrationBuilder.DropTable(
                name: "Crew");

            migrationBuilder.DropTable(
                name: "Pricing");
        }
    }
}
