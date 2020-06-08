import { Component, OnInit, OnDestroy } from '@angular/core';
import { Study } from '../study-view/study-view.model';
import { StudyService } from 'src/app/services/study.service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-study-start',
  templateUrl: './study-start.component.html',
  styleUrls: ['./study-start.component.css']
})
export class StudyStartComponent implements OnInit, OnDestroy {

  loadedStudies: Study[];
  subscription: Subscription;

  constructor(private studyService: StudyService) { }
  ngOnInit() {
    this.getStudies();
  }

  getStudies() {
    this.subscription = this.studyService.getStudies().subscribe(
      studies => { this.loadedStudies = studies; }
    );

  }
  ngOnDestroy() {
    this.subscription.unsubscribe();
  }
}
