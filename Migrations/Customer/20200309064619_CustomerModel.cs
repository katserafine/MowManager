using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MowManager.Migrations.Customer
{
    public partial class CustomerModel : Migration
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
                name: "Service",
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
                    table.PrimaryKey("PK_Service", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Service_Crew_CrewID",
                        column: x => x.CrewID,
                        principalTable: "Crew",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Service_Pricing_PricingID",
                        column: x => x.PricingID,
                        principalTable: "Pricing",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CustomerItems",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Active = table.Column<bool>(nullable: false),
                    ServiceID = table.Column<int>(nullable: true),
                    MailingAddress = table.Column<string>(nullable: true),
                    BillingAddress = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    ReferredByID = table.Column<int>(nullable: true),
                    Gate = table.Column<bool>(nullable: false),
                    CommunityGate = table.Column<bool>(nullable: false),
                    LotSize = table.Column<int>(nullable: false),
                    CornerLot = table.Column<bool>(nullable: false),
                    FollowUp = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerItems", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CustomerItems_CustomerItems_ReferredByID",
                        column: x => x.ReferredByID,
                        principalTable: "CustomerItems",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CustomerItems_Service_ServiceID",
                        column: x => x.ServiceID,
                        principalTable: "Service",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CustomerItems_ReferredByID",
                table: "CustomerItems",
                column: "ReferredByID");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerItems_ServiceID",
                table: "CustomerItems",
                column: "ServiceID");

            migrationBuilder.CreateIndex(
                name: "IX_Service_CrewID",
                table: "Service",
                column: "CrewID");

            migrationBuilder.CreateIndex(
                name: "IX_Service_PricingID",
                table: "Service",
                column: "PricingID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomerItems");

            migrationBuilder.DropTable(
                name: "Service");

            migrationBuilder.DropTable(
                name: "Crew");

            migrationBuilder.DropTable(
                name: "Pricing");
        }
    }
}
