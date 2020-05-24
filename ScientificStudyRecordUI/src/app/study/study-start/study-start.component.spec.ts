/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { StudyStartComponent } from './study-start.component';

describe('StudyStartComponent', () => {
  let component: StudyStartComponent;
  let fixture: ComponentFixture<StudyStartComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ StudyStartComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(StudyStartComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
