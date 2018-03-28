using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace BerrasBio.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Lounge",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    NrOfSeat = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lounge", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Movie",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Info = table.Column<string>(nullable: true),
                    TimeSpan = table.Column<TimeSpan>(nullable: false),
                    Title = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movie", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Showing",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    LoungeID = table.Column<int>(nullable: false),
                    MovieID = table.Column<int>(nullable: false),
                    SeatsLeft = table.Column<int>(nullable: false),
                    StartTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Showing", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Showing_Lounge_LoungeID",
                        column: x => x.LoungeID,
                        principalTable: "Lounge",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Showing_Movie_MovieID",
                        column: x => x.MovieID,
                        principalTable: "Movie",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ShowingSeat",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Booked = table.Column<bool>(nullable: false),
                    Seat = table.Column<int>(nullable: false),
                    ShowingID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShowingSeat", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ShowingSeat_Showing_ShowingID",
                        column: x => x.ShowingID,
                        principalTable: "Showing",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Showing_LoungeID",
                table: "Showing",
                column: "LoungeID");

            migrationBuilder.CreateIndex(
                name: "IX_Showing_MovieID",
                table: "Showing",
                column: "MovieID");

            migrationBuilder.CreateIndex(
                name: "IX_ShowingSeat_ShowingID",
                table: "ShowingSeat",
                column: "ShowingID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ShowingSeat");

            migrationBuilder.DropTable(
                name: "Showing");

            migrationBuilder.DropTable(
                name: "Lounge");

            migrationBuilder.DropTable(
                name: "Movie");
        }
    }
}
