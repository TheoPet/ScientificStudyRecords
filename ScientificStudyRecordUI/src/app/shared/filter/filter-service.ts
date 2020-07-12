import { Injectable } from '@angular/core';
import { BasicSearch } from '../models/basic-search.model';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

@Injectable({ providedIn: 'root' })
export class FilterService {
  filterGeneric<T extends BasicSearch>(
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
}
