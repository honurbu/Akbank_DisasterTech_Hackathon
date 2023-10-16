using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ATOM.Repository.Migrations
{
    /// <inheritdoc />
    public partial class wreckpop_IsClaimed_added : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsClaimed",
                table: "WreckPopulations",
                type: "bit",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsClaimed",
                table: "WreckPopulations");
        }
    }
}
