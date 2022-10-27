using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonsManager.Database.Migrations
{
    public partial class AddAddressId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "AddressId",
                table: "People",
                type: "uniqueidentifier",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AddressId",
                table: "People");
        }
    }
}
