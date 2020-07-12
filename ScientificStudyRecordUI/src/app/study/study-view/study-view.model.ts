import { BasicTask } from 'src/app/shared/models/basic-task.model';
import { BasicGroup } from 'src/app/shared/models/basic-group.model';

export class Study {

constructor(public name: string, public tasks: BasicTask[], public groups: BasicGroup[], public id?: number) {
}
}
