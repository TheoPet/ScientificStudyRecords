import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { StudyViewComponent } from './study/study-view/study-view.component';
import { StudyEditComponent } from './study/study-edit/study-edit.component';
import { StudyStartComponent } from './study/study-start/study-start.component';
import { TestSubjectEditComponent } from './test-subject/test-subject-edit/test-subject-edit.component';
import { TestSubjectViewComponent } from './test-subject/test-subject-view/test-subject-view.component';
import { HomeComponent } from './home/home.component';
import { StudyHomeComponent } from './study/study-home/study-home.component';
import { TestSubjectStartComponent } from './test-subject/test-subject-start/test-subject-start.component';
import { TestSubjectHomeComponent } from './test-subject/test-subject-home/test-subject-home.component';
import { ExperimentStartComponent } from './experiment/experiment-start/experiment-start.component';
import { ExperimentEditComponent } from './experiment/experiment-edit/experiment-edit.component';
import { ExperimentViewComponent } from './experiment/experiment-view/experiment-view.component';
import { GroupComponent } from './group/group.component';


const appRoutes: Routes = [
  // { path: '', redirectTo: '/home', pathMatch: 'full' },
  // { path: 'home', component: HomeComponent },
  { path: '', redirectTo: '/studies', pathMatch: 'full' },
  {
    path: 'studies',
    component: StudyHomeComponent,
    children: [
      { path: '', component: StudyStartComponent },
      { path: 'new', component: StudyEditComponent },
      { path: ':id', component: StudyViewComponent },
      { path: ':id/edit', component: StudyEditComponent },
    ],
  },
  {
    path: 'testsubjects',
    component: TestSubjectHomeComponent,
    children: [
      { path: '', component: TestSubjectStartComponent },
      { path: 'new', component: TestSubjectEditComponent },
      { path: ':id', component: TestSubjectViewComponent },
      { path: ':id/edit', component: TestSubjectEditComponent },
    ],
  },
  {
    path: 'groups/:id', component: GroupComponent,
  },
  {
    path: 'experiments',
    component: ExperimentStartComponent,
    children: [
      { path: '', component: ExperimentStartComponent },
      { path: 'new', component: ExperimentEditComponent },
      { path: ':id', component: ExperimentViewComponent },
      { path: ':id/edit', component: ExperimentEditComponent },
    ],
  },
];

@NgModule({
    imports: [RouterModule.forRoot(appRoutes)],
    exports: [RouterModule]
  })
  export class AppRoutingModule {

  }
