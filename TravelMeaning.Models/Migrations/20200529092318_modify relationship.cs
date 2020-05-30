using Microsoft.EntityFrameworkCore.Migrations;

namespace TravelMeaning.Models.Migrations
{
    public partial class modifyrelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FromUserType",
                table: "RelationShips");

            migrationBuilder.DropColumn(
                name: "ToUserType",
                table: "RelationShips");

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "RelationShips",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "RelationShips");

            migrationBuilder.AddColumn<int>(
                name: "FromUserType",
                table: "RelationShips",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ToUserType",
                table: "RelationShips",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
