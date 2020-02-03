using System;

namespace ScientificStudyWeb.DataObjects
{
    public class TestSubjectData
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime EntryTime { get; set; }
        public string Comment { get; set; }

        public string Study { get; set; }
        public string Group { get; set; }
    }
}