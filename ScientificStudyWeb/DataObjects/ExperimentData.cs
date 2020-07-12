namespace ScientificStudyWeb.DataObjects
{
    public class ExperimentData
    {
        public string Time { get; set; }
        public string Comment { get; set; }

        public int TestSubjectId { get; set; }
        
        public int TaskId { get; set; }

        public int? Id;
    }
}