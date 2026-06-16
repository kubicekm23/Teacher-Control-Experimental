using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeacherControlWeb.Migrations
{
    /// <inheritdoc />
    public partial class memes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "MemeId",
                table: "ChatMessages",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "BingoBoards",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsWon = table.Column<bool>(type: "boolean", nullable: false),
                    WinnerId = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BingoBoards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BingoBoards_AspNetUsers_WinnerId",
                        column: x => x.WinnerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Memes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    ImageUrl = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Memes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BingoTiles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    BoardId = table.Column<Guid>(type: "uuid", nullable: false),
                    Text = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Position = table.Column<int>(type: "integer", nullable: false),
                    IsTriggered = table.Column<bool>(type: "boolean", nullable: false),
                    TriggeredByUserId = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BingoTiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BingoTiles_AspNetUsers_TriggeredByUserId",
                        column: x => x.TriggeredByUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BingoTiles_BingoBoards_BoardId",
                        column: x => x.BoardId,
                        principalTable: "BingoBoards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChatMessages_MemeId",
                table: "ChatMessages",
                column: "MemeId");

            migrationBuilder.CreateIndex(
                name: "IX_BingoBoards_WinnerId",
                table: "BingoBoards",
                column: "WinnerId");

            migrationBuilder.CreateIndex(
                name: "IX_BingoTiles_BoardId",
                table: "BingoTiles",
                column: "BoardId");

            migrationBuilder.CreateIndex(
                name: "IX_BingoTiles_TriggeredByUserId",
                table: "BingoTiles",
                column: "TriggeredByUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ChatMessages_Memes_MemeId",
                table: "ChatMessages",
                column: "MemeId",
                principalTable: "Memes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChatMessages_Memes_MemeId",
                table: "ChatMessages");

            migrationBuilder.DropTable(
                name: "BingoTiles");

            migrationBuilder.DropTable(
                name: "Memes");

            migrationBuilder.DropTable(
                name: "BingoBoards");

            migrationBuilder.DropIndex(
                name: "IX_ChatMessages_MemeId",
                table: "ChatMessages");

            migrationBuilder.DropColumn(
                name: "MemeId",
                table: "ChatMessages");
        }
    }
}
