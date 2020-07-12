import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { map } from 'rxjs/operators';

import { Study } from '../study/study-view/study-view.model';
import { ActivatedRoute, Router } from '@angular/router';
import { BasicStudy } from '../shared/models/basic-study.model';
import { BasicGroup } from '../shared/models/basic-group.model';

@Injectable({ providedIn: 'root' })
export class StudyService {
  private Study = new Study(' ', [], []);
  constructor(
    private httpClient: HttpClient,
    private route: ActivatedRoute,
    private router: Router
  ) {}

  updateStudy(id: number, study: Study) {
    return this.httpClient.put<number>(
      `http://localhost:5000/studies/${id}`,
      study
    );
  }

  addStudy(study: Study) {
    return this.httpClient.post<number>('http://localhost:5000/studies', study);
  }

  getStudy(id: number) {
    return this.httpClient
      .get<Study>(`http://localhost:5000/studies/${id}`)
      .pipe(
        map((data) => {
          return new Study(data.name, data.tasks, data.groups, id);
        })
      );
  }

  getStudies() {
    return this.httpClient.get<Study[]>('http://localhost:5000/studies/');
  }

  getStudiesLookup() {
    return this.httpClient.get<BasicStudy[]>(
      'http://localhost:5000/studies?simplified=true'
    );
  }

  getGroupsLookup(id: number) {
    return this.httpClient.get<BasicGroup[]>(
      `http://localhost:5000/studies/${id}/groups`
    );
  }

  deleteGroupOrTask(studyId: number, id: number, deleteGroup = false) {
    if (deleteGroup) {
      return this.httpClient.delete(`http://localhost:5000/studies/${studyId}/${id}?deleteGroup=true`);
    } else {
      return this.httpClient.delete(`http://localhost:5000/studies/${studyId}/${id}`);
    }
  }
  deleteStudy(id: number) {
    this.httpClient.delete(`http://localhost:5000/studies/${id}`).subscribe(
      (val) => {
        console.log('DELETE call successful value returned in body', val);
      },
      (response) => {
        console.log('DELETE call in error', response);
      }
    );
    this.router.navigate(['../'], { relativeTo: this.route });
  }
}
