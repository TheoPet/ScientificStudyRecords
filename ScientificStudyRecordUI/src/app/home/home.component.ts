import {
  Component,
  OnInit,
  ViewChild,
  AfterViewInit,
  OnDestroy,
  ViewChildren,
  QueryList,
} from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
import { Observable, Subscription } from 'rxjs';
import { BasicStudy } from '../shared/models/basic-study.model';
import { BasicGroup } from '../shared/models/basic-group.model';
import { BasicData } from '../shared/models/basic-data.model';
import { FilterUtils } from '../shared/filter/filter-util';
import { BasicTestSubject } from '../shared/models/basic-test-subject.model';
import { MatPaginator, MatTableDataSource, PageEvent } from '@angular/material';
import { StudyService } from '../services/study.service';
import { startWith, switchMap, map, debounceTime } from 'rxjs/operators';
import { isString } from 'util';
import { FilterService } from '../shared/filter/filter-service';
import { GroupService } from '../services/group.service';
import { TestSubjectService } from '../services/test-subject.service';
import { ReportExperiment } from '../experiment/experiment-report-view.model';
import { ExperimentService } from '../services/experiment-service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
})
export class HomeComponent implements OnInit, AfterViewInit, OnDestroy {
  searchForm: FormGroup;

  showTablePagination: boolean;

  filteredTestSubjectOptions: Observable<BasicTestSubject[]>;
  filteredStudyOptions: Observable<BasicStudy[]>;
  filteredGroupOptions: Observable<BasicGroup[]>;

  filteredExperimentsByStudySubscription: Subscription;
  filteredExperimentsByGroupSubscription: Subscription;
  filteredExperimentsByTestSubjectSubscription: Subscription;

  private studySearchPaginator: MatPaginator;
  private groupSearchPaginator: MatPaginator;
  private subjectSearchPaginator: MatPaginator;
  private experimentPaginator: MatPaginator;

  totalStudiesCount: number;
  totalGroupsCount: number;
  totalSubjectsCount: number;
  totalExperimentsCount: number;

  afterInitNotCalled = true;

  filterByStudyFlag = true;
  filterByGroupFlag = false;
  filterByTestSubjectFlag = false;

  filters = ['Study', 'Group', 'Test subject'];
  selectedFilter = this.filters[0];

  displayColumns = [
    { def: 'study', hide: this.filterByStudyFlag },
    { def: 'group', hide: this.filterByGroupFlag },
    { def: 'testSubject', hide: this.filterByTestSubjectFlag },
    { def: 'testSubjectComment', hide: false },
    { def: 'testSubjectEntryTime', hide: false },
    { def: 'task', hide: false },
    { def: 'experimentTime', hide: false },
    { def: 'experimentComment', hide: false },
  ];

  dataSource = new MatTableDataSource([]);

  // @ViewChild('studySearchPaginator', { static: false }) set matStudyPaginator(
  //   smp: MatPaginator
  // ) {
  //   this.studySearchPaginator = smp;
  // }

  // @ViewChild('groupSearchPaginator', { static: false }) set matGroupPaginator(
  //   gmp: MatPaginator
  // ) {
  //   this.groupSearchPaginator = gmp;
  // }

  // @ViewChild('subjectSearchPaginator', { static: false }) set matSubjectPaginator(
  //   tmp: MatPaginator
  // ) {
  //   this.subjectSearchPaginator = tmp;
  // }

  // @ViewChild('experimentPaginator', { static: false }) set matExperimentPaginator(
  //   emp: MatPaginator
  // ) {
  //   this.experimentPaginator = emp;
  // }
  @ViewChildren(MatPaginator) paginator = new QueryList<MatPaginator>();

  constructor(
    private studyService: StudyService,
    private groupService: GroupService,
    private filterService: FilterService,
    private subjectService: TestSubjectService,
    private experimentService: ExperimentService
  ) {
    this.dataSource = new MatTableDataSource([]);
  }

