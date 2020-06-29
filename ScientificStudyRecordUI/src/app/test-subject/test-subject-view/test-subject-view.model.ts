import { BasicStudy } from 'src/app/shared/models/basic-study.model';
import { BasicGroup } from 'src/app/shared/models/basic-group.model';

export class TestSubject {
  constructor(
    public name: string,
    public surname: string,
    public entryTime: string,
    public comment: string,
    public study: BasicStudy,
    public group: BasicGroup,
    public id?: number
  ) {}
}
