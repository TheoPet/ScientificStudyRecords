using Microsoft.EntityFrameworkCore.Migrations;

namespace ScientificStudiesRecord.Migrations
{
    public partial class GroupTSDeleteBEhaviouChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TestSubjects_Groups_GroupId",
                table: "TestSubjects");

            migrationBuilder.AddForeignKey(
                name: "FK_TestSubjects_Groups_GroupId",
                table: "TestSubjects",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TestSubjects_Groups_GroupId",
                table: "TestSubjects");

            migrationBuilder.AddForeignKey(
                name: "FK_TestSubjects_Groups_GroupId",
                table: "TestSubjects",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
