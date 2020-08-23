import { Injectable } from '@angular/core';
import { BasicData } from '../models/basic-data.model';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { HttpResponse } from '@angular/common/http';
import { BasicTestSubject } from '../models/basic-test-subject.model';

@Injectable({ providedIn: 'root' })
export class FilterService {
  filterGeneric<T extends BasicData>(
    value: { name: string },
    objectsLookup: Observable<T[]>
  ): Observable<T[]> {
    const filterValue = value.name.toLowerCase();
    return objectsLookup.pipe(
      map((data) =>
        data.filter((it) => it.name.toLowerCase().includes(filterValue))
      )
    );
  }

  filterGenericResponseData<T extends BasicData>(
    value: { name: string },
    response: Observable<HttpResponse<T[]>>
  ): Observable<T[]> {
    const filterValue = value.name.toLowerCase();
    return response.pipe(
      map((data) =>
        data.body.filter((it) => it.name.toLowerCase().includes(filterValue))
      )
    );
  }

  filterGenericResponseTestSubjectData<T extends BasicTestSubject>(
    value: { name: string },
    response: Observable<HttpResponse<T[]>>
  ): Observable<T[]> {
    const filterValue = value.name.toLowerCase();
    return response.pipe(
      map((data) =>
        data.body.filter(
          (it) =>
            it.name.toLowerCase().includes(filterValue) ||
            it.surname.toLowerCase().includes(filterValue)
        )
      )
    );
  }
}
