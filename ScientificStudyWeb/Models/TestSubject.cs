using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ScientificStudyWeb.Models
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

        [ForeignKey("StudyId")]
        public Study Study { get; set; }

        public int? StudyId { get; set; }

        public int? GroupId { get; set; }
        
        [ForeignKey("GroupId")]
        public Group Group { get; set; }

        public ICollection<Experiment> Experiments { get; set; }

    }
}