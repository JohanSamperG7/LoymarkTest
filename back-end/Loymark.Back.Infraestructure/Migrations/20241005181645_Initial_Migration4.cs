using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Loymark.Back.Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial_Migration4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Activity_User_UserId",
                schema: "dbo",
                table: "Activity");

            migrationBuilder.AddForeignKey(
                name: "FK_Activity_User_UserId",
                schema: "dbo",
                table: "Activity",
                column: "UserId",
                principalSchema: "dbo",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Activity_User_UserId",
                schema: "dbo",
                table: "Activity");

            migrationBuilder.AddForeignKey(
                name: "FK_Activity_User_UserId",
                schema: "dbo",
                table: "Activity",
                column: "UserId",
                principalSchema: "dbo",
                principalTable: "User",
                principalColumn: "Id");
        }
    }
}
