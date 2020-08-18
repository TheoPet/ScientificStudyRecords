import { Component, OnInit, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { BasicData } from '../../models/basic-data.model';
import { FilterUtils } from '../../filter/filter-util';
import { StudyService } from 'src/app/services/study.service';
import { Observable, Subject } from 'rxjs';
import { BasicStudy } from '../../models/basic-study.model';
import { BasicGroup } from '../../models/basic-group.model';
import { startWith, debounceTime, switchMap } from 'rxjs/operators';
import { isString } from 'util';
import { FilterService } from '../../filter/filter-service';
import { TestSubject } from 'src/app/test-subject/test-subject-view/test-subject-view.model';
import { RequireMatch } from '../../validators/require-match.validator';
import { conditionalValidator } from '../../validators/conditional-require.validator';
import { TestSubjectService } from 'src/app/services/test-subject.service';

@Component({
  selector: 'app-dialog-test-subject-input',
  templateUrl: './dialog-test-subject-input.component.html',
  styleUrls: ['./dialog-test-subject-input.component.css']
})

export class DialogTestSubjectInputComponent implements OnInit {
  dialogForm: FormGroup;
  filteredOptions: Observable<BasicStudy[]>;
  filteredGroupOptions: Observable<BasicGroup[]>;
  selectedStudy: Subject<BasicStudy> = new Subject();


  constructor(public dialogRef: MatDialogRef<DialogTestSubjectInputComponent>,
              @Inject(MAT_DIALOG_DATA) public modalData: any,
              public studyService: StudyService,
              public filterService: FilterService,
              public testSubjectService: TestSubjectService
    ) { }

  ngOnInit() {
    this.initForm();
    this.filterStudies();
    this.filterGroups();
  }

  displayFunction(object: BasicData) {
    return FilterUtils.displayFunction(object);
  }

  filterStudies() {
    this.filteredOptions = this.dialogForm.get('study').valueChanges
    .pipe(
      startWith(''),
      debounceTime(300),
      switchMap(value => {
        if (isString(value)) {
          return this.filterService.filterGeneric({name: value}, this.studyService.getStudiesLookup());
        }

        this.selectedStudy.next(value);

        return this.filterService.filterGeneric(value, this.studyService.getStudiesLookup());
      })
    );
  }

  filterGroups() {
    this.selectedStudy.subscribe((study) => {
      this.filteredGroupOptions = this.dialogForm
        .get('group')
        .valueChanges.pipe(
          startWith(''),
          debounceTime(300),
          switchMap((value) => {
            if (isString(value)) {
              return this.filterService.filterGeneric(
                { name: value },
                this.studyService.getGroupsLookup(study.id)
              );
            }
            return this.filterService.filterGeneric(
              value,
              this.studyService.getGroupsLookup(study.id)
            );
          })
        );
    });
  }

  initForm() {
    this.dialogForm = new FormGroup({
      name: new FormControl('', Validators.required),
      surname: new FormControl('', Validators.required),
      entryTime: new FormControl(new Date(), Validators.required),
      comment: new FormControl(''),
      study: new FormControl(null, RequireMatch),
      group: new FormControl(null, [
        RequireMatch,
        conditionalValidator(() => this.dialogForm.get('study').value),
      ]),
    });
  }

  onSubmit() {
    const testSubject = new TestSubject(
      this.dialogForm.get('name').value,
      this.dialogForm.get('surname').value,
      this.dialogForm.get('entryTime').value,
      this.dialogForm.get('comment').value,
      this.dialogForm.get('study').value,
      this.dialogForm.get('group').value
    );
    return this.testSubjectService.addTestSubject(testSubject).subscribe( data => {
      this.dialogRef.close(data);
    });
  }
}
