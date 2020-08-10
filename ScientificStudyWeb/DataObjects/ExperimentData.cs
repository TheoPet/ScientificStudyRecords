namespace ScientificStudyWeb.DataObjects
{
    public class ExperimentData
    {
        public string Time { get; set; }
        public string Comment { get; set; }

        public int TestSubjectId { get; set; }

        public int GroupId { get; set; }
        
        public BasicData Task { get; set; }

        public int? Id { get; set; }
    }
}