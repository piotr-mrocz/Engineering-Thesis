using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IntranetWebApi.Infrastructure.Migrations
{
    public partial class ChangeColumnName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "VacationDaysEveryYear",
                table: "Users",
                newName: "VacationDaysInRequests");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "VacationDaysInRequests",
                table: "Users",
                newName: "VacationDaysEveryYear");
        }
    }
}
