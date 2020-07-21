using System.Collections.Generic;

namespace ScientificStudyWeb.DataObjects
{
    public class GroupData
    {
        public string Name { get; set; }

        public int Id { get; set; }

        public int StudyId { get; set; }

        public ICollection<BasicTestSubject> TestSubjects { get; set; }

    }
}