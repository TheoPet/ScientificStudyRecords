import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { map } from 'rxjs/operators';

import { ActivatedRoute, Router } from '@angular/router';
import { TestSubject } from '../test-subject/test-subject-view/test-subject-view.model';
import { BasicTestSubject } from '../shared/models/basic-test-subject.model';

@Injectable({ providedIn: 'root' })
export class TestSubjectService {
  constructor(
    private httpClient: HttpClient,
    private route: ActivatedRoute,
    private router: Router
  ) {}

  getTestSubject(id: number) {
    return this.httpClient
      .get<TestSubject>(`http://localhost:5000/testsubjects/${id}`)
      .pipe(
        map((data) => {
          return new TestSubject(
            data.name,
            data.surname,
            data.entryTime,
            data.comment,
            data.study,
            data.group,
            data.experiments,
            id
          );
        })
      );
  }

  getAllTestSubjects() {
    return this.httpClient.get<TestSubject[]>(
      `http://localhost:5000/testsubjects/`
    );
  }
  getTestSubjectWithFilteredExperiment(id: number, groupId: number) {
    return this.httpClient
      .get<TestSubject>(
        `http://localhost:5000/testsubjects/${id}/groups/${groupId}`
      )
      .pipe(
        map((data) => {
          return new TestSubject(
            data.name,
            data.surname,
            data.entryTime,
            data.comment,
            data.study,
            data.group,
            data.experiments,
            id
          );
        })
      );
  }
  getTestSubjectsFromSameStudy(studyId: number) {
    return this.httpClient.get<BasicTestSubject[]>(
      `http://localhost:5000/testsubjects/studies/${studyId}`
    );
  }
  getAvailableTestSubjects() {
    return this.httpClient.get<TestSubject[]>(
      'http://localhost:5000/testsubjects?simplified=true&available=true'
    );
  }

  // updateTestSubject(id: number, data: TestSubject) {
  //   data.id = id;
  //   return this.httpClient.put<any>(
  //     `http://localhost:5000/testsubjects/${id}`,
  //     data
  //   );
  // }
  editTestSubject(testSubject: TestSubject) {
    return this.httpClient.put<TestSubject>(
      `http://localhost:5000/testsubjects/${testSubject.id}`,
      testSubject
    );
  }
  assignStudyAndGroupToTestSubject(testSubject: TestSubject) {
    return this.httpClient.put<TestSubject>(
      `http://localhost:5000/testsubjects/${testSubject.id}/assignStudyAndGroup`,
      testSubject
    );
  }
  addTestSubject(data: TestSubject) {
    return this.httpClient.post<number>(
      'http://localhost:5000/testsubjects',
      data
    );
  }

  deleteTestSubject(id: number) {
    this.httpClient.delete(`http://localhost:5000/testsubject/${id}`).subscribe(
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
