import { Component, OnInit, OnDestroy } from '@angular/core';
import { Group } from './group.model';
import { ActivatedRoute, Params } from '@angular/router';
import { GroupService } from '../services/group.service';
import { Subscription } from 'rxjs';
import { MatDialogConfig, MatDialog } from '@angular/material';
import { DialogSubjectInputComponent } from '../shared/modal/dialog-subject-input/dialog-subject-input.component';
import { DialogSubjectListComponent } from '../shared/modal/dialog-subject-list/dialog-subject-list.component';

@Component({
  selector: 'app-group',
  templateUrl: './group.component.html',
  styleUrls: ['./group.component.css'],
})

export class GroupComponent implements OnInit, OnDestroy {
  loadedGroup = new Group('', 0, null, 0);
  loadGroupSubscription: Subscription;
  removeTestSubjectSubscription: Subscription;
  afterAssignedClosedSubscription: Subscription;
  afterClosedSubscription: Subscription;

  constructor(
    private route: ActivatedRoute,
    private groupService: GroupService,
    public matDialog: MatDialog

  ) {}

  ngOnInit() {
    this.route.params.subscribe((params: Params) => {
      this.loadedGroup.id = +params.id;
      this.getGroup(this.loadedGroup.id);
    });
  }

  getGroup(id: number) {
    this.loadGroupSubscription = this.groupService.getGroup(id).subscribe((group) => {
      this.loadedGroup = group;
    });
  }

  onRemove(id: number) {
    this.removeTestSubjectSubscription = this.groupService
      .removeTestSubject(this.loadedGroup.id, id)
      .subscribe((data) => {
        this.loadedGroup = data;
      });
  }

  openDialog() {
    const dialogConfig = new MatDialogConfig();
    dialogConfig.id = 'modal-component';
    dialogConfig.width = '250px';
    dialogConfig.data = {
      title: 'Add test subject',
      groupId: this.loadedGroup.id
    };

    const modalDialog = this.matDialog.open(DialogSubjectInputComponent, dialogConfig);
    this.afterClosedSubscription = modalDialog.afterClosed().subscribe((data) => {
      if (data !== undefined) {
        this.loadedGroup = data;
      }
    });
  }

  openAssignDialog() {
    const dialogConfig = new MatDialogConfig();
    dialogConfig.id = 'modal-component';
    dialogConfig.data = this.loadedGroup.id;
    const modalDialog = this.matDialog.open(DialogSubjectListComponent, dialogConfig);
    this.afterAssignedClosedSubscription = modalDialog.afterClosed().subscribe((data) => {
      if (data !== undefined) {
        this.loadedGroup = data;
      }
    });
  }

  ngOnDestroy() {
    if (this.loadGroupSubscription) {
      this.loadGroupSubscription.unsubscribe();
    }

    if (this.removeTestSubjectSubscription) {
      this.removeTestSubjectSubscription.unsubscribe();
    }

    if (this.afterClosedSubscription) {
      this.afterClosedSubscription.unsubscribe();
    }
  }
}
