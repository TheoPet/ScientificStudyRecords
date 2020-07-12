export class Experiment {
    constructor(
      public time: string,
      public comment: string,
      public testSubjectId: number,
      public taskId: number,
      public id?: number
    ) {}
  }
