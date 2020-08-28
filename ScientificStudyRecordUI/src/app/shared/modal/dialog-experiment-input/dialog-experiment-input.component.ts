import { Component, OnInit, Inject } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { TestSubject } from 'src/app/test-subject/test-subject-view/test-subject-view.model';
import { TaskService } from 'src/app/services/task.service';
import { startWith, debounceTime, switchMap } from 'rxjs/operators';
import { isString } from 'util';
import { FilterService } from '../../filter/filter-service';
import { BasicTask } from '../../models/basic-task.model';
import { Observable } from 'rxjs';
import { BasicData } from '../../models/basic-data.model';
import { FilterUtils } from '../../filter/filter-util';
import { Experiment } from 'src/app/experiment/experiment-view.model';
import { ExperimentService } from 'src/app/services/experiment-service';
import { StudyService } from 'src/app/services/study.service';
import { BasicTestSubject } from '../../models/basic-test-subject.model';

@Component({
  selector: 'app-dialog-experiment-input',
  templateUrl: './dialog-experiment-input.component.html',
  styleUrls: ['./dialog-experiment-input.component.css'],
})
export class DialogExperimentInputComponent implements OnInit {
  dialogForm: FormGroup;
  testSubject: TestSubject;
  experiment: Experiment;
  filteredOptions: Observable<BasicTask[]>;
  time: string;

  constructor(
    public dialogRef: MatDialogRef<DialogExperimentInputComponent>,
    @Inject(MAT_DIALOG_DATA) public modalData: any,
    public studyService: StudyService,
    public filterService: FilterService,
    public experimentService: ExperimentService
  ) {}

  ngOnInit() {
    this.testSubject = this.modalData.testSubject;
    this.experiment = this.modalData.experiment;
    this.setTime();
    this.initForm();
  }

  filterTasks() {
    this.filteredOptions = this.dialogForm.get('task').valueChanges.pipe(
      startWith(''),
      debounceTime(300),
      switchMap((value) => {
        if (isString(value)) {
          return this.filterService.filterGeneric(
            { name: value },
            this.studyService.getTaskLookup(this.testSubject.study.id)
          );
        }

        return this.filterService.filterGeneric(
          value,
          this.studyService.getTaskLookup(this.testSubject.study.id)
        );
      })
    );
  }

  displayFunction(object: BasicData) {
    return FilterUtils.displayFunction(object);
  }

  initForm() {
    this.dialogForm = new FormGroup({
      study: new FormControl(null, Validators.required),
      group: new FormControl(null, Validators.required),
      task: new FormControl(null, Validators.required),
      comment: new FormControl(null),
    });

    if (this.modalData.editExperiment) {
      this.dialogForm = new FormGroup({
        study: new FormControl(
          { value: this.testSubject.study.name, disabled: true },
          Validators.required
        ),
        group: new FormControl(
          { value: this.testSubject.group.name, disabled: true },
          Validators.required
        ),
        task: new FormControl(
          { value: this.experiment.task, disabled: true },
          Validators.required
        ),
        time: new FormControl(
          new Date(this.experiment.time),
          Validators.required
        ),
        comment: new FormControl(this.experiment.comment),
      });
    } else {
      this.studyService
        .getTaskLookup(this.testSubject.study.id)
        .subscribe((tasks) => {
          this.dialogForm = new FormGroup({
            study: new FormControl(
              { value: this.testSubject.study.name, disabled: true },
              Validators.required
            ),
            group: new FormControl(
              { value: this.testSubject.group.name, disabled: true },
              Validators.required
            ),
            task: new FormControl(null, Validators.required),
            time: new FormControl(new Date(), Validators.required),
            comment: new FormControl(null),
          });
          this.filterTasks();
        });
    }
  }

  onTimeChange(event) {
    this.time = event;
  }
  onSubmit() {
    const experiment = new Experiment(
      this.time,
      this.dialogForm.get('comment').value,
      new BasicTestSubject(
        this.testSubject.name,
        this.testSubject.surname,
        this.testSubject.id
      ),
      this.dialogForm.get('task').value,
      this.testSubject.group.id,
      this.testSubject.study.id
    );

    if (this.modalData.editExperiment) {
      experiment.id = this.experiment.id;
      return this.experimentService
        .updateExperiment(experiment)
        .subscribe((data) => {
          this.dialogRef.close(data);
        });
    }
    return this.experimentService
      .saveExperiment(experiment)
      .subscribe((data) => {
        this.dialogRef.close(data);
      });
  }

  setTime() {
    const date = new Date();
    if (this.modalData.editExperiment) {
      return  this.time = this.experiment.time;
    }

    const hour = date.getHours().toString();
    const minutes = date.getMinutes().toString();
    this.time = hour + ':' + minutes;
  }
}
