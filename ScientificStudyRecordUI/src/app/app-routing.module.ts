import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { StudyViewComponent } from './study/study-view/study-view.component';
import { StudyEditComponent } from './study/study-edit/study-edit.component';
import { StudyStartComponent } from './study/study-start/study-start.component';


const appRoutes: Routes = [
    { path: '', redirectTo: '/study', pathMatch: 'full'},
    { path: 'study', component: StudyStartComponent,  children: [
      { path: 'new', component: StudyEditComponent },
      { path: ':id', component: StudyViewComponent },
      { path: ':id/edit', component: StudyEditComponent },
    ], },
  ];

@NgModule({
    imports: [RouterModule.forRoot(appRoutes)],
    exports: [RouterModule]
  })
  export class AppRoutingModule {

  }
