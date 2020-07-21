import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Group } from '../group/group.model';
import { map } from 'rxjs/operators';
import { TestSubject } from '../test-subject/test-subject-view/test-subject-view.model';

@Injectable({providedIn: 'root'})
export class GroupService {

    constructor(private httpClient: HttpClient) {

    }

    getGroup(id: number) {
        return this.httpClient.get<Group>(`http://localhost:5000/groups/${id}`).
        pipe(
            map((data) => {
                return new Group(
                    data.name,
                    id,
                    data.testSubjects,
                    data.studyId
                );
            })
        );
    }

    removeTestSubject(groupId: number, testSubjectId: number) {
        return this.httpClient.delete<Group>(`http://localhost:5000/groups/${groupId}/${testSubjectId}`);
    }

    addTestSubject(groupId: number, testSubject: TestSubject) {
        return this.httpClient.put<Group>(`http://localhost:5000/groups/${groupId}`, testSubject);
    }
}
