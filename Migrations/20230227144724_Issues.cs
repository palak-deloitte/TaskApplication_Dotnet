using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project_HU.Migrations
{
    public partial class Issues : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Issues",
                columns: table => new
                {
                    issue_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    title = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    type = table.Column<int>(type: "int", nullable: false),
                    description = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    status = table.Column<int>(type: "int", nullable: false),
                    Projectsproject_id = table.Column<int>(type: "int", nullable: false),
                    Reporteruser_id = table.Column<int>(type: "int", nullable: false),
                    Assigneeuser_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Issues", x => x.issue_id);
                    table.ForeignKey(
                        name: "FK_Issues_Projects_Projectsproject_id",
                        column: x => x.Projectsproject_id,
                        principalTable: "Projects",
                        principalColumn: "project_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Issues_Users_Assigneeuser_id",
                        column: x => x.Assigneeuser_id,
                        principalTable: "Users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Issues_Users_Reporteruser_id",
                        column: x => x.Reporteruser_id,
                        principalTable: "Users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Issues_Assigneeuser_id",
                table: "Issues",
                column: "Assigneeuser_id");

            migrationBuilder.CreateIndex(
                name: "IX_Issues_Projectsproject_id",
                table: "Issues",
                column: "Projectsproject_id");

            migrationBuilder.CreateIndex(
                name: "IX_Issues_Reporteruser_id",
                table: "Issues",
                column: "Reporteruser_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Issues");
        }
    }
}
