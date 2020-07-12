import { Component, OnInit, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { FormGroup, FormControl, Validators } from '@angular/forms';

@Component({
  selector: 'app-dialog-input',
  templateUrl: './dialog-input.component.html',
  styleUrls: ['./dialog-input.component.css'],
})
export class DialogInputComponent implements OnInit {

  dialogForm: FormGroup;

  constructor(
    public dialogRef: MatDialogRef<DialogInputComponent>,
    @Inject(MAT_DIALOG_DATA) public modalData: any
  ) {
    console.log(modalData);
  }

  ngOnInit() {
    this.dialogForm = new FormGroup({
      name: new FormControl(null, Validators.required)
    });
  }
}


