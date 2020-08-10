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

@Component({
  selector: 'app-dialog-experiment-input',
  templateUrl: './dialog-experiment-input.component.html',
  styleUrls: ['./dialog-experiment-input.component.css'],
})
export class DialogExperimentInputComponent implements OnInit {
  dialogForm: FormGroup;
  testSubject: TestSubject;
  filteredOptions: Observable<BasicTask[]>;

  constructor(
    public dialogRef: MatDialogRef<DialogExperimentInputComponent>,
    @Inject(MAT_DIALOG_DATA) public modalData: any,
    public taskService: TaskService,
    public filterService: FilterService,
    public experimentService: ExperimentService
  ) {}

  ngOnInit() {
    this.testSubject = this.modalData.testSubject;
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
            this.taskService.getTaskLookup(this.testSubject.study.id)
          );
        }

        return this.filterService.filterGeneric(
          value,
          this.taskService.getTaskLookup(this.testSubject.study.id)
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
      time: new FormControl(new Date(), Validators.required),
      comment: new FormControl(null),
    });

    this.taskService
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

  onSubmit() {
    const experiment = new Experiment(
      this.dialogForm.get('time').value,
      this.dialogForm.get('comment').value,
      this.testSubject.id,
      this.dialogForm.get('task').value,
      this.testSubject.group.id
    );

    return this.experimentService.saveExperiment(experiment).subscribe( data => {
      this.dialogRef.close(data);
    });
  }
}
