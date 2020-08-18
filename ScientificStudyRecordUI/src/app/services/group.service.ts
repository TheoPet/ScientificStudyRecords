import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Group } from '../group/group.model';
import { map } from 'rxjs/operators';
import { TestSubject } from '../test-subject/test-subject-view/test-subject-view.model';
import { ActivatedRoute, Router } from '@angular/router';

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
