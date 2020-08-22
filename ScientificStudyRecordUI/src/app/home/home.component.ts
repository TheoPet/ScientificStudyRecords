import { Component, OnInit, ViewChild, AfterViewInit } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
import { Observable } from 'rxjs';
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

enum SearchBoxType {
  Study = 'Study',
  Group = 'Group',
  Subject = 'Subject',
}

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
})
export class HomeComponent implements OnInit, AfterViewInit {
  searchForm: FormGroup;

  filteredTestSubjectOptions: Observable<BasicTestSubject[]>;
  filteredStudyOptions: Observable<BasicStudy[]>;
  filteredGroupOptions: Observable<BasicGroup[]>;

  private studySearchPaginator: MatPaginator;
  private groupSearchPaginator: MatPaginator;
  private subjectSearchPaginator: MatPaginator;

  totalStudiesCount: number;
  totalGroupsCount: number;
  totalSubjectsCount: number;

  isLoadingResults = false;
  afterInitNotCalled = true;

  @ViewChild(MatPaginator, { static: false }) set matStudyPaginator(
    mp: MatPaginator
  ) {
    this.studySearchPaginator = mp;
  }

  @ViewChild(MatPaginator, { static: false }) set matGroupPaginator(
    mp: MatPaginator
  ) {
    this.groupSearchPaginator = mp;
  }

  @ViewChild(MatPaginator, { static: false }) set matSubjectPaginator(
    mp: MatPaginator
  ) {
    this.subjectSearchPaginator = mp;
  }

  constructor(
    private studyService: StudyService,
    private groupService: GroupService,
    private filterService: FilterService,
    private subjectService: TestSubjectService
  ) {}

  ngOnInit() {
    console.log('ngOnInit');
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
          console.log('studyInit');
          if (isString(value)) {
            return this.studyService
              .getStudiesFiltered(
                this.studySearchPaginator.pageIndex + 1,
                this.studySearchPaginator.pageSize,
                value
              )
              .pipe(
                map((response) => {
                  const paginationHeader = JSON.parse(response.headers.get('X-Pagination'));
                  this.totalStudiesCount = paginationHeader.totalCount;
                  return response.body;
                })
              );
          }
          return this.filterService.filterGeneric(
            value,
            this.studyService.getStudiesLookup()
          );
        })
      );

    this.filteredGroupOptions = this.searchForm
      .get('groupSearch')
      .valueChanges.pipe(
        startWith(''),
        debounceTime(300),
        switchMap((value) => {
          console.log('studyInit');
          if (isString(value)) {
            return this.groupService
              .getGroupsFiltered(
                this.groupSearchPaginator.pageIndex + 1,
                this.groupSearchPaginator.pageSize,
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
          // TODO: imamo li groups simplified?
          // return this.filterService.filterGeneric(value, this.groupService.getAllGroups());
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
              this.subjectSearchPaginator.pageIndex + 1,
              this.subjectSearchPaginator.pageSize,
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
          // TODO
          // return this.filterService.filterGeneric(value, this.subjectService.getAllTestSubjects());
        })
      );
  }

  ngAfterViewInit() {
    if (this.afterInitNotCalled) {
      setTimeout(() => {
        console.log('studyAfter');
        this.studyService
          .getStudiesFiltered(
            this.studySearchPaginator.pageIndex + 1,
            this.studySearchPaginator.pageSize,
            this.searchForm.get('studySearch').value.name
          )
          .subscribe((response) => {
            const header = JSON.parse(response.headers.get('X-Pagination'));
            this.totalStudiesCount = header.totalCount;
          });
      });
      setTimeout(() => {
        console.log('studyAfter');
        this.groupService
          .getGroupsFiltered(
            this.groupSearchPaginator.pageIndex + 1,
            this.groupSearchPaginator.pageSize,
            this.searchForm.get('groupSearch').value.name
          )
          .subscribe((response) => {
            const header = JSON.parse(response.headers.get('X-Pagination'));
            this.totalGroupsCount = header.totalCount;
          });
      });
      setTimeout(() => {
        console.log('subjectAfter');
        this.subjectService
          .getTestSubjectFiltered(
            this.subjectSearchPaginator.pageIndex + 1,
            this.subjectSearchPaginator.pageSize,
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

  onPageFired(event: PageEvent) {
    console.log(event);
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
    console.log(event);
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
}
