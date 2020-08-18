import { Component, OnInit, Inject } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { GroupService } from 'src/app/services/group.service';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { Group } from 'src/app/group/group.model';
import { Observable } from 'rxjs';
import { BasicStudy } from '../../models/basic-study.model';
import { StudyService } from 'src/app/services/study.service';
import { BasicData } from '../../models/basic-data.model';
import { FilterUtils } from '../../filter/filter-util';
import { startWith, debounceTime, switchMap } from 'rxjs/operators';
import { isString } from 'util';
import { FilterService } from '../../filter/filter-service';

@Component({
  selector: 'app-dialog-group-input',
  templateUrl: './dialog-group-input.component.html',
  styleUrls: ['./dialog-group-input.component.css'],
})
export class DialogGroupInputComponent implements OnInit {
  dialogForm: FormGroup;
  filteredOptions: Observable<BasicStudy[]>;


  constructor(
    public dialogRef: MatDialogRef<DialogGroupInputComponent>,
    @Inject(MAT_DIALOG_DATA) public modalData: any,
    public service: GroupService,
    public studyService: StudyService,
    public filterService: FilterService
  ) {}

  ngOnInit() {
    if (this.modalData.editGroup) {
      this.dialogForm = new FormGroup({
        name: new FormControl(this.modalData.group.name, Validators.required),
      });
    } else {
      this.dialogForm = new FormGroup({
        name: new FormControl('', Validators.required),
        study: new FormControl(null, Validators.required)
      });
      this.filterStudies();
    }
  }

  displayFunction(object: BasicData) {
    return FilterUtils.displayFunction(object);
  }

  filterStudies() {
    this.filteredOptions = this.dialogForm.get('study').valueChanges
    .pipe(
      startWith(''),
      debounceTime(300),
      switchMap(value => {
        if (isString(value)) {
          return this.filterService.filterGeneric({name: value}, this.studyService.getStudiesLookup());
        }

        return this.filterService.filterGeneric(value, this.studyService.getStudiesLookup());
      })
    );
  }

  onSubmit() {
    const name = this.dialogForm.get('name').value;
    if (this.modalData.editGroup) {
      this.modalData.group.name = name;
      return this.service.editGroup(this.modalData.group).subscribe((data) => {
        this.dialogRef.close(data);
      });
    }
    const study = this.dialogForm.get('study').value;
    const group = new Group(name, [], study);
    return this.service.addGroup(group).subscribe((data) => {
      this.dialogRef.close(data);
    });
  }
}
