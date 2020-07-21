import { Component, OnInit, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { StudyService } from 'src/app/services/study.service';
import { BasicTask } from '../../models/basic-task.model';
import { BasicGroup } from '../../models/basic-group.model';

@Component({
  selector: 'app-dialog-input',
  templateUrl: './dialog-input.component.html',
  styleUrls: ['./dialog-input.component.css'],
})
export class DialogInputComponent implements OnInit {

  dialogForm: FormGroup;

  constructor(
    public dialogRef: MatDialogRef<DialogInputComponent>,
    @Inject(MAT_DIALOG_DATA) public modalData: any,
    public service: StudyService
  ) {
  }

  ngOnInit() {
    this.dialogForm = new FormGroup({
      name: new FormControl(null, Validators.required)
    });
  }

  onSubmit() {
    const name = this.dialogForm.get('name').value;
    if (this.modalData.group) {
      const group = new BasicGroup(name);
      return this.service.addGroupOrTask(this.modalData.studyId, group, true).subscribe(data => {
        this.dialogRef.close(data);
      });
    } else {
      const task = new BasicTask(name);
      return this.service.addGroupOrTask(this.modalData.studyId, task).subscribe(data => {
        this.dialogRef.close(data);
      });
    }
  }


}


