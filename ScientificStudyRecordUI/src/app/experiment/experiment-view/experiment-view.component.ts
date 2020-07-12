import { Component, OnInit, OnDestroy, ViewChild } from '@angular/core';
import { TestSubjectService } from 'src/app/services/test-subject.service';
import { ActivatedRoute, Params } from '@angular/router';
import { TestSubject } from 'src/app/test-subject/test-subject-view/test-subject-view.model';
import { Subscription, Observable, Subject } from 'rxjs';
import { faTshirt } from '@fortawesome/free-solid-svg-icons';
import { FormControl } from '@angular/forms';
import {  FilterService } from 'src/app/shared/filter/filter-service';
import { BasicSearch } from 'src/app/shared/models/basic-search.model';
import { BasicTask } from 'src/app/shared/models/basic-task.model';
import { TaskService } from 'src/app/services/task.service';
import { startWith, debounceTime, switchMap, map } from 'rxjs/operators';
import { isString } from 'util';
import { Experiment } from '../experiment-view.model';
import { ExperimentService } from 'src/app/services/experiment-service';
import { FilterUtils } from 'src/app/shared/filter/filter-util';
import { MatPaginator, MatSort, MatTableDataSource } from '@angular/material';

export interface UserData {
  id: string;
  name: string;
  progress: string;
}

const NAMES: string[] = [
  'Maia', 'Asher', 'Olivia', 'Atticus', 'Amelia', 'Jack', 'Charlotte', 'Theodore', 'Isla', 'Oliver',
  'Isabella', 'Jasper', 'Cora', 'Levi', 'Violet', 'Arthur', 'Mia', 'Thomas', 'Elizabeth'
];

@Component({
  selector: 'app-experiment-view',
  templateUrl: './experiment-view.component.html',
  styleUrls: ['./experiment-view.component.css'],
})
export class ExperimentViewComponent implements OnInit, OnDestroy {
  loadedSubject$: Observable<TestSubject>;
  subscription: Subscription;
  displayedColumns: string[] = ['id', 'name', 'progress'];
  dataSource: MatTableDataSource<UserData>;

  @ViewChild(MatPaginator, {static: true}) paginator: MatPaginator;
  @ViewChild(MatSort, {static: true}) sort: MatSort;

  constructor(
    private testSubjectService: TestSubjectService,
    private experimentService: ExperimentService,
    private taskService: TaskService,
    private filterService: FilterService,
    private route: ActivatedRoute
  ) {
    // Create 100 users
    const users = Array.from({length: 100}, (_, k) => createNewUser(k + 1));

    // Assign the data to the data source for the table to render
    this.dataSource = new MatTableDataSource(users);
  }

  ngOnInit() {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
    this.route.params.subscribe((params: Params) => {
      this.getTestSubject(+params.id);
    });
  }

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();

    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
  }

  getTestSubject(id: number) {
    this.loadedSubject$ = this.testSubjectService
      .getTestSubject(id);
  }

  displayFunction(object: BasicSearch) {
    return FilterUtils.displayFunction(object);
  }


  ngOnDestroy() {
    if (this.subscription) {
      this.subscription.unsubscribe();
    }
  }
}
/** Builds and returns a new User. */
function createNewUser(id: number): UserData {
  const name = NAMES[Math.round(Math.random() * (NAMES.length - 1))] + ' ' +
      NAMES[Math.round(Math.random() * (NAMES.length - 1))].charAt(0) + '.';

  return {
    id: id.toString(),
    name,
    progress: Math.round(Math.random() * 100).toString()
  };

}
