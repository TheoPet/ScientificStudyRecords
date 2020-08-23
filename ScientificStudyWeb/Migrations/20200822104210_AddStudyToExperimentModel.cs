using Microsoft.EntityFrameworkCore.Migrations;

namespace ScientificStudiesRecord.Migrations
{
    public partial class AddStudyToExperimentModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StudyId",
                table: "Experiments",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StudyId",
                table: "Experiments");
        }
    }
}
