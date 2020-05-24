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


@NgModule({
  declarations: [
    AppComponent,
    StudyViewComponent,
    StudyEditComponent,
    HeaderComponent,
    StudyStartComponent,
    ModalComponent
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
    MatDialogModule
    ],
  providers: [StudyService],
  bootstrap: [AppComponent],
  entryComponents: [ModalComponent]

})
export class AppModule { }
