using System.ComponentModel.DataAnnotations;

namespace ScientificStudyWeb.Models
{
    public class Experiment 
    {
        public int Id { get; set; }

        public string Time { get; set; }
        
        [MaxLength(256)]
        public string Comment { get; set; }

        public int TestSubjectId { get; set; }
        public TestSubject TestSubject { get; set; }

        public int TaskId { get; set; }
        public Task Task { get; set; }

        public int GroupId { get; set; }

    }
}