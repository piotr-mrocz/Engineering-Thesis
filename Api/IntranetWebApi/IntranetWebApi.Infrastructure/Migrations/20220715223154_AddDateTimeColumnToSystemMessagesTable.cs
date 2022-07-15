using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IntranetWebApi.Infrastructure.Migrations
{
    public partial class AddDateTimeColumnToSystemMessagesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "AddedDate",
                table: "SystemMessages",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AddedDate",
                table: "SystemMessages");
        }
    }
}
