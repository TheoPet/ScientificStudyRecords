﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using ScientificStudiesRecord.Data;

namespace ScientificStudiesRecord.Migrations
{
    [DbContext(typeof(ScientificStudiesRecordDbContext))]
    [Migration("20190810211608_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("ScientificStudiesRecord.Models.Experiment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Comment")
                        .HasMaxLength(256);

                    b.Property<long>("Duration");

                    b.Property<int?>("TaskId");

                    b.Property<int?>("TestSubjectId");

                    b.HasKey("Id");

                    b.HasIndex("TaskId");

                    b.HasIndex("TestSubjectId");

                    b.ToTable("Experiments");
                });

            modelBuilder.Entity("ScientificStudiesRecord.Models.Study", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("Studies");
                });

            modelBuilder.Entity("ScientificStudiesRecord.Models.StudyGroup", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("Groups");
                });

            modelBuilder.Entity("ScientificStudiesRecord.Models.Task", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .HasMaxLength(50);

                    b.Property<int?>("StudyId");

                    b.HasKey("Id");

                    b.HasIndex("StudyId");

                    b.ToTable("Tasks");
                });

            modelBuilder.Entity("ScientificStudiesRecord.Models.TestSubject", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Comment")
                        .HasMaxLength(256);

                    b.Property<DateTime>("EntryTime");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("TestSubjects");
                });

            modelBuilder.Entity("ScientificStudiesRecord.Models.TestSubjectStudyGroup", b =>
                {
                    b.Property<int>("TestSubjectId");

                    b.Property<int>("StudyGroupId");

                    b.Property<int>("StudyId");

                    b.HasKey("TestSubjectId", "StudyGroupId", "StudyId");

                    b.HasIndex("StudyGroupId");

                    b.HasIndex("StudyId");

                    b.ToTable("TestSubjectStudyGroup");
                });

            modelBuilder.Entity("ScientificStudiesRecord.Models.Experiment", b =>
                {
                    b.HasOne("ScientificStudiesRecord.Models.Task", "Task")
                        .WithMany("Experiments")
                        .HasForeignKey("TaskId");

                    b.HasOne("ScientificStudiesRecord.Models.TestSubject", "TestSubject")
                        .WithMany("Experiments")
                        .HasForeignKey("TestSubjectId");
                });

            modelBuilder.Entity("ScientificStudiesRecord.Models.Task", b =>
                {
                    b.HasOne("ScientificStudiesRecord.Models.Study", "Study")
                        .WithMany("Tasks")
                        .HasForeignKey("StudyId");
                });

            modelBuilder.Entity("ScientificStudiesRecord.Models.TestSubjectStudyGroup", b =>
                {
                    b.HasOne("ScientificStudiesRecord.Models.StudyGroup", "StudyGroup")
                        .WithMany("TestSubjectStudyGroups")
                        .HasForeignKey("StudyGroupId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ScientificStudiesRecord.Models.Study", "Study")
                        .WithMany("TestSubjectStudyGroups")
                        .HasForeignKey("StudyId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ScientificStudiesRecord.Models.TestSubject", "TestSubject")
                        .WithMany("TestSubjectStudyGroups")
                        .HasForeignKey("TestSubjectId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
