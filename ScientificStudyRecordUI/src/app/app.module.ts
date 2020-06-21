import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';


import { AppComponent } from './app.component';
import { StudyViewComponent } from './study/study-view/study-view.component';
import { StudyEditComponent } from './study/study-edit/study-edit.component';
import { HeaderComponent } from './shared/header/header.component';
import { AppRoutingModule } from './app-routing.module';
import { StudyService } from './services/study.service';
import { StudyStartComponent } from './study/study-start/study-start.component';
import { ModalComponent } from './shared/modal/modal.component';

import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatButtonModule } from '@angular/material/button';
import { MatDialogModule } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatAutocompleteModule } from '@angular/material/autocomplete';
import { MatDatepickerModule, MatNativeDateModule } from '@angular/material';
import { MatMomentDateModule } from '@angular/material-moment-adapter';
import { TestSubjectViewComponent } from './test-subject/test-subject-view/test-subject-view.component';
import { TestSubjectEditComponent } from './test-subject/test-subject-edit/test-subject-edit.component';
import { HomeComponent } from './home/home.component';
import { TestSubjectStartComponent } from './test-subject/test-subject-start/test-subject-start.component';
import { StudyHomeComponent } from './study/study-home/study-home.component';
import { TestSubjectHomeComponent } from './test-subject/test-subject-home/test-subject-home.component';
import { TestSubjectService } from './services/test-subject.service';
import { CustomDatePipe } from './shared/custom/datepipe';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';


@NgModule({
  declarations: [
    AppComponent,
    StudyViewComponent,
    StudyEditComponent,
    HeaderComponent,
    StudyStartComponent,
    ModalComponent,
    TestSubjectViewComponent,
    TestSubjectEditComponent,
    HomeComponent,
    TestSubjectStartComponent,
    StudyHomeComponent,
    TestSubjectHomeComponent,
    CustomDatePipe
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    AppRoutingModule,
    NgbModule,
    BrowserAnimationsModule,
    MatButtonModule,
    MatDialogModule,
    FontAwesomeModule,
    MatFormFieldModule,
    MatInputModule,
    MatDatepickerModule,
    MatMomentDateModule,
    MatAutocompleteModule
    ],
  providers: [StudyService,
    TestSubjectService,
    MatDatepickerModule
  ],
  bootstrap: [AppComponent],
  entryComponents: [ModalComponent],
})
export class AppModule {}
