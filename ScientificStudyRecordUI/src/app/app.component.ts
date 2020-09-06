import { Component, ViewEncapsulation, OnInit, OnDestroy } from '@angular/core';
import { AuthenticationService } from './shared/authorization/auth.service';
import { Subscription } from 'rxjs';
import { Router, ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
  encapsulation: ViewEncapsulation.None
})
export class AppComponent implements OnInit, OnDestroy {
  userLoggedIn: boolean;
  subscription: Subscription;

  constructor(
    private authService: AuthenticationService,
    private router: Router,
    private route: ActivatedRoute
  ) {}

  ngOnInit() {
    this.subscription = this.authService.userLoggedIn.subscribe(data => {
      this.userLoggedIn = data;
      // console.log(this.userLoggedIn);
    }
      );
  }

  ngOnDestroy() {
    if (this.subscription) {
      this.subscription.unsubscribe();
    }
  }
  logOut() {
    this.authService.logout();
    this.router.navigate(['../login'], { relativeTo: this.route });
  }
}

