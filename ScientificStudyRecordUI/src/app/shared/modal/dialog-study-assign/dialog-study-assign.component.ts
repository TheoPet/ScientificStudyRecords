import { Component, OnInit, Inject } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
import { Observable, Subject } from 'rxjs';
import { BasicStudy } from '../../models/basic-study.model';
import { BasicGroup } from '../../models/basic-group.model';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { DialogTestSubjectInputComponent } from '../dialog-test-subject-input/dialog-test-subject-input.component';
import { StudyService } from 'src/app/services/study.service';
import { FilterService } from '../../filter/filter-service';
import { BasicData } from '../../models/basic-data.model';
import { FilterUtils } from '../../filter/filter-util';
import { startWith, debounceTime, switchMap } from 'rxjs/operators';
import { isString } from 'util';
import { RequireMatch } from '../../validators/require-match.validator';
import { conditionalValidator } from '../../validators/conditional-require.validator';
import { TestSubjectService } from 'src/app/services/test-subject.service';

@Component({
  selector: 'app-dialog-study-assign',
  templateUrl: './dialog-study-assign.component.html',
  styleUrls: ['./dialog-study-assign.component.css']
})
export class DialogStudyAssignComponent implements OnInit {
  dialogForm: FormGroup;
  filteredOptions: Observable<BasicStudy[]>;
  filteredGroupOptions: Observable<BasicGroup[]>;
  selectedStudy: Subject<BasicStudy> = new Subject();
  constructor(public dialogRef: MatDialogRef<DialogTestSubjectInputComponent>,
              @Inject(MAT_DIALOG_DATA) public modalData: any,
              public studyService: StudyService,
              public filterService: FilterService,
              public testSubjectService: TestSubjectService) { }

  ngOnInit() {
    this.dialogForm = new FormGroup({
      study: new FormControl(null, RequireMatch),
      group: new FormControl(null, [
        RequireMatch,
        conditionalValidator(() => this.dialogForm.get('study').value),
      ]),
    });
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
  onSubmit() {
    this.modalData.testSubject.study = this.dialogForm.get('study').value;
    this.modalData.testSubject.group = this.dialogForm.get('group').value;

    return this.testSubjectService.assignStudyAndGroupToTestSubject(this.modalData.testSubject).subscribe( data => {
      this.dialogRef.close(data);
    });
  }
}
