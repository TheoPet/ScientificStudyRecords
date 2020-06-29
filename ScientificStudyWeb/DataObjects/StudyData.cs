using System.Collections.Generic;

namespace ScientificStudyWeb.DataObjects
{
    public class StudyData
    {
        public string Name { get; set; }

        public IEnumerable<BasicTaskData> Tasks { get; set; }

        public IEnumerable<BasicGroupData> Groups { get; set; }

        public int? Id { get; set; }
    }
}