import { Component, ViewEncapsulation } from '@angular/core';
import { AuthenticationService } from './shared/authorization/auth.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
  encapsulation: ViewEncapsulation.None
})
export class AppComponent {
  title = 'ScientificStudyRecordUI';
  loggedIn: false;

  constructor(private authService: AuthenticationService) {}

}

