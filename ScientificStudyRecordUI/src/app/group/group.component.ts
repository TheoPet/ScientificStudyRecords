import {
  Component,
  OnInit,
  OnDestroy,
  ViewChild,
  ElementRef,
} from '@angular/core';
import { Group } from './group.model';
import { ActivatedRoute, Params } from '@angular/router';
import { GroupService } from '../services/group.service';
import { Subscription } from 'rxjs';
import { MatDialogConfig, MatDialog} from '@angular/material';
import { DialogSubjectInputComponent } from '../shared/modal/dialog-subject-input/dialog-subject-input.component';
import { DialogSubjectListComponent } from '../shared/modal/dialog-subject-list/dialog-subject-list.component';
import { DialogGroupInputComponent } from '../shared/modal/dialog-group-input/dialog-group-input.component';
import { DialogDeleteComponent } from '../shared/modal/dialog-delete/dialog-delete.component';

@Component({
  selector: 'app-group',
  templateUrl: './group.component.html',
  styleUrls: ['./group.component.css'],
})
export class GroupComponent implements OnInit, OnDestroy {
  loadedGroup = new Group('', [], null);
  loadGroupSubscription: Subscription;
  removeTestSubjectSubscription: Subscription;
  afterAssignedClosedSubscription: Subscription;
  afterClosedSubscription: Subscription;
  editGroupDialogClosedSubscription: Subscription;

  searchText = '';

  toggleSearch = false;

  @ViewChild('searchbar', { static: false }) searchBar: ElementRef;

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

  openSearch() {
    this.toggleSearch = true;
    this.searchBar.nativeElement.focus();
  }
  searchClose() {
    this.searchText = '';
    this.toggleSearch = false;
  }
  getGroup(id: number) {
    this.loadGroupSubscription = this.groupService
      .getGroup(id)
      .subscribe((group) => {
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

  openAddTestSubjectDialog() {
    const dialogConfig = new MatDialogConfig();
    dialogConfig.id = 'modal-component';
    dialogConfig.width = '250px';
    dialogConfig.data = {
      title: 'Add test subject',
      groupId: this.loadedGroup.id,
      editTestSubject: false
    };

    const modalDialog = this.matDialog.open(
      DialogSubjectInputComponent,
      dialogConfig
    );
    this.afterClosedSubscription = modalDialog
      .afterClosed()
      .subscribe((data) => {
        if (data !== undefined) {
          this.loadedGroup = data;
        }
      });
  }

  openAssignTestSubjectDialog() {
    const dialogConfig = new MatDialogConfig();
    dialogConfig.id = 'modal-component';
    dialogConfig.data = this.loadedGroup.id;
    const modalDialog = this.matDialog.open(
      DialogSubjectListComponent,
      dialogConfig
    );
    this.afterAssignedClosedSubscription = modalDialog
      .afterClosed()
      .subscribe((data) => {
        if (data !== undefined) {
          this.loadedGroup = data;
        }
      });
  }

  openEditGroupDialog() {
    const dialogConfig = new MatDialogConfig();
    dialogConfig.id = 'modal-component';
    dialogConfig.width = '250px';
    dialogConfig.data = {
      title: 'Edit group',
      description: 'Please enter new group name',
      group: this.loadedGroup,
      editGroup: true
    };

    const modalDialog = this.matDialog.open(
      DialogGroupInputComponent,
      dialogConfig
    );
    this.editGroupDialogClosedSubscription = modalDialog
      .afterClosed()
      .subscribe((data) => {
        if (data !== undefined) {
          this.loadedGroup = data;
        }
      });
  }

  openDeleteGroupDialog() {
    const dialogConfig = new MatDialogConfig();
    dialogConfig.id = 'dialog-delete-component';
    dialogConfig.width = '300px';
    dialogConfig.data = {
      title: this.loadedGroup.name,
      deleteMethodName: 'deleteGroup',
      data: this.loadedGroup,
    };

    const modalDialog = this.matDialog.open(
      DialogDeleteComponent,
      dialogConfig
    );
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
