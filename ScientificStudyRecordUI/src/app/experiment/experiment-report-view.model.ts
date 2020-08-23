export class ReportExperiment {
    constructor(
      public time: string,
      public comment: string,
      public testSubject: string,
      public testSubjectComment: string,
      public testSubjectEntryTime: string,
      public group: string,
      public study: string,
      public task: string
    ) {}
  }
