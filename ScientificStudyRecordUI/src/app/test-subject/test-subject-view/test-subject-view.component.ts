import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';
import { TestSubjectService } from 'src/app/services/test-subject.service';
import { TestSubject } from './test-subject-view.model';
import { Subscription } from 'rxjs';
import { throwMatDialogContentAlreadyAttachedError } from '@angular/material/dialog';

@Component({
  selector: 'app-test-subject-view',
  templateUrl: './test-subject-view.component.html',
  styleUrls: ['./test-subject-view.component.css']
})

export class TestSubjectViewComponent implements OnInit {

  loadedSubject = new TestSubject('', '', '', '', '', 0, '', 0);
  subscription: Subscription;

  constructor(private route: ActivatedRoute,
              private service: TestSubjectService) { }

  ngOnInit() {
    this.route.params
    .subscribe(
      (params: Params) => {
        this.loadedSubject.id = +params.id;
        this.getTestSubject(this.loadedSubject.id);
      });
  }

  getTestSubject(id: number) {
    this.subscription = this.service.getTestSubject(id).subscribe(testSubject =>
      this.loadedSubject = testSubject);
  }
}
