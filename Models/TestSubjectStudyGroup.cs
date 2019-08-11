using System;

namespace ScientificStudiesRecord.Models
{
    public class TestSubjectStudyGroup
    {
        public int TestSubjectId {get; set;}
        public TestSubject TestSubject {get; set;}

        public int StudyGroupId {get; set;}
        public StudyGroup StudyGroup {get; set;}

        public int StudyId {get; set;}
        public Study Study{get; set;}
    }
}