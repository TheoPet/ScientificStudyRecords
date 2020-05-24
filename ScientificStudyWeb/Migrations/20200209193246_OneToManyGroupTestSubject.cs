using Microsoft.EntityFrameworkCore.Migrations;

namespace ScientificStudiesRecord.Migrations
{
    public partial class OneToManyGroupTestSubject : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GroupId",
                table: "TestSubjects",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_TestSubjects_GroupId",
                table: "TestSubjects",
                column: "GroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_TestSubjects_Groups_GroupId",
                table: "TestSubjects",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TestSubjects_Groups_GroupId",
                table: "TestSubjects");

            migrationBuilder.DropIndex(
                name: "IX_TestSubjects_GroupId",
                table: "TestSubjects");

            migrationBuilder.DropColumn(
                name: "GroupId",
                table: "TestSubjects");
        }
    }
}
