using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace ScientificStudiesRecord.Models
{
    public class Task
    {
        public int Id {get; set;}
        
        [MaxLength(50)]
        public string Name {get; set;}

        public Study Study {get; set;}

         public ICollection<Experiment> Experiments {get; set;}
    }
}