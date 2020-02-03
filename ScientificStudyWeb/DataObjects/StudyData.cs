using System.Collections.Generic;

namespace ScientificStudiesRecord.DataObjects
{
    public class StudyData
    {
        public string Study { get; set; }

        public IEnumerable<string> Tasks { get; set; }

        public IEnumerable<string> Groups { get; set; }
    }
}