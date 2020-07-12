import { Component, OnInit, OnDestroy } from '@angular/core';
import {Router, ActivatedRoute, Params} from '@angular/router';
import { FormGroup, FormArray, FormBuilder, FormControl, Validators } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { MatDialogModule, MatDialogConfig, MatDialog } from '@angular/material/dialog';

import { ModalComponent } from '../../shared/modal/modal.component';

import { Study } from './study-view.model';
import { StudyService } from '../../services/study.service';
import { Subscription, Observable } from 'rxjs';
import { DialogInputComponent } from 'src/app/shared/modal/dialog-input/dialog-input.component';
import { BasicGroup } from 'src/app/shared/models/basic-group.model';
import { BasicTask } from 'src/app/shared/models/basic-task.model';


@Component({
  selector: 'app-study-view',
  templateUrl: './study-view.component.html',
  styleUrls: ['./study-view.component.css'],
})
export class StudyViewComponent implements OnInit, OnDestroy {
  loadedStudy = new Study(' ', [], []);
  addedGroupOrTask: string;

  subscription: Subscription;
  groupDialogSubscription: Subscription;
  taskDialogSubscription: Subscription;
  updateStudySubscription: Subscription;
  deleteGroupOrTaskSubscription: Subscription;
  groupDialogClosed$: Observable<any>;
  taskDialogClosed$: Observable<any>;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private studyService: StudyService,
    public matDialog: MatDialog
  ) {}

  ngOnInit() {
    this.route.params.subscribe((params: Params) => {
      this.loadedStudy.id = +params.id;
      this.getStudy(this.loadedStudy.id);
    });
  }

  getStudy(id: number) {
    this.subscription = this.studyService.getStudy(id).subscribe((study) => {
      this.loadedStudy = study;
    });
  }

  openGroupModal() {
    const dialogConfig = this.configureMatDailogConfig(
      'Add new group',
      'Group name'
    );
    const modalDialog = this.matDialog.open(DialogInputComponent, dialogConfig);

    this.groupDialogClosed$ = modalDialog.afterClosed();

    this.onAddGroup();
  }

  openTaskModal() {
    const dialogConfig = this.configureMatDailogConfig(
      'Add new task',
      'Task name'
    );
    const modalDialog = this.matDialog.open(DialogInputComponent, dialogConfig);

    this.taskDialogClosed$ = modalDialog.afterClosed();

    this.onAddTask();
  }

  configureMatDailogConfig(title: string, description: string) {
    const dialogConfig = new MatDialogConfig();
    dialogConfig.id = 'modal-component';
    dialogConfig.width = '250px';
    dialogConfig.disableClose = true;
    dialogConfig.data = {
      title,
      description,
    };
    return dialogConfig;
  }

  onAddGroup() {
    this.groupDialogSubscription = this.groupDialogClosed$.subscribe(
      (result) => {
        this.loadedStudy.groups.push(new BasicGroup(result));
        this.studyService
          .updateStudy(this.loadedStudy.id, this.loadedStudy)
          .subscribe();
      }
    );
  }
  onAddTask() {
    this.taskDialogSubscription = this.taskDialogClosed$.subscribe((result) => {
      this.loadedStudy.tasks.push(new BasicTask(result));
      this.studyService
        .updateStudy(this.loadedStudy.id, this.loadedStudy)
        .subscribe();
    });
  }

  onDeleteGroup(id: number) {
    const index = this.loadedStudy.groups.findIndex((g) => g.id === id);
    this.loadedStudy.groups.splice(index, 1);
    this.onDelete(id, true);
  }

  onDelete(id: number, deleteGroup = false) {
    if (!deleteGroup) {
      const index = this.loadedStudy.tasks.findIndex((t) => t.id === id);
      this.loadedStudy.tasks.splice(index, 1);
    }
    this.deleteGroupOrTaskSubscription = this.studyService
      .deleteGroupOrTask(this.loadedStudy.id, id, deleteGroup)
      .subscribe();
  }

  ngOnDestroy(): void {
    if (this.subscription) {
      this.subscription.unsubscribe();
    }

    if (this.groupDialogSubscription) {
      this.groupDialogSubscription.unsubscribe();
    }

    if (this.taskDialogSubscription) {
      this.taskDialogSubscription.unsubscribe();
    }

    if (this.updateStudySubscription) {
      this.updateStudySubscription.unsubscribe();
    }

    if (this.deleteGroupOrTaskSubscription) {
      this.deleteGroupOrTaskSubscription.unsubscribe();
    }
  }
}
