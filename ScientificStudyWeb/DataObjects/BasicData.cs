namespace ScientificStudyWeb.DataObjects
{
    public class BasicData
    {
        public string Name { get; set; }

        public int? Id { get; set; }

        public BasicData(string Name, int? Id)
        {
            this.Name = Name;
            if (Id != null)
                this.Id = Id;
        }

        public BasicData() {}

    }
}