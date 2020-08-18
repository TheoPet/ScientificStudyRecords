import { Component, OnInit, OnDestroy } from '@angular/core';
import { Router, ActivatedRoute, Params } from '@angular/router';
import { MatDialogConfig, MatDialog } from '@angular/material/dialog';

import { Study } from './study-view.model';
import { StudyService } from '../../services/study.service';
import { Subscription, Observable } from 'rxjs';
import { DialogInputComponent } from 'src/app/shared/modal/dialog-input/dialog-input.component';
import { DialogStudyInputComponent } from 'src/app/shared/modal/dialog-study-input/dialog-study-input.component';
import { DialogDeleteComponent } from 'src/app/shared/modal/dialog-delete/dialog-delete.component';

@Component({
  selector: 'app-study-view',
  templateUrl: './study-view.component.html',
  styleUrls: ['./study-view.component.css'],
})
export class StudyViewComponent implements OnInit, OnDestroy {
  loadedStudy = new Study(' ', [], []);

  subscription: Subscription;
  groupDialogSubscription: Subscription;
  taskDialogSubscription: Subscription;
  addTaskSubscription: Subscription;
  addGroupSubscription: Subscription;
  deleteGroupOrTaskSubscription: Subscription;
  groupDialogClosedSubscription: Subscription;
  taskDialogClosedSubscription: Subscription;
  editStudyDialogClosedSubscription: Subscription;

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
      'Group name',
      this.loadedStudy.id,
      true
    );
    const modalDialog = this.matDialog.open(DialogInputComponent, dialogConfig);

    this.groupDialogClosedSubscription = modalDialog
      .afterClosed()
      .subscribe((data) => {
        if (data !== undefined) {
          this.loadedStudy = data;
        }
      });
  }

  openTaskModal() {
    const dialogConfig = this.configureMatDailogConfig(
      'Add new task',
      'Task name',
      this.loadedStudy.id
    );
    const modalDialog = this.matDialog.open(DialogInputComponent, dialogConfig);

    this.taskDialogClosedSubscription = modalDialog
      .afterClosed()
      .subscribe((data) => {
        if (data !== undefined) {
          this.loadedStudy = data;
        }
      });
  }

  configureMatDailogConfig(
    title: string,
    description: string,
    studyId: number,
    group = false
  ) {
    const dialogConfig = new MatDialogConfig();
    dialogConfig.id = 'modal-component';
    dialogConfig.width = '250px';
    dialogConfig.data = {
      title,
      description,
      studyId,
      group,
    };
    return dialogConfig;
  }

  openEditStudyDialog() {
    const dialogConfig = new MatDialogConfig();
    dialogConfig.id = 'modal-component';
    dialogConfig.width = '250px';
    dialogConfig.data = {
      title: 'Edit study',
      description: 'Please enter new study name',
      editStudy: true,
      study: this.loadedStudy,
    };

    const modalDialog = this.matDialog.open(
      DialogStudyInputComponent,
      dialogConfig
    );
    this.editStudyDialogClosedSubscription = modalDialog
      .afterClosed()
      .subscribe((data) => {
        if (data !== undefined) {
          this.loadedStudy = data;
        }
      });
  }

  openDeleteStudyDialog() {
    const dialogConfig = new MatDialogConfig();
    dialogConfig.id = 'dialog-delete-component';
    dialogConfig.width = '300px';
    dialogConfig.data = {
      title: this.loadedStudy.name,
      deleteMethodName: 'deleteStudy',
      data: this.loadedStudy,
    };

    const modalDialog = this.matDialog.open(
      DialogDeleteComponent,
      dialogConfig
    );
  }

  onDeleteGroup(id: number) {
    this.onDelete(id, true);
  }

  onDelete(id: number, deleteGroup = false) {
    this.deleteGroupOrTaskSubscription = this.studyService
      .deleteGroupOrTask(this.loadedStudy.id, id, deleteGroup)
      .subscribe((data) => {
        this.loadedStudy = data;
      });
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

    if (this.addTaskSubscription) {
      this.addTaskSubscription.unsubscribe();
    }

    if (this.addGroupSubscription) {
      this.addGroupSubscription.unsubscribe();
    }

    if (this.deleteGroupOrTaskSubscription) {
      this.deleteGroupOrTaskSubscription.unsubscribe();
    }

    if (this.taskDialogClosedSubscription) {
      this.taskDialogClosedSubscription.unsubscribe();
    }

    if (this.groupDialogClosedSubscription) {
      this.groupDialogClosedSubscription.unsubscribe();
    }

    if (this.editStudyDialogClosedSubscription) {
      this.editStudyDialogClosedSubscription.unsubscribe();
    }
  }
}
