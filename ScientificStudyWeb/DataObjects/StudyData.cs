using System.Collections.Generic;
using ScientificStudyWeb.DataObjects;

namespace ScientificStudiesRecord.DataObjects
{
    public class StudyData
    {
        public string Name { get; set; }

        public IEnumerable<string> Tasks { get; set; }

        public IEnumerable<string> Groups { get; set; }
    }
}