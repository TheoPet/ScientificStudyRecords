using Microsoft.EntityFrameworkCore.Migrations;

namespace ScientificStudiesRecord.Migrations
{
    public partial class RemoveStudyGroupsManyToManyRel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StudyGroup");

            migrationBuilder.AddColumn<int>(
                name: "StudyId",
                table: "Groups",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Groups_StudyId",
                table: "Groups",
                column: "StudyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_Studies_StudyId",
                table: "Groups",
                column: "StudyId",
                principalTable: "Studies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Groups_Studies_StudyId",
                table: "Groups");

            migrationBuilder.DropIndex(
                name: "IX_Groups_StudyId",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "StudyId",
                table: "Groups");

            migrationBuilder.CreateTable(
                name: "StudyGroup",
                columns: table => new
                {
                    StudyId = table.Column<int>(type: "integer", nullable: false),
                    GroupId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudyGroup", x => new { x.StudyId, x.GroupId });
                    table.ForeignKey(
                        name: "FK_StudyGroup_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudyGroup_Studies_StudyId",
                        column: x => x.StudyId,
                        principalTable: "Studies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StudyGroup_GroupId",
                table: "StudyGroup",
                column: "GroupId");
        }
    }
}
