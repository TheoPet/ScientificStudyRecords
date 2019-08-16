using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace ScientificStudiesRecord.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Groups",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Name = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Groups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Studies",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Name = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Studies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TestSubjects",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Surname = table.Column<string>(maxLength: 50, nullable: false),
                    EntryTime = table.Column<DateTime>(nullable: false),
                    Comment = table.Column<string>(maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestSubjects", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tasks",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Name = table.Column<string>(maxLength: 50, nullable: true),
                    StudyId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tasks_Studies_StudyId",
                        column: x => x.StudyId,
                        principalTable: "Studies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TestSubjectStudyGroup",
                columns: table => new
                {
                    TestSubjectId = table.Column<int>(nullable: false),
                    StudyGroupId = table.Column<int>(nullable: false),
                    StudyId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestSubjectStudyGroup", x => new { x.TestSubjectId, x.StudyGroupId, x.StudyId });
                    table.ForeignKey(
                        name: "FK_TestSubjectStudyGroup_Groups_StudyGroupId",
                        column: x => x.StudyGroupId,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TestSubjectStudyGroup_Studies_StudyId",
                        column: x => x.StudyId,
                        principalTable: "Studies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TestSubjectStudyGroup_TestSubjects_TestSubjectId",
                        column: x => x.TestSubjectId,
                        principalTable: "TestSubjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Experiments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Duration = table.Column<long>(nullable: false),
                    Comment = table.Column<string>(maxLength: 256, nullable: true),
                    TestSubjectId = table.Column<int>(nullable: true),
                    TaskId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Experiments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Experiments_Tasks_TaskId",
                        column: x => x.TaskId,
                        principalTable: "Tasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Experiments_TestSubjects_TestSubjectId",
                        column: x => x.TestSubjectId,
                        principalTable: "TestSubjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Experiments_TaskId",
                table: "Experiments",
                column: "TaskId");

            migrationBuilder.CreateIndex(
                name: "IX_Experiments_TestSubjectId",
                table: "Experiments",
                column: "TestSubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_StudyId",
                table: "Tasks",
                column: "StudyId");

            migrationBuilder.CreateIndex(
                name: "IX_TestSubjectStudyGroup_StudyGroupId",
                table: "TestSubjectStudyGroup",
                column: "StudyGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_TestSubjectStudyGroup_StudyId",
                table: "TestSubjectStudyGroup",
                column: "StudyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Experiments");

            migrationBuilder.DropTable(
                name: "TestSubjectStudyGroup");

            migrationBuilder.DropTable(
                name: "Tasks");

            migrationBuilder.DropTable(
                name: "Groups");

            migrationBuilder.DropTable(
                name: "TestSubjects");

            migrationBuilder.DropTable(
                name: "Studies");
        }
    }
}
