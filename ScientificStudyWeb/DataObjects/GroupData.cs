using System.Collections.Generic;

namespace ScientificStudyWeb.DataObjects
{
    public class GroupData
    {
        public string Name { get; set; }

        public int Id { get; set; }

        public BasicData Study { get; set; }

        public ICollection<BasicTestSubject> TestSubjects { get; set; }

    }
}