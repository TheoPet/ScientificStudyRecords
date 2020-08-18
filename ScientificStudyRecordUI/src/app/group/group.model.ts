import { BasicTestSubject } from '../shared/models/basic-test-subject.model';
import { BasicStudy } from '../shared/models/basic-study.model';

export class Group {
         constructor(
           public name: string,
           public testSubjects: BasicTestSubject[],
           public study: BasicStudy,
           public id?: number
         ) {}
       }
