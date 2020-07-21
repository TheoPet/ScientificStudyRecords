import { BasicTestSubject } from '../shared/models/basic-test-subject.model';

export class Group {
         constructor(
           public name: string,
           public id: number,
           public testSubjects: BasicTestSubject[],
           public studyId: number
         ) {}
       }
