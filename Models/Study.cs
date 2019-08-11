using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ScientificStudiesRecord.Models
{
    public class Study
    {
        public int Id {get; set;}
        
        [MaxLength(50)]
        public string Name {get; set;}

        public ICollection<Task> Tasks {get; set;}

        public ICollection<TestSubjectStudyGroup> TestSubjectStudyGroups {get; set;}

    }
}