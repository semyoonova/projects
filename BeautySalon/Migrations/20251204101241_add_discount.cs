using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AspLessons.Migrations
{
    /// <inheritdoc />
    public partial class add_discount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "discount",
                table: "registers",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "discount",
                table: "registers");
        }
    }
}
