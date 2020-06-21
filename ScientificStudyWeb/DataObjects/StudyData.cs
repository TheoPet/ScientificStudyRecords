using System.Collections.Generic;

namespace ScientificStudyWeb.DataObjects
{
    public class StudyData
    {
        public string Name { get; set; }

        public IEnumerable<string> Tasks { get; set; }

        public IEnumerable<string> Groups { get; set; }

        public int? Id { get; set; }
    }
}