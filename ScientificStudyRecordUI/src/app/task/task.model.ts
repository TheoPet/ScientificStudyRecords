import { BasicStudy } from '../shared/models/basic-study.model';
import { Experiment } from '../experiment/experiment-view.model';

export class Task {
  constructor(
    public name: string,
    public study: BasicStudy,
    public experiments: Experiment[],
    public id?: number
  ) {}
}
