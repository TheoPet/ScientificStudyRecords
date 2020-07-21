import { Component, OnInit, ViewChild, Inject } from '@angular/core';
import { MatTableDataSource, MAT_DIALOG_DATA, MatDialogRef } from '@angular/material';
import { TestSubjectService } from 'src/app/services/test-subject.service';
import { Subscription } from 'rxjs';
import { MatPaginator } from '@angular/material/paginator';
import { GroupService } from 'src/app/services/group.service';
import { TestSubject } from 'src/app/test-subject/test-subject-view/test-subject-view.model';



@Component({
  selector: 'app-dialog-subject-list',
  templateUrl: './dialog-subject-list.component.html',
  styleUrls: ['./dialog-subject-list.component.css'],
})
export class DialogSubjectListComponent implements OnInit {
  displayedColumns: string[] = ['name', 'surname'];
  data: TestSubject[] = [];
  dataSource = new MatTableDataSource(this.data);
  subscription: Subscription;
  constructor(
    public testSubjectService: TestSubjectService,
    public groupService: GroupService,
    @Inject(MAT_DIALOG_DATA) public modalData: number,
    public dialogRef: MatDialogRef<DialogSubjectListComponent>
  ) {}

  @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;

  ngOnInit() {
    this.subscription = this.testSubjectService
      .getAvailableTestSubjects()
      .subscribe((data) => {
        this.data = data;
        this.dataSource.data = this.data;
      });
    this.dataSource.paginator = this.paginator;
  }

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();
  }

  assign(rowData: TestSubject) {
    rowData.study = null;
    rowData.group = null;
    return this.groupService.addTestSubject(this.modalData, rowData).subscribe( data => {
      this.dialogRef.close(data);
    });
  }
}

