using System.Collections.Generic;

namespace ScientificStudyWeb.DataObjects
{
    public class TaskData
    {
        public string Name { get; set; }
        public BasicData Study { get; set; }

        //Teo: Da li je ovo potrebno ili ce se vezati sa TaskId-jem iz exp
        public ICollection<ExperimentData> Experiments { get; set; }
        
        public int? Id { get; set; }
    }
}