using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace BeautySalon.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "favors",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    favor_name = table.Column<string>(type: "text", nullable: false),
                    price = table.Column<int>(type: "integer", nullable: false),
                    duration = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_favors", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "masters",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_masters", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: true),
                    phone_number = table.Column<string>(type: "text", nullable: false),
                    password = table.Column<string>(type: "text", nullable: false),
                    role = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "work_hours",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    date = table.Column<DateOnly>(type: "date", nullable: false),
                    begin = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    end = table.Column<TimeOnly>(type: "time without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_work_hours", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "favor_master",
                columns: table => new
                {
                    favors_id = table.Column<int>(type: "integer", nullable: false),
                    masters_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_favor_master", x => new { x.favors_id, x.masters_id });
                    table.ForeignKey(
                        name: "fk_favor_master_favors_favors_id",
                        column: x => x.favors_id,
                        principalTable: "favors",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_favor_master_masters_masters_id",
                        column: x => x.masters_id,
                        principalTable: "masters",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "registers",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    user_id = table.Column<int>(type: "integer", nullable: false),
                    master_id = table.Column<int>(type: "integer", nullable: false),
                    favor_id = table.Column<int>(type: "integer", nullable: false),
                    register_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_registers", x => x.id);
                    table.ForeignKey(
                        name: "fk_registers_favors_favor_id",
                        column: x => x.favor_id,
                        principalTable: "favors",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_registers_masters_master_id",
                        column: x => x.master_id,
                        principalTable: "masters",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_registers_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "master_work_hours",
                columns: table => new
                {
                    masters_id = table.Column<int>(type: "integer", nullable: false),
                    work_hours_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_master_work_hours", x => new { x.masters_id, x.work_hours_id });
                    table.ForeignKey(
                        name: "fk_master_work_hours_masters_masters_id",
                        column: x => x.masters_id,
                        principalTable: "masters",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_master_work_hours_work_hours_work_hours_id",
                        column: x => x.work_hours_id,
                        principalTable: "work_hours",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_favor_master_masters_id",
                table: "favor_master",
                column: "masters_id");

            migrationBuilder.CreateIndex(
                name: "ix_master_work_hours_work_hours_id",
                table: "master_work_hours",
                column: "work_hours_id");

            migrationBuilder.CreateIndex(
                name: "ix_registers_favor_id",
                table: "registers",
                column: "favor_id");

            migrationBuilder.CreateIndex(
                name: "ix_registers_master_id",
                table: "registers",
                column: "master_id");

            migrationBuilder.CreateIndex(
                name: "ix_registers_user_id",
                table: "registers",
                column: "user_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "favor_master");

            migrationBuilder.DropTable(
                name: "master_work_hours");

            migrationBuilder.DropTable(
                name: "registers");

            migrationBuilder.DropTable(
                name: "work_hours");

            migrationBuilder.DropTable(
                name: "favors");

            migrationBuilder.DropTable(
                name: "masters");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
