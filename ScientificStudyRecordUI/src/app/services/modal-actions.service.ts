import { Injectable } from '@angular/core';
import { StudyService } from './study.service';
import { Study } from '../study/study-view/study-view.model';
import { Group } from '../group/group.model';
import { GroupService } from './group.service';
import { TestSubject } from '../test-subject/test-subject-view/test-subject-view.model';
import { TestSubjectService } from './test-subject.service';
import { Experiment } from '../experiment/experiment-view.model';
import { ExperimentService } from './experiment-service';
import { TaskService } from './task.service';
import { Task } from '../task/task.model';

@Injectable({
  providedIn: 'root'
})
export class ModalActionsService {

  constructor(private studyService: StudyService,
              private groupService: GroupService,
              private taskService: TaskService,
              private testSubjectService: TestSubjectService,
              private experimentService: ExperimentService) { }

  modalAction(modalData: any) {
    switch (modalData.deleteMethodName) {
      case 'deleteStudy':
        this.deleteStudy(modalData.data);
        break;
      case 'deleteGroup':
        this.deleteGroup(modalData.data);
        break;
      case 'deleteTestSubject':
        this.deleteTestSubject(modalData.data);
        break;
        case 'deleteExperiment':
        this.deleteExperiment(modalData.data);
        break;
        case 'deleteTask':
        this.deleteTask(modalData.data);
        break;
      default:
        break;
    }
  }

  private deleteStudy(study: Study) {
    this.studyService.deleteStudy(study.id);
  }

  private deleteGroup(group: Group) {
    this.groupService.deleteGroup(group.id);
  }

  private deleteTestSubject(testSubject: TestSubject) {
    this.testSubjectService.deleteTestSubject(testSubject.id);

  }

  private deleteExperiment(experiment: Experiment) {
    this.experimentService.deleteExperiment(experiment);

  }

  private deleteTask(task: Task) {
    this.taskService.deleteTask(task);
  }
}
