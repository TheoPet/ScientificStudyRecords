using Microsoft.EntityFrameworkCore;
using ScientificStudiesRecord.Models;

namespace ScientificStudiesRecord.Data
{
    public class ScientificStudiesRecordDbContext:DbContext
    {
        public ScientificStudiesRecordDbContext(DbContextOptions<ScientificStudiesRecordDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TestSubjectStudyGroup>()
                .HasKey(ssg => new { ssg.TestSubjectId, ssg.StudyGroupId, ssg.StudyId });
           
            modelBuilder.Entity<TestSubjectStudyGroup>()
                .HasOne(ssg => ssg.Study)
                .WithMany(s => s.TestSubjectStudyGroups)
                .HasForeignKey(ssg => ssg.StudyId);
            
            modelBuilder.Entity<TestSubjectStudyGroup>()
                .HasOne(ssg => ssg.StudyGroup)
                .WithMany(g => g.TestSubjectStudyGroups)
                .HasForeignKey(ssg => ssg.StudyGroupId);
            
            modelBuilder.Entity<TestSubjectStudyGroup>()
                .HasOne(ssg => ssg.TestSubject)
                .WithMany(ts => ts.TestSubjectStudyGroups)
                .HasForeignKey(ssg => ssg.TestSubjectId);
        }

        public DbSet<Study> Studies {get; set;}
        public DbSet<StudyGroup> Groups {get; set;}
        public DbSet<Task> Tasks {get; set;}
        public DbSet<TestSubject> TestSubjects {get; set;}
        public DbSet<Experiment> Experiments {get; set;}
        //public DbSet<TestSubjectStudyGroup> TestSubjectStudyGroups {get; set;}

    }
}