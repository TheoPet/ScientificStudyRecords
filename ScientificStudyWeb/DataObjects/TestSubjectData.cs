using System;

namespace ScientificStudyWeb.DataObjects
{
    public class TestSubjectData
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string EntryTime { get; set; }
        public string Comment { get; set; }

        public BasicStudyData Study { get; set; }

        public BasicGroupData Group { get; set; }

        //public ICollection<ExperimentData> Experiments { get; set; }

    }
}