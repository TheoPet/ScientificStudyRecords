using System;

namespace ScientificStudyWeb.DataObjects
{
    public class ReportExperimentData
    {

        public DateTime Time { get; set; }
        public string Comment { get; set; }

        public string TestSubject { get; set; }

        public string TestSubjectComment { get; set; }

        public DateTime TestSubjectEntryTime { get; set; }

        public string Group { get; set; }

        public string Study { get; set; }

        public string Task { get; set; }

    }
}