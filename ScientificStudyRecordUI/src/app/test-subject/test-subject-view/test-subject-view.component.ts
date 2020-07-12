import { Component, OnInit, OnDestroy } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';
import { TestSubjectService } from 'src/app/services/test-subject.service';
import { TestSubject } from './test-subject-view.model';
import { Subscription, Observable } from 'rxjs';
import { throwMatDialogContentAlreadyAttachedError } from '@angular/material/dialog';

@Component({
  selector: 'app-test-subject-view',
  templateUrl: './test-subject-view.component.html',
  styleUrls: ['./test-subject-view.component.css'],
})
export class TestSubjectViewComponent implements OnInit {
  loadedSubject = new TestSubject('', '', '', '', null, null);
  loadedSubject$: Observable<TestSubject>;

  constructor(
    private route: ActivatedRoute,
    private service: TestSubjectService
  ) {}

  ngOnInit() {
    this.route.params.subscribe((params: Params) => {
      this.getTestSubject(+params.id);
    });
  }

  getTestSubject(id: number) {
    this.loadedSubject$ = this.service
      .getTestSubject(id);
  }
}
