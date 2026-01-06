using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BeautySalon.Migrations
{
    /// <inheritdoc />
    public partial class addconstraint : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "phone_number",
                table: "users",
                type: "character varying(13)",
                maxLength: 13,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "users",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "masters",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "favor_name",
                table: "favors",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.CreateIndex(
                name: "ix_users_name_phone_number_password",
                table: "users",
                columns: new[] { "name", "phone_number", "password" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_masters_name",
                table: "masters",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_favors_favor_name",
                table: "favors",
                column: "favor_name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_users_name_phone_number_password",
                table: "users");

            migrationBuilder.DropIndex(
                name: "ix_masters_name",
                table: "masters");

            migrationBuilder.DropIndex(
                name: "ix_favors_favor_name",
                table: "favors");

            migrationBuilder.AlterColumn<string>(
                name: "phone_number",
                table: "users",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(13)",
                oldMaxLength: 13);

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "users",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "masters",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "favor_name",
                table: "favors",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50);
        }
    }
}
