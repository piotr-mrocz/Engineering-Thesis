using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IntranetWebApi.Infrastructure.Migrations
{
    public partial class ChangeColumnType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "IsAcceptedBySupervisor",
                table: "RequestForLeaves",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AddColumn<int>(
                name: "DaysAbsence",
                table: "RequestForLeaves",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DaysAbsence",
                table: "RequestForLeaves");

            migrationBuilder.AlterColumn<bool>(
                name: "IsAcceptedBySupervisor",
                table: "RequestForLeaves",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);
        }
    }
}
