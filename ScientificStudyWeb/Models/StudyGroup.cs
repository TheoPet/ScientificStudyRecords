using System;

namespace ScientificStudyWeb.Models
{
    public class StudyGroup
    {
        public int GroupId { get; set; }
        public Group Group { get; set; }

        public int StudyId { get; set; }
        public Study Study { get; set; }

    }
}