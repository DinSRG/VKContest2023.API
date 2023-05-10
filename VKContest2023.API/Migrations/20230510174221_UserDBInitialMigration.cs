using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace VKContest2023.API.Migrations
{
    /// <inheritdoc />
    public partial class UserDBInitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "user_group",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    code = table.Column<string>(type: "text", nullable: true),
                    description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_group", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "user_state",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    code = table.Column<string>(type: "text", nullable: true),
                    description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_state", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "user",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    login = table.Column<string>(type: "text", nullable: true),
                    password = table.Column<string>(type: "text", nullable: true),
                    created_date = table.Column<DateOnly>(type: "date", nullable: false),
                    user_group_id = table.Column<int>(type: "integer", nullable: false),
                    user_state_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user", x => x.id);
                    table.ForeignKey(
                        name: "FK_user_user_group_user_group_id",
                        column: x => x.user_group_id,
                        principalTable: "user_group",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_user_user_state_user_state_id",
                        column: x => x.user_state_id,
                        principalTable: "user_state",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "user_group",
                columns: new[] { "id", "code", "description" },
                values: new object[,]
                {
                    { 1, "Admin", "Actual Administrator!" },
                    { 2, "User", "I'm only user after all..." }
                });

            migrationBuilder.InsertData(
                table: "user_state",
                columns: new[] { "id", "code", "description" },
                values: new object[,]
                {
                    { 1, "Active", "User can use and be used!" },
                    { 2, "Blocked", "Deleted user." }
                });

            migrationBuilder.InsertData(
                table: "user",
                columns: new[] { "id", "created_date", "login", "password", "user_group_id", "user_state_id" },
                values: new object[,]
                {
                    { 1, new DateOnly(2023, 5, 10), "Old Ilya", "asdfasdf", 1, 2 },
                    { 2, new DateOnly(2023, 5, 10), "Ilya", "345235234", 2, 1 },
                    { 3, new DateOnly(2023, 5, 10), "You", "345235234df", 1, 1 },
                    { 4, new DateOnly(2023, 5, 10), "UserUser", "3452f", 1, 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_user_user_group_id",
                table: "user",
                column: "user_group_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_user_state_id",
                table: "user",
                column: "user_state_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "user");

            migrationBuilder.DropTable(
                name: "user_group");

            migrationBuilder.DropTable(
                name: "user_state");
        }
    }
}
