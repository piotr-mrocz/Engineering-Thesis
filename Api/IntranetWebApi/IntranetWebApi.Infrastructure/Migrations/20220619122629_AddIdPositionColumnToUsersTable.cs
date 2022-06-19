using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IntranetWebApi.Infrastructure.Migrations
{
    public partial class AddIdPositionColumnToUsersTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdPosition",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdPosition",
                table: "Users");
        }
    }
}
