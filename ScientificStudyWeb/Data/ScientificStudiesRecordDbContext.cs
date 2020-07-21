using Microsoft.EntityFrameworkCore;
using ScientificStudyWeb.Models;

namespace ScientificStudyWeb.Data
{
    public class ScientificStudiesRecordDbContext:DbContext
    {
        public ScientificStudiesRecordDbContext(DbContextOptions options):base(options)
        {}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Task>()
                .HasMany<Experiment>(t => t.Experiments)
                .WithOne(e => e.Task)
                .HasForeignKey(e => e.TaskId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<TestSubject>()
                .HasMany<Experiment>(t => t.Experiments)
                .WithOne(e => e.TestSubject)
                .HasForeignKey(e => e.TestSubjectId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Study>()
            .HasMany<Task>(s => s.Tasks)
            .WithOne(t => t.Study)
            .HasForeignKey(t => t.StudyId)
            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Study>()
            .HasMany<TestSubject>(s => s.TestSubjects)
            .WithOne(t => t.Study)
            .HasForeignKey(t => t.StudyId)
            .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Study>()
            .HasMany<Group>(s => s.Groups)
            .WithOne(g => g.Study)
            .HasForeignKey(g => g.StudyId)
            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Study>()
            .HasIndex("Name")
            .IsUnique();

            modelBuilder.Entity<Group>()
            .HasMany<TestSubject>(s => s.TestSubjects)
            .WithOne(g => g.Group)
            .HasForeignKey(g => g.GroupId)
            .OnDelete(DeleteBehavior.SetNull);
        }

        public DbSet<Study> Studies { get; set; }
        public DbSet<Group> Groups {get; set;}
        public DbSet<Task> Tasks {get; set;}
        public DbSet<TestSubject> TestSubjects {get; set;}
        public DbSet<Experiment> Experiments {get; set;}
        
        public DbSet<User> Users {get; set;}

    }
}