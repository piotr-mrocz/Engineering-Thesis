using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IntranetWebApi.Infrastructure.Migrations
{
    public partial class ChangeTableRequestForLeave : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateOfAcceptance",
                table: "RequestForLeaves");

            migrationBuilder.DropColumn(
                name: "DateOfCancel",
                table: "RequestForLeaves");

            migrationBuilder.DropColumn(
                name: "IsAcceptedBySupervisor",
                table: "RequestForLeaves");

            migrationBuilder.DropColumn(
                name: "IsCancel",
                table: "RequestForLeaves");

            migrationBuilder.RenameColumn(
                name: "RejectionReason",
                table: "RequestForLeaves",
                newName: "RejectReason");

            migrationBuilder.AddColumn<DateTime>(
                name: "ActionDate",
                table: "RequestForLeaves",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "RequestForLeaves",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActionDate",
                table: "RequestForLeaves");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "RequestForLeaves");

            migrationBuilder.RenameColumn(
                name: "RejectReason",
                table: "RequestForLeaves",
                newName: "RejectionReason");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfAcceptance",
                table: "RequestForLeaves",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfCancel",
                table: "RequestForLeaves",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsAcceptedBySupervisor",
                table: "RequestForLeaves",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsCancel",
                table: "RequestForLeaves",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
