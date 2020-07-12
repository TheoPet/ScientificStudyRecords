using System.Collections.Generic;

namespace ScientificStudyWeb.DataObjects
{
    public class StudyData
    {
        public string Name { get; set; }

        public ICollection<BasicTaskData> Tasks { get; set; }

        public ICollection<BasicGroupData> Groups { get; set; }

        public int? Id { get; set; }
    }
}