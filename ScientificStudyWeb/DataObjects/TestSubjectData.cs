using System;
using System.Collections.Generic;

namespace ScientificStudyWeb.DataObjects
{
    public class TestSubjectData
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string EntryTime { get; set; }
        public string Comment { get; set; }

        public BasicData Study { get; set; }

        public BasicData Group { get; set; }

        public ICollection<ExperimentData> Experiments { get; set; }

    }
}