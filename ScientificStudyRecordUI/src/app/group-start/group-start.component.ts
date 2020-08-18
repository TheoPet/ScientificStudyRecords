import { Component, OnInit, ViewChild } from '@angular/core';
import { Group } from '../group/group.model';
import { Subscription } from 'rxjs';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator, MatDialog, MatDialogConfig } from '@angular/material';
import { GroupService } from '../services/group.service';
import { DialogGroupInputComponent } from '../shared/modal/dialog-group-input/dialog-group-input.component';

@Component({
  selector: 'app-group-start',
  templateUrl: './group-start.component.html',
  styleUrls: ['./group-start.component.css']
})
export class GroupStartComponent implements OnInit {

  loadedGroups: Group[];
  subscription: Subscription;
  afterClosedSubscription: Subscription;
  dataSource = new MatTableDataSource([]);
  private paginator: MatPaginator;
  displayedColumns: string[] = ['group', 'study'];

  @ViewChild(MatPaginator, {static: false}) set matPaginator(mp: MatPaginator) {
    this.paginator = mp;
    this.dataSource.paginator = this.paginator;
  }

  constructor(public service: GroupService,
              public matDialog: MatDialog ) { }

  ngOnInit() {
    this.getGroups();
  }

  getGroups() {
    this.subscription = this.service.getAllGroups().subscribe(data => {
      this.loadedGroups = data;
      this.dataSource.data = this.loadedGroups;
    });
    this.dataSource.paginator = this.paginator;
  }
  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();
  }

  onAddGroup() {
    const dialogConfig = new MatDialogConfig();
    dialogConfig.id = 'modal-component';
    dialogConfig.width = '350px';
    dialogConfig.panelClass = 'mat-dialog-group';
    dialogConfig.data = {
      title: 'Add group',
      description: 'Add new group',
      editGroup: false
    };

    const modalDialog = this.matDialog.open(
      DialogGroupInputComponent,
      dialogConfig
    );
    this.afterClosedSubscription = modalDialog
      .afterClosed()
      .subscribe((data) => {
        if (data !== undefined) {
          this.loadedGroups.push(data);
          this.dataSource.data = this.loadedGroups;
        }
      });
  }


}
