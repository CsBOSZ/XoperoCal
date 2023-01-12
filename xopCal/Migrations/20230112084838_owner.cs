using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace xopCal.Migrations
{
    /// <inheritdoc />
    public partial class owner : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "EventCals",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "OwnerId",
                table: "EventCals",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_EventCals_OwnerId",
                table: "EventCals",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_EventCals_Users_OwnerId",
                table: "EventCals",
                column: "OwnerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EventCals_Users_OwnerId",
                table: "EventCals");

            migrationBuilder.DropIndex(
                name: "IX_EventCals_OwnerId",
                table: "EventCals");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "EventCals");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "EventCals");
        }
    }
}