  ngOnInit() {
    this.searchForm = new FormGroup({
      studySearch: new FormControl(''),
      groupSearch: new FormControl(''),
      subjectSearch: new FormControl(''),
    });

    this.filteredStudyOptions = this.searchForm
      .get('studySearch')
      .valueChanges.pipe(
        startWith(''),
        debounceTime(300),
        switchMap((value) => {
          if (isString(value)) {
            return this.studyService
              .getStudiesFiltered(
                this.paginator.toArray()[0].pageIndex + 1,
                this.paginator.toArray()[0].pageSize,
                value
              )
              .pipe(
                map((response) => {
                  const paginationHeader = JSON.parse(
                    response.headers.get('X-Pagination')
                  );
                  this.totalStudiesCount = paginationHeader.totalCount;
                  return response.body;
                })
              );
          }

          return this.filterService.filterGenericResponseData(
            value,
            this.studyService.getStudiesFiltered(
              this.paginator.toArray()[0].pageIndex + 1,
              this.paginator.toArray()[0].pageSize,
              value.name
            )
          );
        })
      );

    this.filteredGroupOptions = this.searchForm
      .get('groupSearch')
      .valueChanges.pipe(
        startWith(''),
        debounceTime(300),
        switchMap((value) => {
          if (isString(value)) {
            return this.groupService
              .getGroupsFiltered(
                this.paginator.toArray()[0].pageIndex + 1,
                this.paginator.toArray()[0].pageSize,
                value
              )
              .pipe(
                map((response) => {
                  const test = JSON.parse(response.headers.get('X-Pagination'));
                  this.totalGroupsCount = test.totalCount;
                  return response.body;
                })
              );
          }
          return this.filterService.filterGenericResponseData(
            value,
            this.groupService.getGroupsFiltered(
              this.paginator.toArray()[0].pageIndex + 1,
              this.paginator.toArray()[0].pageSize,
              value.name
            )
          );
        })
      );

    this.filteredTestSubjectOptions = this.searchForm
      .get('subjectSearch')
      .valueChanges.pipe(
        startWith(''),
        debounceTime(300),
        switchMap((value) => {
          if (isString(value)) {
            return this.subjectService
              .getTestSubjectFiltered(
                this.paginator.toArray()[0].pageIndex + 1,
                this.paginator.toArray()[0].pageSize,
                value
              )
              .pipe(
                map((response) => {
                  const test = JSON.parse(response.headers.get('X-Pagination'));
                  this.totalSubjectsCount = test.totalCount;
                  return response.body;
                })
              );
          }
          return this.filterService.filterGenericResponseData(
            value,
            this.subjectService.getTestSubjectFiltered(
              this.paginator.toArray()[0].pageIndex + 1,
              this.paginator.toArray()[0].pageSize,
              value.name
            )
          );
        })
      );
  }

  ngAfterViewInit() {
    if (this.afterInitNotCalled) {
      setTimeout(() => {
        this.studyService
          .getStudiesFiltered(
            this.paginator.toArray()[0].pageIndex + 1,
            this.paginator.toArray()[0].pageSize,
            this.searchForm.get('studySearch').value.name
          )
          .subscribe((response) => {
            const header = JSON.parse(response.headers.get('X-Pagination'));
            this.totalStudiesCount = header.totalCount;
          });
      });
      setTimeout(() => {
        this.groupService
          .getGroupsFiltered(
            this.paginator.toArray()[0].pageIndex + 1,
            this.paginator.toArray()[0].pageSize,
            this.searchForm.get('groupSearch').value.name
          )
          .subscribe((response) => {
            const header = JSON.parse(response.headers.get('X-Pagination'));
            this.totalGroupsCount = header.totalCount;
          });
      });
      setTimeout(() => {
        this.subjectService
          .getTestSubjectFiltered(
            this.paginator.toArray()[0].pageIndex + 1,
            this.paginator.toArray()[0].pageSize,
            this.searchForm.get('subjectSearch').value.name
          )
          .subscribe((response) => {
            const header = JSON.parse(response.headers.get('X-Pagination'));
            this.totalGroupsCount = header.totalCount;
          });
      });
      this.afterInitNotCalled = false;
    }
  }

