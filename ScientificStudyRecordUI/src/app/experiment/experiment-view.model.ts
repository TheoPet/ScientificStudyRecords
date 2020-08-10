import { BasicTask } from '../shared/models/basic-task.model';

export class Experiment {
    constructor(
      public time: string,
      public comment: string,
      public testSubjectId: number,
      public task: BasicTask,
      public groupId: number,
      public id?: number
    ) {}
  }
