import { Component, OnInit, Inject } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { TestSubject } from 'src/app/test-subject/test-subject-view/test-subject-view.model';
import { GroupService } from 'src/app/services/group.service';
import { TestSubjectService } from 'src/app/services/test-subject.service';

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
    public groupService: GroupService,
    public testSubjectService: TestSubjectService
  ) {}

  ngOnInit() {
    this.dialogForm = new FormGroup({
      name: new FormControl('', Validators.required),
      surname: new FormControl('', Validators.required),
      entryTime: new FormControl(new Date(), Validators.required),
      comment: new FormControl(''),
    });

    if (this.modalData.editTestSubject) {
      this.dialogForm = new FormGroup({
        name: new FormControl(this.modalData.testSubject.name, Validators.required),
        surname: new FormControl(this.modalData.testSubject.surname, Validators.required),
        entryTime: new FormControl(new Date(this.modalData.testSubject.entryTime), Validators.required),
        comment: new FormControl(this.modalData.testSubject.comment),
      });
    }
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

    if (this.modalData.editTestSubject) {
      this.modalData.testSubject.name = testSubject.name;
      this.modalData.testSubject.surname = testSubject.surname;
      this.modalData.testSubject.entryTime = testSubject.entryTime;
      this.modalData.testSubject.comment = testSubject.comment;

      return this.testSubjectService.editTestSubject(this.modalData.testSubject).subscribe(data => {
        this.dialogRef.close(data);
      });
    }
    return this.groupService.addTestSubject(this.modalData.groupId, testSubject).subscribe( data => {
      this.dialogRef.close(data);
    });
  }
}
