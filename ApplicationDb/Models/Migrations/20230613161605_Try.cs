using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EntityModels.Migrations
{
    /// <inheritdoc />
    public partial class Try : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:PostgresExtension:uuid-ossp", ",,");

            migrationBuilder.CreateTable(
                name: "client",
                columns: table => new
                {
                    client_id = table.Column<Guid>(type: "uuid", nullable: false),
                    first_name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    last_name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    date_of_birth = table.Column<DateOnly>(type: "date", nullable: false),
                    phone = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    passport = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    connected_company = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    adress = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_client", x => x.client_id);
                });

            migrationBuilder.CreateTable(
                name: "currency",
                columns: table => new
                {
                    currency_code = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    currency_name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_currency", x => x.currency_code);
                });

            migrationBuilder.CreateTable(
                name: "employee",
                columns: table => new
                {
                    employee_id = table.Column<Guid>(type: "uuid", nullable: false),
                    first_name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    last_name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    date_of_birth = table.Column<DateOnly>(type: "date", nullable: false),
                    phone = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    passport = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    position = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    salary = table.Column<decimal>(type: "money", nullable: false),
                    contract = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_employee", x => x.employee_id);
                });

            migrationBuilder.CreateTable(
                name: "account",
                columns: table => new
                {
                    account_id = table.Column<Guid>(type: "uuid", nullable: false),
                    owner_id = table.Column<Guid>(type: "uuid", nullable: false),
                    currency_iso = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    amount = table.Column<decimal>(type: "money", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_account", x => x.account_id);
                    table.ForeignKey(
                        name: "FK_account_client_owner_id",
                        column: x => x.owner_id,
                        principalTable: "client",
                        principalColumn: "client_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_account_currency_currency_iso",
                        column: x => x.currency_iso,
                        principalTable: "currency",
                        principalColumn: "currency_code",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_account_currency_iso",
                table: "account",
                column: "currency_iso");

            migrationBuilder.CreateIndex(
                name: "IX_account_owner_id",
                table: "account",
                column: "owner_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "account");

            migrationBuilder.DropTable(
                name: "employee");

            migrationBuilder.DropTable(
                name: "client");

            migrationBuilder.DropTable(
                name: "currency");
        }
    }
}
