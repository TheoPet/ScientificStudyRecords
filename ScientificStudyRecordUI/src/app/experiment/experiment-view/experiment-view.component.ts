import { Component, OnInit, OnDestroy } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { ExperimentService } from 'src/app/services/experiment-service';
import { TestSubjectService } from 'src/app/services/test-subject.service';
import { Subscription } from 'rxjs';
import { TestSubject } from 'src/app/test-subject/test-subject-view/test-subject-view.model';
import { switchMap } from 'rxjs/operators';
import { Experiment } from '../experiment-view.model';
import { MatDialogConfig, MatDialog } from '@angular/material/dialog';
import { DialogDeleteComponent } from 'src/app/shared/modal/dialog-delete/dialog-delete.component';
import { DialogExperimentInputComponent } from 'src/app/shared/modal/dialog-experiment-input/dialog-experiment-input.component';
import { BasicTestSubject } from 'src/app/shared/models/basic-test-subject.model';

@Component({
  selector: 'app-experiment-view',
  templateUrl: './experiment-view.component.html',
  styleUrls: ['./experiment-view.component.css'],
})
export class ExperimentViewComponent implements OnInit, OnDestroy {
  experimentForm: FormGroup;
  experimentSubscription: Subscription;
  loadedTestSubject: TestSubject;
  loadedExperiment: Experiment;
  afterClosedSubscription: Subscription;
  experimentId: number;

  constructor(
    private route: ActivatedRoute,
    private experimentService: ExperimentService,
    private testSubjectService: TestSubjectService,
    public matDialog: MatDialog
  ) {}

  ngOnInit() {
    this.route.params.subscribe((params: Params) => {
      this.experimentId = +params.id;
      this.initForm(this.experimentId);
    });
  }

  initForm(id: number) {
    this.experimentForm = new FormGroup({
      task: new FormControl(null, Validators.required),
      time: new FormControl(null, Validators.required),
      comment: new FormControl(''),
    });

    this.experimentSubscription = this.experimentService
      .getExperiment(id)
      .pipe(
        switchMap((data) => {
          this.loadedExperiment = data;
          this.experimentForm = new FormGroup({
            task: new FormControl({ value: data.task, disabled: true }),
            time: new FormControl(new Date(data.time), Validators.required),
            comment: new FormControl(data.comment),
          });
          return this.testSubjectService.getTestSubject(data.testSubject.id);
        })
      )
      .subscribe((subject) => {
        this.loadedTestSubject = new TestSubject(
          subject.name,
          subject.surname,
          subject.entryTime,
          subject.comment,
          subject.study,
          subject.group,
          subject.experiments,
          subject.id
        );
      });
  }

  openEditExperimentDialog() {
    const dialogConfig = new MatDialogConfig();
    dialogConfig.id = 'modal-component';
    dialogConfig.width = '350px';
    dialogConfig.panelClass = 'mat-dialog-experiments';
    dialogConfig.data = {
      title: 'Add experiment',
      testSubject: this.loadedTestSubject,
      experiment: this.loadedExperiment,
      editExperiment: true,
    };

    const modalDialog = this.matDialog.open(
      DialogExperimentInputComponent,
      dialogConfig
    );
    this.afterClosedSubscription = modalDialog
      .afterClosed()
      .subscribe((data) => {
        if (data !== undefined) {
          this.loadedExperiment = data;
        }
      });
  }

  openDeleteExperimentDialog() {
    const dialogConfig = new MatDialogConfig();
    dialogConfig.id = 'dialog-delete-component';
    dialogConfig.width = '300px';
    dialogConfig.data = {
      title:
        'experiment for ' +
        this.loadedTestSubject.name +
        ' ' +
        this.loadedTestSubject.surname,
      deleteMethodName: 'deleteExperiment',
      data: this.loadedExperiment,
    };
    const modalDialog = this.matDialog.open(
      DialogDeleteComponent,
      dialogConfig
    );
  }

  ngOnDestroy() {
    if (this.experimentSubscription) {
      this.experimentSubscription.unsubscribe();
    }

    if (this.afterClosedSubscription) {
      this.afterClosedSubscription.unsubscribe();
    }
  }
}
