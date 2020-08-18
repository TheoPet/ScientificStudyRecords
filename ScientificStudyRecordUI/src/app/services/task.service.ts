import { Injectable } from '@angular/core';
import { BasicTask } from '../shared/models/basic-task.model';
import { HttpClient } from '@angular/common/http';
import { Experiment } from '../experiment/experiment-view.model';
import { ActivatedRoute, Router } from '@angular/router';
import { Task } from '../task/task.model';

@Injectable({ providedIn: 'root' })
export class TaskService {
    constructor(private httpClient: HttpClient,
                private route: ActivatedRoute,
                private router: Router) {}


    deleteTask(task: Task) {
      this.httpClient.delete(`http://localhost:5000/tasks/${task.id}`).subscribe(
        (val) => {
          console.log('DELETE call successful value returned in body', val);
        },
        (response) => {
          console.log('DELETE call in error', response);
        }
      );
      this.router.navigate(['../studies', task.study.id], { relativeTo: this.route });
    }

    getTask(id: number) {
      return this.httpClient.get<Task>(`http://localhost:5000/tasks/${id}`);
    }
}
