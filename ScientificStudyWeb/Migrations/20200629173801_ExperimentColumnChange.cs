using Microsoft.EntityFrameworkCore.Migrations;

namespace ScientificStudiesRecord.Migrations
{
    public partial class ExperimentColumnChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Duration",
                table: "Experiments");

            migrationBuilder.AddColumn<string>(
                name: "Time",
                table: "Experiments",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Time",
                table: "Experiments");

            migrationBuilder.AddColumn<long>(
                name: "Duration",
                table: "Experiments",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }
    }
}
