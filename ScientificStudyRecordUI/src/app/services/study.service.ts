import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { map } from 'rxjs/operators';

import { Study } from '../study/study-view/study-view.model';
import { ActivatedRoute, Router } from '@angular/router';

@Injectable()
export class StudyService {
    private Study = new Study(' ', [], []);
    constructor(private httpClient: HttpClient,
                private route: ActivatedRoute,
                private router: Router) { }

    updateStudy(id: number, study: Study) {

    }

    addStudy(study: Study) {
        this.httpClient.post<number>('http://localhost:5000/study/save',
        study)
        .subscribe (responseData => {
            this.Study = new Study(study.name, study.tasks, study.groups, responseData);
         }
        );
    }

    getStudy(id: number) {
        return this.httpClient.get<Study>(`http://localhost:5000/study/${id}`)
        .pipe(map(
            data => {
                return new Study(data.name, data.tasks, data.groups, id);
            }
        ));
    }

    deleteStudy(id: number) {
         this.httpClient.delete(`http://localhost:5000/study/${id}`)
         .subscribe(
            (val) => {
                console.log('DELETE call successful value returned in body', val);
            },
            response => {
                console.log('DELETE call in error', response);
            }
         );
         this.router.navigate(['../'], {relativeTo: this.route});
    }
}