  onPageFiredExperiment(event: PageEvent) {
    this.search(event.pageIndex);
  }
  onPageFiredStudy(event: PageEvent) {
    this.filteredStudyOptions = this.studyService
      .getStudiesFiltered(
        ++event.pageIndex,
        event.pageSize,
        this.searchForm.get('studySearch').value.name
      )
      .pipe(
        map((response) => {
          const paginationHeader = JSON.parse(
            response.headers.get('X-Pagination')
          );
          this.totalStudiesCount = paginationHeader.totalCount;
          return response.body;
        })
      );
  }

  onPageFiredGroup(event: PageEvent) {
    this.filteredGroupOptions = this.groupService
      .getGroupsFiltered(
        ++event.pageIndex,
        event.pageSize,
        this.searchForm.get('groupSearch').value.name
      )
      .pipe(
        map((response) => {
          const paginationHeader = JSON.parse(
            response.headers.get('X-Pagination')
          );
          this.totalGroupsCount = paginationHeader.totalCount;
          return response.body;
        })
      );
  }

  onPageFiredSubject(event: PageEvent) {
    this.filteredTestSubjectOptions = this.subjectService
      .getTestSubjectFiltered(
        ++event.pageIndex,
        event.pageSize,
        this.searchForm.get('subjectSearch').value.name
      )
      .pipe(
        map((response) => {
          const paginationHeader = JSON.parse(
            response.headers.get('X-Pagination')
          );
          this.totalSubjectsCount = paginationHeader.totalCount;
          return response.body;
        })
      );
  }

  displayFunction(data: BasicData) {
    return FilterUtils.displayFunction(data);
  }

  displayFunctionTestSubject(data: BasicTestSubject) {
    return FilterUtils.displayFunctionTestSubject(data);
  }

  filterByStudy() {
    this.clearTable();
    this.filterByStudyFlag = true;
    this.filterByGroupFlag = false;
    this.filterByTestSubjectFlag = false;
    this.reinitializeDisplayColumns();
  }

  filterByGroup() {
    this.clearTable();
    this.filterByStudyFlag = false;
    this.filterByGroupFlag = true;
    this.filterByTestSubjectFlag = false;
    this.reinitializeDisplayColumns();
  }

  filterByTestSubject() {
    this.clearTable();
    this.filterByStudyFlag = false;
    this.filterByGroupFlag = false;
    this.filterByTestSubjectFlag = true;
    this.reinitializeDisplayColumns();
  }

  reinitializeDisplayColumns() {
    this.displayColumns = [
      { def: 'study', hide: this.filterByStudyFlag },
      { def: 'group', hide: this.filterByGroupFlag },
      { def: 'testSubject', hide: this.filterByTestSubjectFlag },
      { def: 'testSubjectComment', hide: false },
      { def: 'testSubjectEntryTime', hide: false },
      { def: 'task', hide: false },
      { def: 'experimentTime', hide: false },
      { def: 'experimentComment', hide: false },
    ];
  }

  clearTable() {
    this.dataSource.data = [];
    this.dataSource.paginator = this.paginator.toArray()[1];
  }

  selectFilter(event: Event) {
    this.showTablePagination = false;
    switch (event.toString()) {
      case 'Study':
        this.filterByStudy();
        break;

      case 'Group':
        this.filterByGroup();
        break;

      case 'Test subject':
        this.filterByTestSubject();
        break;
    }
  }

  search(pageIndex: number) {
    this.showTablePagination = true;
    setTimeout(() => this.searchInternal(pageIndex), 1);
  }

