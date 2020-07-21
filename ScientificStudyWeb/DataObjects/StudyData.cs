using System.Collections.Generic;

namespace ScientificStudyWeb.DataObjects
{
    public class StudyData
    {
        public string Name { get; set; }

        public ICollection<BasicData> Tasks { get; set; }

        public ICollection<BasicData> Groups { get; set; }

        public int? Id { get; set; }
    }
}