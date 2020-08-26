﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using ScientificStudyWeb.Data;

namespace ScientificStudiesRecord.Migrations
{
    [DbContext(typeof(ScientificStudiesRecordDbContext))]
    [Migration("20200824172149_UpdateUserRole")]
    partial class UpdateUserRole
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("ScientificStudyWeb.Models.Experiment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Comment")
                        .HasColumnType("character varying(256)")
                        .HasMaxLength(256);

                    b.Property<int>("GroupId")
                        .HasColumnType("integer");

                    b.Property<int>("StudyId")
                        .HasColumnType("integer");

                    b.Property<int>("TaskId")
                        .HasColumnType("integer");

                    b.Property<int>("TestSubjectId")
                        .HasColumnType("integer");

                    b.Property<string>("Time")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("TaskId");

                    b.HasIndex("TestSubjectId");

                    b.ToTable("Experiments");
                });

            modelBuilder.Entity("ScientificStudyWeb.Models.Group", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Name")
                        .HasColumnType("character varying(50)")
                        .HasMaxLength(50);

                    b.Property<int>("StudyId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("StudyId");

                    b.ToTable("Groups");
                });

            modelBuilder.Entity("ScientificStudyWeb.Models.Study", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Name")
                        .HasColumnType("character varying(50)")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Studies");
                });

            modelBuilder.Entity("ScientificStudyWeb.Models.Task", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Name")
                        .HasColumnType("character varying(50)")
                        .HasMaxLength(50);

                    b.Property<int>("StudyId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("StudyId");

                    b.ToTable("Tasks");
                });

            modelBuilder.Entity("ScientificStudyWeb.Models.TestSubject", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Comment")
                        .HasColumnType("character varying(256)")
                        .HasMaxLength(256);

                    b.Property<DateTime>("EntryTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int?>("GroupId")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("character varying(50)")
                        .HasMaxLength(50);

                    b.Property<int?>("StudyId")
                        .HasColumnType("integer");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("character varying(50)")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.HasIndex("GroupId");

                    b.HasIndex("StudyId");

                    b.ToTable("TestSubjects");
                });

            modelBuilder.Entity("ScientificStudyWeb.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<byte[]>("PasswordHash")
                        .HasColumnType("bytea");

                    b.Property<byte[]>("PaswordSalt")
                        .HasColumnType("bytea");

                    b.Property<string>("UserRole")
                        .HasColumnType("text");

                    b.Property<string>("Username")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("ScientificStudyWeb.Models.Experiment", b =>
                {
                    b.HasOne("ScientificStudyWeb.Models.Task", "Task")
                        .WithMany("Experiments")
                        .HasForeignKey("TaskId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ScientificStudyWeb.Models.TestSubject", "TestSubject")
                        .WithMany("Experiments")
                        .HasForeignKey("TestSubjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ScientificStudyWeb.Models.Group", b =>
                {
                    b.HasOne("ScientificStudyWeb.Models.Study", "Study")
                        .WithMany("Groups")
                        .HasForeignKey("StudyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ScientificStudyWeb.Models.Task", b =>
                {
                    b.HasOne("ScientificStudyWeb.Models.Study", "Study")
                        .WithMany("Tasks")
                        .HasForeignKey("StudyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ScientificStudyWeb.Models.TestSubject", b =>
                {
                    b.HasOne("ScientificStudyWeb.Models.Group", "Group")
                        .WithMany("TestSubjects")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("ScientificStudyWeb.Models.Study", "Study")
                        .WithMany("TestSubjects")
                        .HasForeignKey("StudyId")
                        .OnDelete(DeleteBehavior.SetNull);
                });
#pragma warning restore 612, 618
        }
    }
}
