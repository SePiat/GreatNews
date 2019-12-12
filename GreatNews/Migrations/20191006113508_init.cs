using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GreatNews.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "News_",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Source = table.Column<string>(nullable: true),
                    Heading = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    PositiveIndex = table.Column<int>(nullable: false),
                    Content = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_News_", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Comments_",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Comment_ = table.Column<string>(nullable: true),
                    NewsId1 = table.Column<Guid>(nullable: true),
                    NewsId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments_", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments__News__NewsId1",
                        column: x => x.NewsId1,
                        principalTable: "News_",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Users_",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Role = table.Column<string>(nullable: true),
                    newsId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users_", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users__News__newsId",
                        column: x => x.newsId,
                        principalTable: "News_",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comments__NewsId1",
                table: "Comments_",
                column: "NewsId1");

            migrationBuilder.CreateIndex(
                name: "IX_Users__newsId",
                table: "Users_",
                column: "newsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comments_");

            migrationBuilder.DropTable(
                name: "Users_");

            migrationBuilder.DropTable(
                name: "News_");
        }
    }
}
