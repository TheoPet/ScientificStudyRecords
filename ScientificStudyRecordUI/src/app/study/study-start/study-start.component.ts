import { Component, OnInit, OnDestroy, ViewChild } from '@angular/core';
import { Study } from '../study-view/study-view.model';
import { StudyService } from 'src/app/services/study.service';
import { Subscription } from 'rxjs';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
import { ActivatedRoute, Router } from '@angular/router';
import { MatDialogConfig, MatDialog } from '@angular/material';
import { DialogStudyInputComponent } from 'src/app/shared/modal/dialog-study-input/dialog-study-input.component';

@Component({
  selector: 'app-study-start',
  templateUrl: './study-start.component.html',
  styleUrls: ['./study-start.component.css'],
})

export class StudyStartComponent implements OnInit, OnDestroy {
  loadedStudies: Study[];
  subscription: Subscription;
  afterClosedSubscription: Subscription;
  dataSource = new MatTableDataSource([]);
  private paginator: MatPaginator;
  displayedColumns: string[] = ['study'];

  constructor(
    private studyService: StudyService,
    private route: ActivatedRoute,
    private router: Router,
    public matDialog: MatDialog
  ) {}

  ngOnInit() {
    this.getStudies();
  }

  @ViewChild(MatPaginator, { static: false }) set matPaginator(
    mp: MatPaginator
  ) {
    this.paginator = mp;
    this.dataSource.paginator = this.paginator;
  }

  getStudies() {
    this.subscription = this.studyService.getStudies().subscribe((studies) => {
      this.loadedStudies = studies;
      this.dataSource.data = this.loadedStudies;
    });
    this.dataSource.paginator = this.paginator;
  }


  onAddStudy() {
    const dialogConfig = new MatDialogConfig();
    dialogConfig.id = 'modal-component';
    dialogConfig.width = '350px';
    dialogConfig.data = {
      title: 'Add study',
      editStudy: false,
      description: 'Name'
    };

    const modalDialog = this.matDialog.open(DialogStudyInputComponent, dialogConfig);
    this.afterClosedSubscription = modalDialog.afterClosed().subscribe((data) => {
      if (data !== undefined) {
        this.loadedStudies.push(data);
        this.dataSource.data = this.loadedStudies;
      }
    });
  }

  openStudy(study: Study) {
    this.router.navigate(['../studies', study.id], { relativeTo: this.route });
  }

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();
  }

  ngOnDestroy() {
    this.subscription.unsubscribe();
  }
}
