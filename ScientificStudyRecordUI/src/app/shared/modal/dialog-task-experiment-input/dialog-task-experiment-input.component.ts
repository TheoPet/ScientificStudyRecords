import { Component, OnInit, Inject } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Experiment } from 'src/app/experiment/experiment-view.model';
import { ExperimentService } from 'src/app/services/experiment-service';
import { startWith, debounceTime, switchMap, filter } from 'rxjs/operators';
import { Observable } from 'rxjs';
import { BasicTestSubject } from '../../models/basic-test-subject.model';
import { FilterService } from '../../filter/filter-service';
import { isString } from 'util';
import { TestSubjectService } from 'src/app/services/test-subject.service';
import { BasicData } from '../../models/basic-data.model';
import { FilterUtils } from '../../filter/filter-util';

@Component({
  selector: 'app-dialog-task-experiment-input',
  templateUrl: './dialog-task-experiment-input.component.html',
  styleUrls: ['./dialog-task-experiment-input.component.css'],
})
export class DialogTaskExperimentInputComponent implements OnInit {
  dialogForm: FormGroup;
  filteredOptions: Observable<BasicTestSubject[]>;
  time: string;

  constructor(
    public dialogRef: MatDialogRef<DialogTaskExperimentInputComponent>,
    @Inject(MAT_DIALOG_DATA) public modalData: any,
    private experimentService: ExperimentService,
    private filterService: FilterService,
    private testSubjectService: TestSubjectService
  ) {}

  ngOnInit() {
    this.initForm();
    this.setTime();
  }

  initForm() {
    this.dialogForm = new FormGroup({
      testSubject: new FormControl(null, Validators.required),
      comment: new FormControl(''),
    });
    this.filterTestSubjects();
  }

  onTimeChange(event: string) {
    this.time = event;
  }

  setTime() {
    const date = new Date();
    const hour = date.getHours().toString();
    const minutes = date.getMinutes().toString();
    this.time = hour + ':' + minutes;
  }
  onSubmit() {
    const experiment = new Experiment(
      this.time,
      this.dialogForm.get('comment').value,
      this.dialogForm.get('testSubject').value,
      this.modalData.task,
      this.dialogForm.get('testSubject').value.groupId,
      this.modalData.studyId
    );

    return this.experimentService.saveExperiment(experiment).subscribe( data => {
      this.dialogRef.close(data);
    });
  }

  filterTestSubjects() {
    this.filteredOptions = this.dialogForm.get('testSubject')
    .valueChanges.pipe(
      startWith(''),
      debounceTime(300),
      switchMap((value) => {
        if (isString(value)) {
          return this.filterService.filterGeneric(
            { name: value },
            this.testSubjectService.getTestSubjectsFromSameStudy(this.modalData.studyId)
          );
        }
        return this.filterService.filterGeneric(
          value,
          this.testSubjectService.getTestSubjectsFromSameStudy(this.modalData.studyId)
          );
      }));
  }

  displayFunction(object: BasicTestSubject) {
    return FilterUtils.displaFunctionTestSubject(object);
  }
}
