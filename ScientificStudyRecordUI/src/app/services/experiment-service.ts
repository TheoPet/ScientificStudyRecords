import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { ActivatedRoute, Router } from '@angular/router';
import { Experiment } from '../experiment/experiment-view.model';
import { ReportExperiment } from '../experiment/experiment-report-view.model';

@Injectable({ providedIn: 'root' })
export class ExperimentService {
  constructor(
    private httpClient: HttpClient,
    private route: ActivatedRoute,
    private router: Router
  ) {}

  export() {
    return this.httpClient.get(
      'http://localhost:5000/experiments/export/study',
      {
        responseType: 'blob',
      }
    );
  }
  exportReportFilteredByStudy(studyId: number, studyName: string) {
    return this.httpClient.get(
      `http://localhost:5000/experiments/export/study/${studyId}`,
      {
        responseType: 'blob',
        params: new HttpParams().set('studyName', studyName),
      }
    );
  }

  exportReportFilteredByGroup(groupId: number, groupName: string) {
    return this.httpClient.get(
      `http://localhost:5000/experiments/export/group/${groupId}`,
      {
        responseType: 'blob',
        params: new HttpParams().set('groupName', groupName),
      }
    );
  }
  exportReportFilteredByTestSubject(
    testSubjectId: number,
    testSubjectName: string
  ) {
    return this.httpClient.get(
      `http://localhost:5000/experiments/export/testsubject/${testSubjectId}`,
      {
        responseType: 'blob',
        params: new HttpParams().set('testSubjectName', testSubjectName),
      }
    );
  }
  saveExperiment(data: Experiment) {
    return this.httpClient.post('http://localhost:5000/experiments', data);
  }

  updateExperiment(data: Experiment) {
    return this.httpClient.put<Experiment>(
      'http://localhost:5000/experiments',
      data
    );
  }

  getExperiment(id: number) {
    return this.httpClient.get<Experiment>(
      `http://localhost:5000/experiments/${id}`
    );
  }

  deleteExperiment(experiment: Experiment) {
    this.httpClient
      .delete(`http://localhost:5000/experiments/${experiment.id}`)
      .subscribe();
    this.router.navigate(
      [
        '../testsubjects',
        experiment.testSubject.id,
        'groups',
        experiment.groupId,
      ],
      { relativeTo: this.route }
    );
  }

  getFilteredExperimentsByStudy(
    filterId: number,
    pageSize: number,
    pageNumber: number
  ) {
    return this.httpClient.get<ReportExperiment[]>(
      `http://localhost:5000/experiments/filtered/studies/${filterId}`,
      {
        observe: 'response',
        params: new HttpParams()
          .set('pageSize', pageSize.toString())
          .set('pageNumber', pageNumber.toString()),
      }
    );
  }

  getFilteredExperimentsByGroup(
    filterId: number,
    pageSize: number,
    pageNumber: number
  ) {
    return this.httpClient.get<ReportExperiment[]>(
      `http://localhost:5000/experiments/filtered/groups/${filterId}`,
      {
        observe: 'response',
        params: new HttpParams()
          .set('pageSize', pageSize.toString())
          .set('pageNumber', pageNumber.toString()),
      }
    );
  }

  getFilteredExperimentsByTestSubject(
    filterId: number,
    pageSize: number,
    pageNumber: number
  ) {
    return this.httpClient.get<ReportExperiment[]>(
      `http://localhost:5000/experiments/filtered/testsubjects/${filterId}`,
      {
        observe: 'response',
        params: new HttpParams()
          .set('pageSize', pageSize.toString())
          .set('pageNumber', pageNumber.toString()),
      }
    );
  }
}
