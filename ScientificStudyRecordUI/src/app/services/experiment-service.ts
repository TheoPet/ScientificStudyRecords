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

  saveExperiment(data: Experiment) {
    return this.httpClient.post('http://localhost:5000/experiments', data);
  }

  updateExperiment(data: Experiment) {
    return this.httpClient.put('http://localhost:5000/experiments', data);
  }

  getExperiment(id: number) {
    return this.httpClient.get<Experiment>(`http://localhost:5000/experiments/${id}`);
  }

}
