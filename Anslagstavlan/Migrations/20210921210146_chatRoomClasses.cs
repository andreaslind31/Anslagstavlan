using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Anslagstavlan.Migrations
{
    public partial class chatRoomClasses : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ChatRoomModels",
                columns: table => new
                {
                    ChatRoomId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ChatRoomOwner = table.Column<int>(type: "int", nullable: false),
                    ChatRoomName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatRoomModels", x => x.ChatRoomId);
                });

            migrationBuilder.CreateTable(
                name: "ChatUserModels",
                columns: table => new
                {
                    ChatUserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatUserModels", x => x.ChatUserId);
                });

            migrationBuilder.CreateTable(
                name: "ChatMessageModels",
                columns: table => new
                {
                    ChatMessageId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChatRoomId = table.Column<int>(type: "int", nullable: false),
                    ChatUserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatMessageModels", x => x.ChatMessageId);
                    table.ForeignKey(
                        name: "FK_ChatMessageModels_ChatRoomModels_ChatRoomId",
                        column: x => x.ChatRoomId,
                        principalTable: "ChatRoomModels",
                        principalColumn: "ChatRoomId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChatMessageModels_ChatUserModels_ChatUserId",
                        column: x => x.ChatUserId,
                        principalTable: "ChatUserModels",
                        principalColumn: "ChatUserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChatMessageModels_ChatRoomId",
                table: "ChatMessageModels",
                column: "ChatRoomId");

            migrationBuilder.CreateIndex(
                name: "IX_ChatMessageModels_ChatUserId",
                table: "ChatMessageModels",
                column: "ChatUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChatMessageModels");

            migrationBuilder.DropTable(
                name: "ChatRoomModels");

            migrationBuilder.DropTable(
                name: "ChatUserModels");
        }
    }
}
