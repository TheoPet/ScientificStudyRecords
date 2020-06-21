import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { map } from 'rxjs/operators';

import { ActivatedRoute, Router } from '@angular/router';
import { TestSubject } from '../test-subject/test-subject-view/test-subject-view.model';

@Injectable()
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
            data.studyId,
            data.group,
            data.groupId,
            id
          );
        })
      );
  }

  updateTestSubject(id: number, data: TestSubject) {}

  addTestSubject(data: TestSubject) {
     return this.httpClient.post<number>('http://localhost:5000/testsubjects', data) ;
  }
}
