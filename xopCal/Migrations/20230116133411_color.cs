using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace xopCal.Migrations
{
    /// <inheritdoc />
    public partial class color : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Color",
                table: "Users",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Color",
                table: "EventCals",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "EventCalUser",
                columns: table => new
                {
                    SubscribeEventCalsId = table.Column<int>(type: "integer", nullable: false),
                    SubscribersId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventCalUser", x => new { x.SubscribeEventCalsId, x.SubscribersId });
                    table.ForeignKey(
                        name: "FK_EventCalUser_EventCals_SubscribeEventCalsId",
                        column: x => x.SubscribeEventCalsId,
                        principalTable: "EventCals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EventCalUser_Users_SubscribersId",
                        column: x => x.SubscribersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EventCalUser_SubscribersId",
                table: "EventCalUser",
                column: "SubscribersId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EventCalUser");

            migrationBuilder.DropColumn(
                name: "Color",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Color",
                table: "EventCals");
        }
    }
}
