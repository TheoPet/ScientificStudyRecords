import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { map } from 'rxjs/operators';

import { ActivatedRoute, Router } from '@angular/router';
import { TestSubject } from '../test-subject/test-subject-view/test-subject-view.model';

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
            id
          );
        })
      );
  }

  updateTestSubject(id: number, data: TestSubject) {
    data.id = id;
    return this.httpClient.put<any>(
      `http://localhost:5000/testsubjects/${id}`,
      data
    );
  }

  addTestSubject(data: TestSubject) {
    return this.httpClient.post<number>(
      'http://localhost:5000/testsubjects',
      data
    );
  }

  getAvailableTestSubjects() {
    return this.httpClient.get<TestSubject[]>(
      'http://localhost:5000/testsubjects?simplified=true&available=true'
    );
  }
}
