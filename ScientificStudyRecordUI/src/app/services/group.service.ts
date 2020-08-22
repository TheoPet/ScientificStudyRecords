import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Group } from '../group/group.model';
import { map } from 'rxjs/operators';
import { TestSubject } from '../test-subject/test-subject-view/test-subject-view.model';
import { ActivatedRoute, Router } from '@angular/router';
import { BasicGroup } from '../shared/models/basic-group.model';

@Injectable({providedIn: 'root'})
export class GroupService {

    constructor(private httpClient: HttpClient,
                private route: ActivatedRoute,
                private router: Router) {

    }
    getAllGroups() {
        return this.httpClient.get<Group[]>(`http://localhost:5000/groups`);
    }
    getGroup(id: number) {
        return this.httpClient.get<Group>(`http://localhost:5000/groups/${id}`).
        pipe(
            map((data) => {
                return new Group(
                    data.name,
                    data.testSubjects,
                    data.study,
                    id
                );
            })
        );
    }

    getGroupsFiltered(
        pageNumber: number = 1,
        pageSize: number = 5,
        filter: string = ''
      ) {
        return this.httpClient
          .get<BasicGroup[]>('http://localhost:5000/groups/filtered', {
            observe: 'response',
            params: new HttpParams()
              .set('pageSize', pageSize.toString())
              .set('pageNumber', pageNumber.toString())
              .set('searchTerm', filter),
          });
      }

    editGroup(group: Group) {
        return this.httpClient.put<Group>(`http://localhost:5000/groups/${group.id}`, group);
    }

    addGroup(group: Group) {
        return this.httpClient.post<Group>(`http://localhost:5000/groups`, group);
    }

    deleteGroup(groupId: number) {
         this.httpClient.delete(`http://localhost:5000/groups/${groupId}`).subscribe(
            (val) => {
              console.log('DELETE call successful value returned in body', val);
            },
            (response) => {
              console.log('DELETE call in error', response);
            }
            );
         this.router.navigate(['../'], { relativeTo: this.route });
    }

    removeTestSubject(groupId: number, testSubjectId: number) {
        return this.httpClient.delete<Group>(`http://localhost:5000/groups/${groupId}/${testSubjectId}`);
    }

    addTestSubject(groupId: number, testSubject: TestSubject) {
        return this.httpClient.patch<Group>(`http://localhost:5000/groups/${groupId}`, testSubject);
    }

}
