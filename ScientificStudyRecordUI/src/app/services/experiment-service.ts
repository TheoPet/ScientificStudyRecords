import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ActivatedRoute, Router } from '@angular/router';
import { Experiment } from '../experiment/experiment-view.model';
import { map } from 'rxjs/operators';

@Injectable({ providedIn: 'root' })
export class ExperimentService {
  constructor(
    private httpClient: HttpClient,
    private route: ActivatedRoute,
    private router: Router
  ) {}

  getExperiments(taskId: number) {
    return this.httpClient.get<Experiment[]>(`http://localhost:5000/experiments/${taskId}`);
  }

}
