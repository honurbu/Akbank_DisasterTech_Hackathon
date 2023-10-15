using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ATOM.Repository.Migrations
{
    /// <inheritdoc />
    public partial class test23 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAccessible",
                table: "HelpDemands");

            migrationBuilder.AddColumn<int>(
                name: "GatheringCenterId",
                table: "HelpDemands",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "HelpCenterId",
                table: "HelpDemands",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_HelpDemands_GatheringCenterId",
                table: "HelpDemands",
                column: "GatheringCenterId");

            migrationBuilder.CreateIndex(
                name: "IX_HelpDemands_HelpCenterId",
                table: "HelpDemands",
                column: "HelpCenterId");

            migrationBuilder.AddForeignKey(
                name: "FK_HelpDemands_GatheringCenters_GatheringCenterId",
                table: "HelpDemands",
                column: "GatheringCenterId",
                principalTable: "GatheringCenters",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_HelpDemands_HelpCenters_HelpCenterId",
                table: "HelpDemands",
                column: "HelpCenterId",
                principalTable: "HelpCenters",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HelpDemands_GatheringCenters_GatheringCenterId",
                table: "HelpDemands");

            migrationBuilder.DropForeignKey(
                name: "FK_HelpDemands_HelpCenters_HelpCenterId",
                table: "HelpDemands");

            migrationBuilder.DropIndex(
                name: "IX_HelpDemands_GatheringCenterId",
                table: "HelpDemands");

            migrationBuilder.DropIndex(
                name: "IX_HelpDemands_HelpCenterId",
                table: "HelpDemands");

            migrationBuilder.DropColumn(
                name: "GatheringCenterId",
                table: "HelpDemands");

            migrationBuilder.DropColumn(
                name: "HelpCenterId",
                table: "HelpDemands");

            migrationBuilder.AddColumn<bool>(
                name: "IsAccessible",
                table: "HelpDemands",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
