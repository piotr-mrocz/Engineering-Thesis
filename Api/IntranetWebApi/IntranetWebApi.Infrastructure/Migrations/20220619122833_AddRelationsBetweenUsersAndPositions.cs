using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IntranetWebApi.Infrastructure.Migrations
{
    public partial class AddRelationsBetweenUsersAndPositions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Users_IdPosition",
                table: "Users",
                column: "IdPosition");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Positions_IdPosition",
                table: "Users",
                column: "IdPosition",
                principalTable: "Positions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Positions_IdPosition",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_IdPosition",
                table: "Users");
        }
    }
}
