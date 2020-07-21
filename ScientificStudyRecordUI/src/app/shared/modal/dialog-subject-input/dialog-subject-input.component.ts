import { Component, OnInit, Inject } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { TestSubject } from 'src/app/test-subject/test-subject-view/test-subject-view.model';
import { GroupService } from 'src/app/services/group.service';

@Component({
  selector: 'app-dialog-subject-input',
  templateUrl: './dialog-subject-input.component.html',
  styleUrls: ['./dialog-subject-input.component.css'],
})
export class DialogSubjectInputComponent implements OnInit {
  dialogForm: FormGroup;

  constructor(
    public dialogRef: MatDialogRef<DialogSubjectInputComponent>,
    @Inject(MAT_DIALOG_DATA) public modalData: any,
    public service: GroupService
  ) {}

  ngOnInit() {
    this.dialogForm = new FormGroup({
      name: new FormControl('', Validators.required),
      surname: new FormControl('', Validators.required),
      entryTime: new FormControl(new Date(), Validators.required),
      comment: new FormControl(''),
    });
  }

  onSubmit() {
    const testSubject = new TestSubject(
      this.dialogForm.get('name').value,
      this.dialogForm.get('surname').value,
      this.dialogForm.get('entryTime').value,
      this.dialogForm.get('comment').value,
      null,
      null
    );

    return this.service.addTestSubject(this.modalData.groupId, testSubject).subscribe( data => {
      this.dialogRef.close(data);
    });
  }
}
