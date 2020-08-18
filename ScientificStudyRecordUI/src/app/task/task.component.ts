import { Component, OnInit, ViewChild, OnDestroy } from '@angular/core';
import { TaskService } from '../services/task.service';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { MatDialog, MatDialogConfig } from '@angular/material';
import { Task } from './task.model';
import { ActivatedRoute, Router, Params } from '@angular/router';
import { Subscription } from 'rxjs';
import { DialogDeleteComponent } from '../shared/modal/dialog-delete/dialog-delete.component';
import { DialogTaskExperimentInputComponent } from '../shared/modal/dialog-task-experiment-input/dialog-task-experiment-input.component';
import { BasicTask } from '../shared/models/basic-task.model';

@Component({
  selector: 'app-task',
  templateUrl: './task.component.html',
  styleUrls: ['./task.component.css'],
})
export class TaskComponent implements OnInit, OnDestroy {
  displayedColumns: string[] = [
    'testSubject',
    'experimentTime',
    'experimentComment',
  ];

  dataSource = new MatTableDataSource([]);
  private paginator: MatPaginator;
  loadedTask: Task;
  taskSubscription: Subscription;
  afterClosedSubscription: Subscription;

  @ViewChild(MatPaginator, { static: false }) set matPaginator(
    mp: MatPaginator
  ) {
    this.paginator = mp;
    this.dataSource.paginator = this.paginator;
  }

  constructor(public service: TaskService,
              public matDialog: MatDialog,
              private route: ActivatedRoute,
              private router: Router) {}

  ngOnInit() {
    this.route.params.subscribe((params: Params) => {
      this.getTask(+params.id);
    });
  }

  getTask(id: number) {
    this.taskSubscription = this.service.getTask(id).subscribe(data => {
      this.loadedTask = data;
      this.dataSource = new MatTableDataSource(data.experiments);
    });
  }

  openDeleteTaskDialog() {
    const dialogConfig = new MatDialogConfig();
    dialogConfig.id = 'dialog-delete-component';
    dialogConfig.width = '300px';
    dialogConfig.data = {
      title: this.loadedTask.name,
      deleteMethodName: 'deleteTask',
      data: this.loadedTask,
    };

    const modalDialog = this.matDialog.open(
      DialogDeleteComponent,
      dialogConfig
    );
  }

  openAddExperimentDialog() {
    const dialogConfig = new MatDialogConfig();
    dialogConfig.id = 'modal-component';
    dialogConfig.width = '300px';
    dialogConfig.panelClass = 'mat-dialog-experiments';
    dialogConfig.data = {
      title: 'Add experiment',
      task: new BasicTask(this.loadedTask.name, this.loadedTask.id),
      studyId: this.loadedTask.study.id
    };

    const modalDialog = this.matDialog.open(
      DialogTaskExperimentInputComponent,
      dialogConfig
    );
    this.afterClosedSubscription = modalDialog
      .afterClosed()
      .subscribe((data) => {
        if (data !== undefined) {
          this.loadedTask.experiments.push(data);
          this.dataSource.data = this.loadedTask.experiments;
        }
      });
  }

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();
  }

  configureFilterPredicate() {
    this.dataSource.filterPredicate = (rowData, searchTerm) => {
      const dataStr = (
        rowData.testSubject.name +
        rowData.comment +
        rowData.time
      ).toLowerCase();
      return dataStr.includes(searchTerm);
    };
  }

  ngOnDestroy() {
    if (this.taskSubscription) {
      this.taskSubscription.unsubscribe();
    }

    if (this.afterClosedSubscription) {
      this.afterClosedSubscription.unsubscribe();
    }
  }
}
