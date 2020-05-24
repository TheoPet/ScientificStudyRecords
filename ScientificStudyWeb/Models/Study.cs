using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ScientificStudyWeb.Models
{
    public class Study
    {
        public int Id { get; set; }

        [MaxLength(50)]
        public string Name { get; set; }

        public ICollection<Task> Tasks { get; set; }

        public ICollection<TestSubject> TestSubjects { get; set; }

        public ICollection<StudyGroup> StudyGroups { get; set; }

    }
}