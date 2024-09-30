using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DA.Migrations
{
    /// <inheritdoc />
    public partial class pilotageEmails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "CategorizedEmails",
                type: "nvarchar(21)",
                maxLength: 21,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "PilotageInfoId",
                table: "CategorizedEmails",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ContactDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Company = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactDetails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pilotage",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Eta = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DepartureTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EmbarkationPoint = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DisembarkationPoint = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pilotage", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Speed",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<double>(type: "float", nullable: true),
                    Rpm = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Speed", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Vessel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CallSign = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImoNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GrossTonnage = table.Column<int>(type: "int", nullable: true),
                    Length = table.Column<double>(type: "float", nullable: true),
                    Width = table.Column<double>(type: "float", nullable: true),
                    Draught = table.Column<double>(type: "float", nullable: true),
                    SpeedId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vessel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vessel_Speed_SpeedId",
                        column: x => x.SpeedId,
                        principalTable: "Speed",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PilotageInfo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VesselId = table.Column<int>(type: "int", nullable: false),
                    Cargo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PilotageId = table.Column<int>(type: "int", nullable: false),
                    FaultsOrDeficiencies = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContactDetailsId = table.Column<int>(type: "int", nullable: false),
                    PaymentInfo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AdditionalInfo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HelcomNotificationRequired = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PilotageInfo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PilotageInfo_ContactDetails_ContactDetailsId",
                        column: x => x.ContactDetailsId,
                        principalTable: "ContactDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PilotageInfo_Pilotage_PilotageId",
                        column: x => x.PilotageId,
                        principalTable: "Pilotage",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PilotageInfo_Vessel_VesselId",
                        column: x => x.VesselId,
                        principalTable: "Vessel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CategorizedEmails_PilotageInfoId",
                table: "CategorizedEmails",
                column: "PilotageInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_PilotageInfo_ContactDetailsId",
                table: "PilotageInfo",
                column: "ContactDetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_PilotageInfo_PilotageId",
                table: "PilotageInfo",
                column: "PilotageId");

            migrationBuilder.CreateIndex(
                name: "IX_PilotageInfo_VesselId",
                table: "PilotageInfo",
                column: "VesselId");

            migrationBuilder.CreateIndex(
                name: "IX_Vessel_SpeedId",
                table: "Vessel",
                column: "SpeedId");

            migrationBuilder.AddForeignKey(
                name: "FK_CategorizedEmails_PilotageInfo_PilotageInfoId",
                table: "CategorizedEmails",
                column: "PilotageInfoId",
                principalTable: "PilotageInfo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CategorizedEmails_PilotageInfo_PilotageInfoId",
                table: "CategorizedEmails");

            migrationBuilder.DropTable(
                name: "PilotageInfo");

            migrationBuilder.DropTable(
                name: "ContactDetails");

            migrationBuilder.DropTable(
                name: "Pilotage");

            migrationBuilder.DropTable(
                name: "Vessel");

            migrationBuilder.DropTable(
                name: "Speed");

            migrationBuilder.DropIndex(
                name: "IX_CategorizedEmails_PilotageInfoId",
                table: "CategorizedEmails");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "CategorizedEmails");

            migrationBuilder.DropColumn(
                name: "PilotageInfoId",
                table: "CategorizedEmails");
        }
    }
}
