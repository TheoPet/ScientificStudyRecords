import { Component, OnInit, Inject } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { StudyService } from 'src/app/services/study.service';
import { Study } from 'src/app/study/study-view/study-view.model';

@Component({
  selector: 'app-dialog-study-input',
  templateUrl: './dialog-study-input.component.html',
  styleUrls: ['./dialog-study-input.component.css']
})
export class DialogStudyInputComponent implements OnInit {
  dialogForm: FormGroup;

  constructor(public dialogRef: MatDialogRef<DialogStudyInputComponent>,
              @Inject(MAT_DIALOG_DATA) public modalData: any,
              public service: StudyService) { }

  ngOnInit() {
    this.dialogForm = new FormGroup({
      name: new FormControl('', Validators.required)
    });
    if (this.modalData.editStudy) {
      this.dialogForm = new FormGroup({
        name: new FormControl(this.modalData.study.name, Validators.required)
      });
    }
  }

  onSubmit() {
    const study = new Study(
      this.dialogForm.get('name').value, [], []);

    if (this.modalData.editStudy) {
      this.modalData.study.name = study.name;
      return this.service.editStudy(this.modalData.study).subscribe(data => {
        this.dialogRef.close(data);
      });
    }

    return this.service.addStudy(study).subscribe( data => {
      this.dialogRef.close(data);
    });
  }
}
