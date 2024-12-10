using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication2.Migrations
{
    /// <inheritdoc />
    public partial class MigrationName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Account",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Account", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Agency",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Num_employeer = table.Column<int>(type: "int", nullable: false),
                    Rating = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Agency__737584F7FE97EFFE", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Population = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Countrie__737584F791E89EFC", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "CommentAgency",
                columns: table => new
                {
                    Comment_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Agency = table.Column<int>(type: "int", nullable: false),
                    Comment_Text = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__CommentA__99D3E6C3F942F615", x => x.Comment_id);
                    table.ForeignKey(
                        name: "FK_CommentAgency_Agency",
                        column: x => x.Agency,
                        principalTable: "Agency",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Guide",
                columns: table => new
                {
                    Employee_code = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Agency = table.Column<int>(type: "int", nullable: false),
                    Rating = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Guide__AB80DE1982717AB3", x => x.Employee_code);
                    table.ForeignKey(
                        name: "FK_Guide_Agency",
                        column: x => x.Agency,
                        principalTable: "Agency",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Country = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.id);
                    table.ForeignKey(
                        name: "FK__Cities__Country__398D8EEE",
                        column: x => x.Country,
                        principalTable: "Countries",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tour_Plan",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    City_id = table.Column<int>(type: "int", nullable: false),
                    Rating = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Tour_Pla__737584F71D297A01", x => x.id);
                    table.ForeignKey(
                        name: "FK_Tour_Plan_Cities",
                        column: x => x.City_id,
                        principalTable: "Cities",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CommentTour",
                columns: table => new
                {
                    Comment_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tour_Plan = table.Column<int>(type: "int", nullable: false),
                    Comment_Text = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__CommentT__99D3E6C37397F80E", x => x.Comment_id);
                    table.ForeignKey(
                        name: "FK_CommentTour_Tour_Plan",
                        column: x => x.Tour_Plan,
                        principalTable: "Tour_Plan",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tour2",
                columns: table => new
                {
                    Tour_code = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tour_Plan = table.Column<int>(type: "int", nullable: false),
                    Guide_code = table.Column<int>(type: "int", nullable: false),
                    Date_time = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Tour2__EC54E201A0361FC1", x => x.Tour_code);
                    table.ForeignKey(
                        name: "FK_Tour2_Tour_Plan",
                        column: x => x.Tour_Plan,
                        principalTable: "Tour_Plan",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__Tour2__Guide_cod__5629CD9C",
                        column: x => x.Guide_code,
                        principalTable: "Guide",
                        principalColumn: "Employee_code");
                });

            migrationBuilder.CreateTable(
                name: "Users_cart",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    User_id = table.Column<int>(type: "int", nullable: false),
                    Tour_code = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Users_ca__C737CA77DB1A17A1", x => x.id);
                    table.ForeignKey(
                        name: "FK_Users_cart_Account",
                        column: x => x.User_id,
                        principalTable: "Account",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__Users_car__Tour___59FA5E80",
                        column: x => x.Tour_code,
                        principalTable: "Tour2",
                        principalColumn: "Tour_code",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cities_Country",
                table: "Cities",
                column: "Country");

            migrationBuilder.CreateIndex(
                name: "IX_CommentAgency_Agency",
                table: "CommentAgency",
                column: "Agency");

            migrationBuilder.CreateIndex(
                name: "IX_CommentTour_Tour_Plan",
                table: "CommentTour",
                column: "Tour_Plan");

            migrationBuilder.CreateIndex(
                name: "IX_Guide_Agency",
                table: "Guide",
                column: "Agency");

            migrationBuilder.CreateIndex(
                name: "IX_Tour_Plan_City_id",
                table: "Tour_Plan",
                column: "City_id");

            migrationBuilder.CreateIndex(
                name: "IX_Tour2_Guide_code",
                table: "Tour2",
                column: "Guide_code");

            migrationBuilder.CreateIndex(
                name: "IX_Tour2_Tour_Plan",
                table: "Tour2",
                column: "Tour_Plan");

            migrationBuilder.CreateIndex(
                name: "IX_Users_cart_Tour_code",
                table: "Users_cart",
                column: "Tour_code");

            migrationBuilder.CreateIndex(
                name: "IX_Users_cart_User_id",
                table: "Users_cart",
                column: "User_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CommentAgency");

            migrationBuilder.DropTable(
                name: "CommentTour");

            migrationBuilder.DropTable(
                name: "Users_cart");

            migrationBuilder.DropTable(
                name: "Account");

            migrationBuilder.DropTable(
                name: "Tour2");

            migrationBuilder.DropTable(
                name: "Tour_Plan");

            migrationBuilder.DropTable(
                name: "Guide");

            migrationBuilder.DropTable(
                name: "Cities");

            migrationBuilder.DropTable(
                name: "Agency");

            migrationBuilder.DropTable(
                name: "Countries");
        }
    }
}
