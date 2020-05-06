using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TravelMeaning.Models.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: true),
                    IsRemove = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(200)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: true),
                    IsRemove = table.Column<bool>(nullable: false),
                    Username = table.Column<string>(type: "nvarchar(10)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    Avatar = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    Location = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    Occupation = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    UId = table.Column<int>(nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    Gender = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BindingTypeUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: true),
                    IsRemove = table.Column<bool>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    Type = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BindingTypeUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BindingTypeUsers_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Conversations",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: true),
                    IsRemove = table.Column<bool>(nullable: false),
                    User1Id = table.Column<Guid>(nullable: false),
                    User2Id = table.Column<Guid>(nullable: false),
                    IsShow = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Conversations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Conversations_Users_User1Id",
                        column: x => x.User1Id,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Conversations_Users_User2Id",
                        column: x => x.User2Id,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: true),
                    IsRemove = table.Column<bool>(nullable: false),
                    ToUserId = table.Column<Guid>(nullable: false),
                    FromUserId = table.Column<Guid>(nullable: false),
                    Content = table.Column<string>(type: "nvarchar(1000)", nullable: true),
                    Type = table.Column<int>(nullable: false),
                    IsRead = table.Column<bool>(nullable: false),
                    IsShow = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Messages_Users_FromUserId",
                        column: x => x.FromUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Messages_Users_ToUserId",
                        column: x => x.ToUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RelationShips",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: true),
                    IsRemove = table.Column<bool>(nullable: false),
                    ToUserId = table.Column<Guid>(nullable: false),
                    FromUserId = table.Column<Guid>(nullable: false),
                    ToUserType = table.Column<int>(nullable: false),
                    FromUserType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RelationShips", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RelationShips_Users_FromUserId",
                        column: x => x.FromUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RelationShips_Users_ToUserId",
                        column: x => x.ToUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TravelGuides",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: true),
                    IsRemove = table.Column<bool>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(30)", nullable: true),
                    CoverImage = table.Column<string>(type: "nvarchar(1000)", nullable: true),
                    UpVoteCount = table.Column<int>(nullable: false),
                    ViewedCount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TravelGuides", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TravelGuides_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: true),
                    IsRemove = table.Column<bool>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    RoleId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ChatMessages",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: true),
                    IsRemove = table.Column<bool>(nullable: false),
                    ConversationId = table.Column<Guid>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    Content = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    Type = table.Column<int>(nullable: false),
                    IsRead = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatMessages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChatMessages_Conversations_ConversationId",
                        column: x => x.ConversationId,
                        principalTable: "Conversations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ChatMessages_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: true),
                    IsRemove = table.Column<bool>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    TravelGuideId = table.Column<Guid>(nullable: false),
                    Content = table.Column<string>(type: "nvarchar(1000)", nullable: true),
                    UpVoteCount = table.Column<int>(nullable: false),
                    ReplyCommentId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_TravelGuides_TravelGuideId",
                        column: x => x.TravelGuideId,
                        principalTable: "TravelGuides",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Comments_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Favorites",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: true),
                    IsRemove = table.Column<bool>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    TravelGuideId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Favorites", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Favorites_TravelGuides_TravelGuideId",
                        column: x => x.TravelGuideId,
                        principalTable: "TravelGuides",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Favorites_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TravelGuideReviews",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: true),
                    IsRemove = table.Column<bool>(nullable: false),
                    TravelGuideId = table.Column<Guid>(nullable: false),
                    State = table.Column<int>(nullable: false),
                    Note = table.Column<string>(type: "nvarchar(100)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TravelGuideReviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TravelGuideReviews_TravelGuides_TravelGuideId",
                        column: x => x.TravelGuideId,
                        principalTable: "TravelGuides",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CommentReviews",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: true),
                    IsRemove = table.Column<bool>(nullable: false),
                    CommentId = table.Column<Guid>(nullable: false),
                    State = table.Column<int>(nullable: false),
                    Note = table.Column<string>(type: "nvarchar(100)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommentReviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CommentReviews_Comments_CommentId",
                        column: x => x.CommentId,
                        principalTable: "Comments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BindingTypeUsers_UserId",
                table: "BindingTypeUsers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ChatMessages_ConversationId",
                table: "ChatMessages",
                column: "ConversationId");

            migrationBuilder.CreateIndex(
                name: "IX_ChatMessages_UserId",
                table: "ChatMessages",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CommentReviews_CommentId",
                table: "CommentReviews",
                column: "CommentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Comments_TravelGuideId",
                table: "Comments",
                column: "TravelGuideId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_UserId",
                table: "Comments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Conversations_User1Id",
                table: "Conversations",
                column: "User1Id");

            migrationBuilder.CreateIndex(
                name: "IX_Conversations_User2Id",
                table: "Conversations",
                column: "User2Id");

            migrationBuilder.CreateIndex(
                name: "IX_Favorites_TravelGuideId",
                table: "Favorites",
                column: "TravelGuideId");

            migrationBuilder.CreateIndex(
                name: "IX_Favorites_UserId",
                table: "Favorites",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_FromUserId",
                table: "Messages",
                column: "FromUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_ToUserId",
                table: "Messages",
                column: "ToUserId");

            migrationBuilder.CreateIndex(
                name: "IX_RelationShips_FromUserId",
                table: "RelationShips",
                column: "FromUserId");

            migrationBuilder.CreateIndex(
                name: "IX_RelationShips_ToUserId",
                table: "RelationShips",
                column: "ToUserId");

            migrationBuilder.CreateIndex(
                name: "IX_TravelGuideReviews_TravelGuideId",
                table: "TravelGuideReviews",
                column: "TravelGuideId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TravelGuides_UserId",
                table: "TravelGuides",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleId",
                table: "UserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_UserId",
                table: "UserRoles",
                column: "UserId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BindingTypeUsers");

            migrationBuilder.DropTable(
                name: "ChatMessages");

            migrationBuilder.DropTable(
                name: "CommentReviews");

            migrationBuilder.DropTable(
                name: "Favorites");

            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DropTable(
                name: "RelationShips");

            migrationBuilder.DropTable(
                name: "TravelGuideReviews");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "Conversations");

            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "TravelGuides");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
