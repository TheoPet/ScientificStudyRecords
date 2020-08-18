import { Component, OnInit, OnDestroy, ViewChild } from '@angular/core';
import { TestSubjectService } from 'src/app/services/test-subject.service';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { TestSubject } from '../test-subject-view/test-subject-view.model';
import { Subscription } from 'rxjs';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
import { ActivatedRoute, Router } from '@angular/router';
import { DialogTestSubjectInputComponent } from 'src/app/shared/modal/dialog-test-subject-input/dialog-test-subject-input.component';
import { DialogStudyAssignComponent } from 'src/app/shared/modal/dialog-study-assign/dialog-study-assign.component';

@Component({
  selector: 'app-test-subject-start',
  templateUrl: './test-subject-start.component.html',
  styleUrls: ['./test-subject-start.component.css']
})
export class TestSubjectStartComponent implements OnInit, OnDestroy {
  loadedTestSubjects: TestSubject[];
  subscription: Subscription;
  afterClosedSubscription: Subscription;
  dataSource = new MatTableDataSource([]);
  private paginator: MatPaginator;
  displayedColumns: string[] = ['testSubject', 'entryTime', 'comment', 'study', 'group'];

  @ViewChild(MatPaginator, {static: false}) set matPaginator(mp: MatPaginator) {
    this.paginator = mp;
    this.dataSource.paginator = this.paginator;
  }
  constructor(private route: ActivatedRoute,
              private router: Router,
              private service: TestSubjectService,
              public matDialog: MatDialog) { }

  ngOnInit() {
    this.getTestSubjects();
  }

  getTestSubjects() {
    this.subscription = this.service.getAllTestSubjects().subscribe(data => {
      this.loadedTestSubjects = data;
      this.dataSource.data = this.loadedTestSubjects;
    });
    this.dataSource.paginator = this.paginator;
  }

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();
  }

  openTestSubject(testSubject: TestSubject) {

    if (testSubject.study.name === null) {
      this.assignTestSubjectToStudy(testSubject);
    } else {
      this.router.navigate(['../testsubjects', testSubject.id, 'groups', testSubject.group.id], { relativeTo: this.route });

    }
  }

  assignTestSubjectToStudy(testSubject: TestSubject) {
    const dialogConfig = new MatDialogConfig();
    dialogConfig.id = 'modal-component';
    dialogConfig.width = '350px';
    dialogConfig.panelClass = 'mat-dialog-assign';
    dialogConfig.data = {
      title: 'Assign ' + testSubject.name + ' ' + testSubject.surname + ' to study',
      testSubject
    };

    const modalDialog = this.matDialog.open(
      DialogStudyAssignComponent,
      dialogConfig
    );
    this.afterClosedSubscription = modalDialog
      .afterClosed()
      .subscribe((data) => {
        if (data !== undefined) {
          this.loadedTestSubjects.push(data);
          this.dataSource.data = this.loadedTestSubjects;
        }
      });
  }

  onAddTestSubject() {
    const dialogConfig = new MatDialogConfig();
    dialogConfig.id = 'modal-component';
    dialogConfig.width = '350px';
    dialogConfig.panelClass = 'mat-dialog-testSubject';
    dialogConfig.data = {
      title: 'Add test subject',
    };

    const modalDialog = this.matDialog.open(
      DialogTestSubjectInputComponent,
      dialogConfig
    );
    this.afterClosedSubscription = modalDialog
      .afterClosed()
      .subscribe((data) => {
        if (data !== undefined) {
          this.loadedTestSubjects.push(data);
          this.dataSource.data = this.loadedTestSubjects;
        }
      });
  }
  ngOnDestroy() {
    this.subscription.unsubscribe();
  }
}
