import { BasicTask } from '../shared/models/basic-task.model';
import { BasicTestSubject } from '../shared/models/basic-test-subject.model';

export class Experiment {
    constructor(
      public time: string,
      public comment: string,
      public testSubject: BasicTestSubject,
      public task: BasicTask,
      public groupId: number,
      public studyId: number,
      public id?: number
    ) {}
  }
