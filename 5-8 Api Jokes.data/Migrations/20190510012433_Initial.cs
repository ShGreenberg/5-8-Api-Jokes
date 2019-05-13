using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace _5_8_Api_Jokes.data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Jokes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    DataId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SetUp = table.Column<string>(nullable: true),
                    Punchline = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jokes", x => x.DataId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UsersLikedJokes",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false),
                    JokeId = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    Liked = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersLikedJokes", x => new { x.UserId, x.JokeId });
                    table.ForeignKey(
                        name: "FK_UsersLikedJokes_Jokes_JokeId",
                        column: x => x.JokeId,
                        principalTable: "Jokes",
                        principalColumn: "DataId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UsersLikedJokes_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UsersLikedJokes_JokeId",
                table: "UsersLikedJokes",
                column: "JokeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UsersLikedJokes");

            migrationBuilder.DropTable(
                name: "Jokes");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
