using System.Collections.Generic;

namespace ScientificStudyWeb.DataObjects
{
    public class TaskData
    {
        public string Name { get; set; }
        public BasicData Study { get; set; }

        public ICollection<ExperimentData> Experiments { get; set; }
        
        public int? Id { get; set; }
    }
}