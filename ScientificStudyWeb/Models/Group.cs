using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ScientificStudyWeb.Models
{
    public class Group
    {
        public int Id { get; set; }

        public int StudyId { get; set; }

        [ForeignKey("StudyId")]
        public Study Study {get; set;}
        
        [MaxLength(50)]
        public string Name { get; set; }

        public ICollection<TestSubject> TestSubjects { get; set; }
    }
}