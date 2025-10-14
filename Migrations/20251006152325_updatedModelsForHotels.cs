using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace viesbuciu_rezervacija_backend.Migrations
{
    /// <inheritdoc />
    public partial class updatedModelsForHotels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "SquareMeters",
                table: "Rooms",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<bool>(
                name: "Toileteries",
                table: "Rooms",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Breakfast",
                table: "Hotels",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Parking",
                table: "Hotels",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Street",
                table: "Hotels",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "StreetNumber",
                table: "Hotels",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SquareMeters",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "Toileteries",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "Breakfast",
                table: "Hotels");

            migrationBuilder.DropColumn(
                name: "Parking",
                table: "Hotels");

            migrationBuilder.DropColumn(
                name: "Street",
                table: "Hotels");

            migrationBuilder.DropColumn(
                name: "StreetNumber",
                table: "Hotels");
        }
    }
}