  searchInternal(pageIndex: number) {
    if (this.filterByStudyFlag) {
      this.filteredExperimentsByStudySubscription = this.experimentService
        .getFilteredExperimentsByStudy(
          this.searchForm.get('studySearch').value.id,
          this.paginator.toArray()[1].pageSize,
          pageIndex ? pageIndex + 1 : this.paginator.toArray()[1].pageIndex + 1
        )
        .subscribe((response) => {
          const header = JSON.parse(response.headers.get('X-Pagination'));
          this.totalExperimentsCount = header.totalCount;
          this.dataSource.data = response.body;
          this.dataSource.paginator = this.paginator.toArray()[1];
        });
    }

    if (this.filterByGroupFlag) {
      this.filteredExperimentsByGroupSubscription = this.experimentService
        .getFilteredExperimentsByGroup(
          this.searchForm.get('groupSearch').value.id,
          this.paginator.toArray()[1].pageSize,
          pageIndex ? pageIndex + 1 : this.paginator.toArray()[1].pageIndex + 1
        )
        .subscribe((response) => {
          const header = JSON.parse(response.headers.get('X-Pagination'));
          this.totalExperimentsCount = header.totalCount;
          this.dataSource.data = response.body;
          this.dataSource.paginator = this.paginator.toArray()[1];
        });
    }

    if (this.filterByTestSubjectFlag) {
      this.filteredExperimentsByTestSubjectSubscription = this.experimentService
        .getFilteredExperimentsByTestSubject(
          this.searchForm.get('subjectSearch').value.id,
          this.paginator.toArray()[1].pageSize,
          pageIndex ? pageIndex + 1 : this.paginator.toArray()[1].pageIndex + 1
        )
        .subscribe((response) => {
          const header = JSON.parse(response.headers.get('X-Pagination'));
          this.totalExperimentsCount = header.totalCount;
          this.dataSource.data = response.body;
          this.dataSource.paginator = this.paginator.toArray()[1];
        });
    }
  }

  getDisplayedColumns(): string[] {
    return this.displayColumns.filter((cd) => !cd.hide).map((cd) => cd.def);
  }

  ngOnDestroy() {
    if (this.filteredExperimentsByStudySubscription) {
      this.filteredExperimentsByStudySubscription.unsubscribe();
    }

    if (this.filteredExperimentsByGroupSubscription) {
      this.filteredExperimentsByGroupSubscription.unsubscribe();
    }

    if (this.filteredExperimentsByTestSubjectSubscription) {
      this.filteredExperimentsByTestSubjectSubscription.unsubscribe();
    }
  }

  onClick() {
    if (this.filterByStudyFlag) {
      const studyId = this.searchForm.get('studySearch').value.id;
      const studyName = this.searchForm.get('studySearch').value.name;
      this.experimentService
        .exportReportFilteredByStudy(studyId, studyName)
        .subscribe((response) => {
          const url = URL.createObjectURL(response);
          const anchor = document.createElement('a');
          anchor.href = url;
          anchor.target = '_blank';
          anchor.click();
          anchor.remove();
        });
    }

    if (this.filterByGroupFlag) {
      const groupId = this.searchForm.get('groupSearch').value.id;
      const groupName = this.searchForm.get('groupSearch').value.name;
      this.experimentService
        .exportReportFilteredByGroup(groupId, groupName)
        .subscribe((response) => {
          const url = URL.createObjectURL(response);
          const anchor = document.createElement('a');
          anchor.href = url;
          anchor.target = '_blank';
          anchor.click();
          anchor.remove();
        });
    }

    if (this.filterByTestSubjectFlag) {
      const subjectId = this.searchForm.get('subjectSearch').value.id;
      const subjectName = this.searchForm.get('subjectSearch').value.name;
      this.experimentService
        .exportReportFilteredByTestSubject(subjectId, subjectName)
        .subscribe((response) => {
          const url = URL.createObjectURL(response);
          const anchor = document.createElement('a');
          anchor.href = url;
          anchor.target = '_blank';
          anchor.click();
          anchor.remove();
        });
    }
  }
}
