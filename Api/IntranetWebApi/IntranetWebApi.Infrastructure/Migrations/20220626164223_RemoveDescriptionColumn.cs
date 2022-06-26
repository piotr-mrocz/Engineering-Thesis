using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IntranetWebApi.Infrastructure.Migrations
{
    internal class _20220626164223_RemoveDescriptionColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Photos");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
               name: "Description",
               table: "Photos",
               type: "nvarchar(max)",
               nullable: false,
               defaultValue: "");
        }
    }
}
