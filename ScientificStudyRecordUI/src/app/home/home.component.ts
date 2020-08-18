import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
import { Observable } from 'rxjs';
import { BasicStudy } from '../shared/models/basic-study.model';
import { BasicGroup } from '../shared/models/basic-group.model';
import { BasicData } from '../shared/models/basic-data.model';
import { FilterUtils } from '../shared/filter/filter-util';
import { BasicTestSubject } from '../shared/models/basic-test-subject.model';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  searchForm: FormGroup;
  filteredTestSubjectOptions: Observable<BasicTestSubject[]>;
  filteredStudyOptions: Observable<BasicStudy[]>;
  filteredGroupOptions: Observable<BasicGroup[]>;
  constructor() { }

  ngOnInit() {
    this.searchForm = new FormGroup({
      studySearch: new FormControl(''),
      groupSearch: new FormControl(''),
      testSubjectSearch: new FormControl('')
    });
  }

  addExperiment() {}

  displayFunction(data: BasicData) {
    return FilterUtils.displayFunction(data);
  }
}
