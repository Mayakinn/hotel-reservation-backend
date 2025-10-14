using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace viesbuciu_rezervacija_backend.Migrations
{
    /// <inheritdoc />
    public partial class AddedEnum : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Hotels",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "Hotels");
        }
    }
}
