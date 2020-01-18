using Microsoft.EntityFrameworkCore;
using ScientificStudyWeb.Models;

namespace ScientificStudyWeb.Data
{
    public class ScientificStudiesRecordDbContext:DbContext
    {
        public ScientificStudiesRecordDbContext(DbContextOptions<ScientificStudiesRecordDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StudyGroup>()
                .HasKey(ssg => new { ssg.StudyId, ssg.GroupId });
           
            modelBuilder.Entity<StudyGroup>()
                .HasOne(ssg => ssg.Study)
                .WithMany(s => s.StudyGroups)
                .HasForeignKey(ssg => ssg.StudyId)
                .OnDelete(DeleteBehavior.Cascade);
            
            modelBuilder.Entity<StudyGroup>()
                .HasOne(ssg => ssg.Group)
                .WithMany(g => g.StudyGroups)
                .HasForeignKey(ssg => ssg.GroupId)
                .OnDelete(DeleteBehavior.Cascade);
            
            modelBuilder.Entity<StudyTestSubject>()
                .HasKey(ssg => new { ssg.StudyId, ssg.TestSubjectId });
            
            modelBuilder.Entity<StudyTestSubject>()
                .HasOne(ssg => ssg.Study)
                .WithMany(ts => ts.StudyTestSubjects)
                .HasForeignKey(ssg => ssg.StudyId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<StudyTestSubject>()
                .HasOne(ssg => ssg.TestSubject)
                .WithMany(ts => ts.StudyTestSubjects)
                .HasForeignKey(ssg => ssg.TestSubjectId)
                .OnDelete(DeleteBehavior.Cascade);

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
            .HasForeignKey(t =>t.StudyId)
            .OnDelete(DeleteBehavior.Cascade);


        }

        public DbSet<Study> Studies {get; set;}
        public DbSet<Group> Groups {get; set;}
        public DbSet<Task> Tasks {get; set;}
        public DbSet<TestSubject> TestSubjects {get; set;}
        public DbSet<Experiment> Experiments {get; set;}
        
        public DbSet<User> Users {get; set;}

    }
}