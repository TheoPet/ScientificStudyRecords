import { Component, OnInit } from '@angular/core';
import { FormGroup } from '@angular/forms';

@Component({
  selector: 'app-experiment-edit',
  templateUrl: './experiment-edit.component.html',
  styleUrls: ['./experiment-edit.component.css']
})
export class ExperimentEditComponent implements OnInit {
  experimentForm: FormGroup;
  constructor() { }

  ngOnInit() {
  }

  onSubmit() {
  }
}
