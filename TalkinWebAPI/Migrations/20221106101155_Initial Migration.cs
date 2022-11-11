using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TalkinWebAPI.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    user_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    userName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    sex = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    firstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    lastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    dateOfBirth = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.user_ID);
                });

            migrationBuilder.CreateTable(
                name: "Message",
                columns: table => new
                {
                    message_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    sender_IDuser_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    receiver_IDuser_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    message_text = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Message", x => x.message_ID);
                    table.ForeignKey(
                        name: "FK_Message_User_receiver_IDuser_ID",
                        column: x => x.receiver_IDuser_ID,
                        principalTable: "User",
                        principalColumn: "user_ID");
                    table.ForeignKey(
                        name: "FK_Message_User_sender_IDuser_ID",
                        column: x => x.sender_IDuser_ID,
                        principalTable: "User",
                        principalColumn: "user_ID");
                });

            migrationBuilder.CreateTable(
                name: "Conversation",
                columns: table => new
                {
                    conversation_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    user1_IDuser_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    user2_IDuser_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    msg_IDmessage_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Conversation", x => x.conversation_ID);
                    table.ForeignKey(
                        name: "FK_Conversation_Message_msg_IDmessage_ID",
                        column: x => x.msg_IDmessage_ID,
                        principalTable: "Message",
                        principalColumn: "message_ID");
                    table.ForeignKey(
                        name: "FK_Conversation_User_user1_IDuser_ID",
                        column: x => x.user1_IDuser_ID,
                        principalTable: "User",
                        principalColumn: "user_ID");
                    table.ForeignKey(
                        name: "FK_Conversation_User_user2_IDuser_ID",
                        column: x => x.user2_IDuser_ID,
                        principalTable: "User",
                        principalColumn: "user_ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Conversation_msg_IDmessage_ID",
                table: "Conversation",
                column: "msg_IDmessage_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Conversation_user1_IDuser_ID",
                table: "Conversation",
                column: "user1_IDuser_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Conversation_user2_IDuser_ID",
                table: "Conversation",
                column: "user2_IDuser_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Message_receiver_IDuser_ID",
                table: "Message",
                column: "receiver_IDuser_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Message_sender_IDuser_ID",
                table: "Message",
                column: "sender_IDuser_ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Conversation");

            migrationBuilder.DropTable(
                name: "Message");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
