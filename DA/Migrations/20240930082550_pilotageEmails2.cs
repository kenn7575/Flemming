using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DA.Migrations
{
    /// <inheritdoc />
    public partial class pilotageEmails2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PilotageInfo_ContactDetails_ContactDetailsId",
                table: "PilotageInfo");

            migrationBuilder.DropForeignKey(
                name: "FK_PilotageInfo_Pilotage_PilotageId",
                table: "PilotageInfo");

            migrationBuilder.DropForeignKey(
                name: "FK_PilotageInfo_Vessel_VesselId",
                table: "PilotageInfo");

            migrationBuilder.AlterColumn<int>(
                name: "VesselId",
                table: "PilotageInfo",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "PilotageId",
                table: "PilotageInfo",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "PaymentInfo",
                table: "PilotageInfo",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "FaultsOrDeficiencies",
                table: "PilotageInfo",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "ContactDetailsId",
                table: "PilotageInfo",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Cargo",
                table: "PilotageInfo",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "AdditionalInfo",
                table: "PilotageInfo",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddForeignKey(
                name: "FK_PilotageInfo_ContactDetails_ContactDetailsId",
                table: "PilotageInfo",
                column: "ContactDetailsId",
                principalTable: "ContactDetails",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PilotageInfo_Pilotage_PilotageId",
                table: "PilotageInfo",
                column: "PilotageId",
                principalTable: "Pilotage",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PilotageInfo_Vessel_VesselId",
                table: "PilotageInfo",
                column: "VesselId",
                principalTable: "Vessel",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PilotageInfo_ContactDetails_ContactDetailsId",
                table: "PilotageInfo");

            migrationBuilder.DropForeignKey(
                name: "FK_PilotageInfo_Pilotage_PilotageId",
                table: "PilotageInfo");

            migrationBuilder.DropForeignKey(
                name: "FK_PilotageInfo_Vessel_VesselId",
                table: "PilotageInfo");

            migrationBuilder.AlterColumn<int>(
                name: "VesselId",
                table: "PilotageInfo",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PilotageId",
                table: "PilotageInfo",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PaymentInfo",
                table: "PilotageInfo",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FaultsOrDeficiencies",
                table: "PilotageInfo",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ContactDetailsId",
                table: "PilotageInfo",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Cargo",
                table: "PilotageInfo",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AdditionalInfo",
                table: "PilotageInfo",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PilotageInfo_ContactDetails_ContactDetailsId",
                table: "PilotageInfo",
                column: "ContactDetailsId",
                principalTable: "ContactDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PilotageInfo_Pilotage_PilotageId",
                table: "PilotageInfo",
                column: "PilotageId",
                principalTable: "Pilotage",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PilotageInfo_Vessel_VesselId",
                table: "PilotageInfo",
                column: "VesselId",
                principalTable: "Vessel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
