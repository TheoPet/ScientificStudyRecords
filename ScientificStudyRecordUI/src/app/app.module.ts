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
import { DialogDeleteComponent } from './shared/modal/dialog-delete/dialog-delete.component';

import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatButtonModule } from '@angular/material/button';
import { MatDialogModule } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatAutocompleteModule } from '@angular/material/autocomplete';
// tslint:disable-next-line: max-line-length
import { MatDatepickerModule, MatPaginatorModule, MatTableModule, MatListModule, MatIconModule, MatCardModule, MatToolbarModule, MatSidenavModule, MatGridListModule, MatMenuModule, MatExpansionModule, MatSelectModule } from '@angular/material';
import { MatMomentDateModule } from '@angular/material-moment-adapter';
import { TestSubjectViewComponent } from './test-subject/test-subject-view/test-subject-view.component';
import { TestSubjectEditComponent } from './test-subject/test-subject-edit/test-subject-edit.component';
import { HomeComponent } from './home/home.component';
import { TestSubjectStartComponent } from './test-subject/test-subject-start/test-subject-start.component';
import { TestSubjectHomeComponent } from './test-subject/test-subject-home/test-subject-home.component';
import { TestSubjectService } from './services/test-subject.service';
import { CustomDatePipe } from './shared/custom/datepipe';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { ExperimentViewComponent } from './experiment/experiment-view/experiment-view.component';
import { ExperimentStartComponent } from './experiment/experiment-start/experiment-start.component';
import { ExperimentService } from './services/experiment-service';
import { TaskService } from './services/task.service';
import { FilterService } from './shared/filter/filter-service';
import { DialogInputComponent } from './shared/modal/dialog-input/dialog-input.component';
import { GroupComponent } from './group/group.component';
import { DialogSubjectInputComponent } from './shared/modal/dialog-subject-input/dialog-subject-input.component';
import { DialogSubjectListComponent } from './shared/modal/dialog-subject-list/dialog-subject-list.component';
import { DialogExperimentInputComponent } from './shared/modal/dialog-experiment-input/dialog-experiment-input.component';
import {
  NgxMatDatetimePickerModule,
  NgxMatTimepickerModule,
  NgxMatNativeDateModule,
} from '@angular-material-components/datetime-picker';
import { DialogStudyInputComponent } from './shared/modal/dialog-study-input/dialog-study-input.component';
import { DialogGroupInputComponent } from './shared/modal/dialog-group-input/dialog-group-input.component';
import { TaskComponent } from './task/task.component';
import { DialogTaskExperimentInputComponent } from './shared/modal/dialog-task-experiment-input/dialog-task-experiment-input.component';
import { DialogTestSubjectInputComponent } from './shared/modal/dialog-test-subject-input/dialog-test-subject-input.component';
import { DialogStudyAssignComponent } from './shared/modal/dialog-study-assign/dialog-study-assign.component';
import { GroupStartComponent } from './group-start/group-start.component';
import { GroupHomeComponent } from './group-home/group-home.component';
import { StudyHomeComponent } from './study/study-home/study-home.component';

@NgModule({
  declarations: [
    AppComponent,
    StudyViewComponent,
    StudyEditComponent,
    HeaderComponent,
    StudyStartComponent,
    DialogDeleteComponent,
    TestSubjectViewComponent,
    TestSubjectEditComponent,
    HomeComponent,
    TestSubjectStartComponent,
    TestSubjectHomeComponent,
    CustomDatePipe,
    ExperimentViewComponent,
    ExperimentStartComponent,
    DialogInputComponent,
    GroupComponent,
    DialogSubjectInputComponent,
    DialogSubjectListComponent,
    DialogExperimentInputComponent,
    DialogStudyInputComponent,
    DialogGroupInputComponent,
    TaskComponent,
    DialogTaskExperimentInputComponent,
    DialogTestSubjectInputComponent,
    DialogStudyAssignComponent,
    GroupStartComponent,
    GroupHomeComponent,
    StudyHomeComponent,
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
    MatPaginatorModule,
    MatInputModule,
    MatTableModule,
    MatDatepickerModule,
    MatMomentDateModule,
    MatAutocompleteModule,
    MatCardModule,
    MatToolbarModule,
    MatListModule,
    MatIconModule,
    MatSidenavModule,
    MatGridListModule,
    NgxMatDatetimePickerModule,
    NgxMatTimepickerModule,
    NgxMatNativeDateModule,
    MatMenuModule,
    MatExpansionModule,
    MatSelectModule
  ],
  providers: [
    StudyService,
    TestSubjectService,
    ExperimentService,
    TaskService,
    FilterService,
    MatDatepickerModule,
  ],
  bootstrap: [AppComponent],
  entryComponents: [
    DialogDeleteComponent,
    DialogInputComponent,
    DialogSubjectInputComponent,
    DialogSubjectListComponent,
    DialogExperimentInputComponent,
    DialogStudyInputComponent,
    DialogGroupInputComponent,
    DialogTaskExperimentInputComponent,
    DialogTestSubjectInputComponent,
    DialogStudyAssignComponent
  ],
})
export class AppModule {}
