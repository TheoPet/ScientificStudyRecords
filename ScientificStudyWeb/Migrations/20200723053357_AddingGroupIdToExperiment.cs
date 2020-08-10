using Microsoft.EntityFrameworkCore.Migrations;

namespace ScientificStudiesRecord.Migrations
{
    public partial class AddingGroupIdToExperiment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GroupId",
                table: "Experiments",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GroupId",
                table: "Experiments");
        }
    }
}
