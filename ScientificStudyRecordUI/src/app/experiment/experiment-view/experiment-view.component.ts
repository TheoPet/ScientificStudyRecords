import { Component, OnInit, OnDestroy } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';
import {
  FormGroup,
  FormControl,
  Validators,
} from '@angular/forms';
import { Route } from '@angular/compiler/src/core';
import { ExperimentService } from 'src/app/services/experiment-service';
import { TestSubjectService } from 'src/app/services/test-subject.service';
import { Subscription, Subject, Observable } from 'rxjs';
import { TestSubject } from 'src/app/test-subject/test-subject-view/test-subject-view.model';
import { map, tap, switchMap } from 'rxjs/operators';
import { BasicData } from 'src/app/shared/models/basic-data.model';
import { FilterUtils } from 'src/app/shared/filter/filter-util';
import { Experiment } from '../experiment-view.model';

@Component({
  selector: 'app-experiment-view',
  templateUrl: './experiment-view.component.html',
  styleUrls: ['./experiment-view.component.css'],
})
export class ExperimentViewComponent implements OnInit, OnDestroy {
  experimentForm: FormGroup;
  experimentSubscription: Subscription;
  loadedTestSubject: TestSubject;
  experimentId: number;

  constructor(
    private route: ActivatedRoute,
    private experimentService: ExperimentService,
    private testSubjectService: TestSubjectService
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
        switchMap(data => {
          this.experimentForm = new FormGroup({
            task: new FormControl( {value: data.task, disabled: true}),
            time: new FormControl(new Date(data.time), Validators.required),
            comment: new FormControl(data.comment),
          });
          return this.testSubjectService
          .getTestSubject(data.testSubjectId);
        })
      ).subscribe(subject => {
        this.loadedTestSubject =  new TestSubject(
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

  ngOnDestroy() {
    if (this.experimentSubscription) {
      this.experimentSubscription.unsubscribe();
    }
  }

  onSubmit() {
    const experiment = new Experiment(
      this.experimentForm.get('time').value,
      this.experimentForm.get('comment').value,
      this.loadedTestSubject.id,
      this.experimentForm.get('task').value,
      this.loadedTestSubject.group.id,
      this.experimentId
    );

    this.experimentService.updateExperiment(experiment).subscribe();
  }
}
