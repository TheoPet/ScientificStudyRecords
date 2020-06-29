import { Component, OnInit, OnDestroy } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { TestSubjectService } from 'src/app/services/test-subject.service';
import { ActivatedRoute, Router, Params } from '@angular/router';
import { Subscription, Observable, Subject } from 'rxjs';
import { map, startWith, filter, switchMap, debounceTime, tap } from 'rxjs/operators';
import { faCalendarAlt } from '@fortawesome/free-solid-svg-icons';
import { MatDialog } from '@angular/material/dialog';
import { isString } from 'util';
import { StudyService } from 'src/app/services/study.service';
import { BasicSearch } from 'src/app/shared/models/basic-search.model';
import { BasicStudy } from 'src/app/shared/models/basic-study.model';
import { BasicGroup } from 'src/app/shared/models/basic-group.model';
import { RequireMatch } from 'src/app/shared/validators/require-match.validator';

@Component({
  selector: 'app-test-subject-edit',
  templateUrl: './test-subject-edit.component.html',
  styleUrls: ['./test-subject-edit.component.css'],
})

export class TestSubjectEditComponent implements OnInit, OnDestroy {
  testSubjectForm: FormGroup;
  editMode: boolean;
  id: number;
  subscription: Subscription;
  filteredOptions: Observable<BasicStudy[]>;
  filteredGroupOptions: Observable<BasicGroup[]>;
  selectedStudy: Subject<BasicStudy> = new Subject();

  faCalendarAlt = faCalendarAlt;
  constructor(
    private service: TestSubjectService,
    private studyService: StudyService,
    private route: ActivatedRoute,
    private router: Router,
    public matDialog: MatDialog
  ) {
  }

  ngOnInit() {
    this.route.params
    .subscribe(
      (params: Params) => {
        this.id = +params.id;
        this.editMode = !isNaN(this.id);
        this.initForm();

      }
    );
    this.filterStudies();
    this.filterGroups();
    // this.filteredOptions = this.testSubjectForm.get('study').valueChanges
    //   .pipe(
    //     startWith(''),
    //     tap(
    //       data => console.log('bilo sta')
    //     ),
    //     debounceTime(300),
    //     switchMap(value => {
    //       console.log(value);
    //       if (isString(value)) {
    //         console.log(value);
    //         return this._filterGeneric({name: value}, this.studyService.getStudiesLookup());
    //       }

    //       this.selectedStudy.next(value);

    //       return this._filterGeneric(value, this.studyService.getStudiesLookup());
    //     })
    //   );

    // this.selectedStudy.subscribe( study => {
    //      this.filteredGroupOptions = this.testSubjectForm
    //        .get('group')
    //        .valueChanges.pipe(
    //          startWith(''),
    //          debounceTime(300),
    //          switchMap((value) => {
    //            if (isString(value)) {
    //              console.log(value);
    //              return this._filterGeneric(
    //                { name: value },
    //                this.studyService.getGroupsLookup(study.id)
    //              );
    //            }
    //            console.log(value);
    //            return this._filterGeneric(
    //              value,
    //              this.studyService.getGroupsLookup(study.id)
    //            );
    //          })
    //        );
    //    }
    //   );
  }

  onSubmit() {
    if (this.editMode) {
      this.service.updateTestSubject(this.id, this.testSubjectForm.value)
      .subscribe(() => this.onCancel);
    } else {
      this.service
        .addTestSubject(this.testSubjectForm.value)
        .subscribe(() => this.onCancel());
    }
  }

  onCancel() {
    this.router.navigate(['../'], {relativeTo: this.route});
  }

  initForm() {
    this.testSubjectForm = new FormGroup(
      {
        name: new FormControl('', Validators.required),
        surname: new FormControl('', Validators.required),
        entryTime: new FormControl(new Date(), Validators.required),
        comment: new FormControl(''),
        study: new FormControl(null, [Validators.required, RequireMatch]),
        group: new FormControl(null, [Validators.required, RequireMatch])
      }
    );

    if (this.editMode) {
      this.subscription = this.service.getTestSubject(this.id)
      .pipe(map(data => {
        return new FormGroup({
          name: new FormControl(data.name, Validators.required),
          surname: new FormControl(data.surname, Validators.required),
          entryTime: new FormControl(new Date(data.entryTime), Validators.required),
          comment: new FormControl(data.comment),
          study: new FormControl(data.study, [Validators.required, RequireMatch]),
          group: new FormControl(data.group, [Validators.required, RequireMatch])
        });
      })).subscribe(form => {
        this.testSubjectForm = form;
        this.filterStudies();
        this.filterGroups();
      });
    }
  }

  private _filterGeneric<T extends BasicSearch>(value: {name: string}, objectsLookup: Observable<T[]>): Observable<T[]> {
    const filterValue = value.name.toLowerCase();
    return objectsLookup.pipe(
      map(data => data.filter(it => it.name.toLowerCase().includes(filterValue)))
    );
}

  displayFunction(object: BasicSearch) {
    if (object) {
      return object.name;
    }
  }

  filterStudies() {
    let startWithString: string;
    if (this.editMode) {
      startWithString = this.testSubjectForm.get('study').value;
    } else {
      startWithString = '';
    }
    this.filteredOptions = this.testSubjectForm.get('study').valueChanges
    .pipe(
      startWith(startWithString),
      debounceTime(300),
      switchMap(value => {
        console.log(value);
        if (isString(value)) {
          return this._filterGeneric({name: value}, this.studyService.getStudiesLookup());
        }

        this.selectedStudy.next(value);

        return this._filterGeneric(value, this.studyService.getStudiesLookup());
      })
    );
  }

  filterGroups() {
    this.selectedStudy.subscribe((study) => {
      this.filteredGroupOptions = this.testSubjectForm
        .get('group')
        .valueChanges.pipe(
          startWith(''),
          debounceTime(300),
          switchMap((value) => {
            if (isString(value)) {
              console.log(value);
              return this._filterGeneric(
                { name: value },
                this.studyService.getGroupsLookup(study.id)
              );
            }
            console.log(value);
            return this._filterGeneric(
              value,
              this.studyService.getGroupsLookup(study.id)
            );
          })
        );
    });
  }
  ngOnDestroy() {
    if (this.subscription) {
    this.subscription.unsubscribe();
    }
  }
}

