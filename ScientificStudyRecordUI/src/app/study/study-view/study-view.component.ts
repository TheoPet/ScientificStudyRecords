import { Component, OnInit, OnDestroy } from '@angular/core';
import {Router, ActivatedRoute, Params} from '@angular/router';
import { FormGroup, FormArray, FormBuilder, FormControl, Validators } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { MatDialogModule, MatDialogConfig, MatDialog } from '@angular/material/dialog';

import { ModalComponent } from '../../shared/modal/modal.component';

import { Study } from './study-view.model';
import { StudyService } from '../../services/study.service';
import { Subscription } from 'rxjs';


@Component({
  selector: 'app-study-view',
  templateUrl: './study-view.component.html',
  styleUrls: ['./study-view.component.css']
})
export class StudyViewComponent implements OnInit, OnDestroy {

  loadedStudy = new Study(' ', [], []);

  subscription: Subscription;

  constructor(private http: HttpClient,
              private route: ActivatedRoute,
              private router: Router,
              private studyService: StudyService,
              public matDialog: MatDialog) {
      }

  ngOnInit() {
    this.route.params
      .subscribe(
        (params: Params) => {
          this.loadedStudy.id = +params.id;
          this.getStudy(this.loadedStudy.id);
        }
      );
    }

    getStudy(id: number) {
      this.subscription = this.studyService.getStudy(id).subscribe(study => {
        this.loadedStudy = study;
      });

    }

    openModal() {
      const dialogConfig = new MatDialogConfig();
      dialogConfig.disableClose = true;
      dialogConfig.id = 'modal-component';
      dialogConfig.height = '300px';
      dialogConfig.width = '300px';
      dialogConfig.data = {
        name: 'deleteStudy',
        title: 'Are you sure you want to delete this study?',
        description: 'If you continue, study ' + this.loadedStudy.name + ' will be deleted.',
        actionButtonText: 'Delete',
        studyId: this.loadedStudy.id
      };
      const modalDialog = this.matDialog.open(ModalComponent, dialogConfig);
    }

    ngOnDestroy(): void {
      if (this.subscription) {
        this.subscription.unsubscribe();
      }
    }
}
