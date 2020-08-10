import { Component, OnInit, OnDestroy } from '@angular/core';
import { FormArray, FormControl, FormGroup, Validators} from '@angular/forms';
import { Router, ActivatedRoute, Params} from '@angular/router';
import { StudyService } from '../../services/study.service';
import { Study } from '../study-view/study-view.model';
import { Subscription, Observable } from 'rxjs';
import { map } from 'rxjs/operators';

@Component({
  selector: 'app-study-edit',
  templateUrl: './study-edit.component.html',
  styleUrls: ['./study-edit.component.css']
})
export class StudyEditComponent implements OnInit, OnDestroy {
  study: Observable<Study>;
  studyForm: FormGroup;
  id: number;
  editMode: boolean;
  subscription: Subscription;

  constructor(private route: ActivatedRoute,
              private router: Router,
              private studyService: StudyService) { }

  ngOnInit() {
    this.route.params
      .subscribe(
        (params: Params) => {
          this.id = +params.id;
          this.editMode = !isNaN(this.id);
          this.initForm();
        }
      );
  }

  onAddTasks() {
    const control = new FormControl(null);
    (this.studyForm.get('tasks') as FormArray).push(control);
  }

  onAddGroups() {
     const control = new FormControl(null, Validators.required);
     (this.studyForm.get('groups')as FormArray).push(control);
  }

  onSubmit() {
    if (this.editMode) {
      // this.studyService.updateStudy(this.id, this.studyForm.value);
    } else {
      this.studyService.addStudy(this.studyForm.value).subscribe(() => this.onCancel());
    }
  }

  onCancel() {
    this.router.navigate(['../'], {relativeTo: this.route});
  }

  initForm() {
    const tasks = new FormArray([]);
    const groups = new FormArray([]);
    this.studyForm = new FormGroup(
      {
        name: new FormControl('', Validators.required),
        tasks,
        groups
      }
    );

    if (this.editMode) {
     this.subscription = this.studyService.getStudy(this.id)
      .pipe(map(data => {

        for (const t of data.tasks) {
          tasks.push(new FormControl(t));
        }

        for (const g of data.groups) {
          groups.push(new FormControl(g));
        }

        return new FormGroup(
          {
            name: new FormControl(data.name, Validators.required),
            tasks,
            groups
          }
        );
      })).subscribe(form => this.studyForm = form);
    }
  }

  ngOnDestroy() {
    if (this.subscription) {
      this.subscription.unsubscribe();
     }
  }
}
