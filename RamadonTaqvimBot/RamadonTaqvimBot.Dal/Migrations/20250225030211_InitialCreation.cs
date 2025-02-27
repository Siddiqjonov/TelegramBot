using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RamadonTaqvimBot.Dal.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BotUser",
                columns: table => new
                {
                    BotUserId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TelegramUserId = table.Column<long>(type: "bigint", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: true),
                    Username = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    IsBlocked = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BotUser", x => x.BotUserId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BotUser_TelegramUserId",
                table: "BotUser",
                column: "TelegramUserId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BotUser");
        }
    }
}
