using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ScientificStudiesRecord.Models
{
    public class TestSubject
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [MaxLength(50)]
        public string Surname { get; set; }

        [Required]
        public DateTime EntryTime { get; set; }

        [MaxLength(256)]
        public string Comment { get; set; }

        public ICollection<Experiment> Experiments { get; set; }

        public ICollection<StudyTestSubject> StudyTestSubjects { get; set; }

    }
}