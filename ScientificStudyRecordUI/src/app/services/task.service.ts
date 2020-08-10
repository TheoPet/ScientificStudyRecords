import { Injectable } from '@angular/core';
import { BasicTask } from '../shared/models/basic-task.model';
import { HttpClient } from '@angular/common/http';
import { Experiment } from '../experiment/experiment-view.model';

@Injectable({ providedIn: 'root' })
export class TaskService {
    constructor(private httpClient: HttpClient) {}

    getTaskLookup(studyId: number) {
        return this.httpClient.get<BasicTask[]>(
          `http://localhost:5000/tasks/${studyId}`
        );
    }
}
