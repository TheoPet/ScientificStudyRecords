namespace ScientificStudyWeb.Models
{
    public class StudyTestSubject
    {
        public int StudyId { get; set; }
        public Study Study { get; set; }

        public int TestSubjectId { get; set; }
        public TestSubject TestSubject { get; set; }
    }
}