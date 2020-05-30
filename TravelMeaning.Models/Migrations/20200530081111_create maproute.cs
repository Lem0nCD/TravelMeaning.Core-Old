using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TravelMeaning.Models.Migrations
{
    public partial class createmaproute : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RelationShips_Users_ToUserId",
                table: "RelationShips");

            migrationBuilder.CreateTable(
                name: "GuideRoutes",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: true),
                    IsRemove = table.Column<bool>(nullable: false),
                    TravelGuideId = table.Column<Guid>(nullable: false),
                    Start = table.Column<string>(nullable: true),
                    End = table.Column<string>(nullable: true),
                    MapLocation = table.Column<string>(nullable: true),
                    Waypoints1 = table.Column<string>(nullable: true),
                    Waypoints2 = table.Column<string>(nullable: true),
                    Waypoints3 = table.Column<string>(nullable: true),
                    Waypoints4 = table.Column<string>(nullable: true),
                    Waypoints5 = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GuideRoutes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GuideRoutes_TravelGuides_TravelGuideId",
                        column: x => x.TravelGuideId,
                        principalTable: "TravelGuides",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GuideRoutes_TravelGuideId",
                table: "GuideRoutes",
                column: "TravelGuideId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_RelationShips_Users_ToUserId",
                table: "RelationShips",
                column: "ToUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RelationShips_Users_ToUserId",
                table: "RelationShips");

            migrationBuilder.DropTable(
                name: "GuideRoutes");

            migrationBuilder.AddForeignKey(
                name: "FK_RelationShips_Users_ToUserId",
                table: "RelationShips",
                column: "ToUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
