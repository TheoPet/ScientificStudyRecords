import { Component, OnInit, OnDestroy, ViewChild } from '@angular/core';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { TestSubjectService } from 'src/app/services/test-subject.service';
import { TestSubject } from './test-subject-view.model';
import { Subscription } from 'rxjs';
import { MatTableDataSource } from '@angular/material/table';
import { MatDialogConfig, MatDialog, MatPaginator, MatSort } from '@angular/material';
import { DialogExperimentInputComponent } from 'src/app/shared/modal/dialog-experiment-input/dialog-experiment-input.component';
import { Experiment } from 'src/app/experiment/experiment-view.model';
import { DialogDeleteComponent } from 'src/app/shared/modal/dialog-delete/dialog-delete.component';
import { DialogSubjectInputComponent } from 'src/app/shared/modal/dialog-subject-input/dialog-subject-input.component';

@Component({
  selector: 'app-test-subject-view',
  templateUrl: './test-subject-view.component.html',
  styleUrls: ['./test-subject-view.component.css'],
})

export class TestSubjectViewComponent implements OnInit, OnDestroy {
  loadedSubject: TestSubject;
  loadedSubjectSubscription: Subscription;
  afterClosedSubscription: Subscription;
  displayedColumns: string[] = ['task', 'comment', 'entryTime'];
  dataSource = new MatTableDataSource([]);
  private paginator: MatPaginator;


  @ViewChild(MatPaginator, {static: false}) set matPaginator(mp: MatPaginator) {
    this.paginator = mp;
    this.dataSource.paginator = this.paginator;
  }

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private service: TestSubjectService,
    public matDialog: MatDialog
  ) {}

  ngOnInit() {
    this.route.params.subscribe((params: Params) => {
      this.getTestSubject(+params.id, +params.groupId);
    });
  }

  getTestSubject(id: number, groupId: number) {
    this.loadedSubjectSubscription = this.service
      .getTestSubjectWithFilteredExperiment(id, groupId)
      .subscribe((data) => {
        this.loadedSubject = data;
        this.dataSource.data =  this.loadedSubject.experiments;
        this.configureFilterPredicate();
      });
    this.dataSource.paginator = this.paginator;
  }

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();
  }

  configureFilterPredicate() {
    this.dataSource.filterPredicate = (rowData, searchTerm) => {
      const dataStr = (
        rowData.task.name +
        rowData.comment +
        rowData.time
      ).toLowerCase();
      return dataStr.includes(searchTerm);
    };
  }

  openAddExperimentDialog() {
    const dialogConfig = new MatDialogConfig();
    dialogConfig.id = 'modal-component';
    dialogConfig.width = '350px';
    dialogConfig.panelClass = 'mat-dialog-experiments';
    dialogConfig.data = {
      title: 'Add experiment',
      testSubject: this.loadedSubject,
      editExperiment: false,
    };

    const modalDialog = this.matDialog.open(
      DialogExperimentInputComponent,
      dialogConfig
    );
    this.afterClosedSubscription = modalDialog
      .afterClosed()
      .subscribe((data) => {
        if (data !== undefined) {
          this.loadedSubject.experiments.push(data);
          this.dataSource.data = this.loadedSubject.experiments;
        }
      });
  }

  openExperiment(experiment: Experiment) {
    this.router.navigate(['../experiments', experiment.id]);
  }

  openEditTestSubjectDialog() {
    const dialogConfig = new MatDialogConfig();
    dialogConfig.id = 'modal-component';
    dialogConfig.width = '350px';
    dialogConfig.data = {
      title: 'Edit test subject',
      testSubject: this.loadedSubject,
      editTestSubject: true
    };

    const modalDialog = this.matDialog.open(
      DialogSubjectInputComponent,
      dialogConfig
    );
    this.afterClosedSubscription = modalDialog
      .afterClosed()
      .subscribe((data) => {
        if (data !== undefined) {
          this.loadedSubject = data;
        }
      });
  }

  openDeleteTestSubjectDialog() {
    const dialogConfig = new MatDialogConfig();
    dialogConfig.id = 'dialog-delete-component';
    dialogConfig.width = '350px';
    dialogConfig.data = {
      title: this.loadedSubject.name + ' ' + this.loadedSubject.surname,
      deleteMethodName: 'deleteTestSubject',
      data: this.loadedSubject,
    };

    const modalDialog = this.matDialog.open(
      DialogDeleteComponent,
      dialogConfig
    );
  }
  ngOnDestroy() {
    if (this.loadedSubjectSubscription) {
      this.loadedSubjectSubscription.unsubscribe();
    }
  }
}
