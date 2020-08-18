using System.Collections.Generic;

namespace ScientificStudyWeb.DataObjects
{
    public class BasicTestSubject : BasicData
    {
        public string Surname { get; set; }

        public int? groupId { get; set; }

        public BasicTestSubject(string Name, string Surname, int Id, int? groupId = null) : base(Name, Id)
        {
            this.Surname = Surname;

            if (groupId != null)
                this.groupId = groupId;
        }

        public BasicTestSubject() {}
    }
}