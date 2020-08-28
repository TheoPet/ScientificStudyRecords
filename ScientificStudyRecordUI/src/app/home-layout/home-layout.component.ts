import { Component, OnInit, OnDestroy } from '@angular/core';
import { AuthenticationService } from '../shared/authorization/auth.service';
import { Subscription, Observable, BehaviorSubject } from 'rxjs';
import { Router, ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-home-layout',
  templateUrl: './home-layout.component.html',
  styleUrls: ['./home-layout.component.css'],
})
export class HomeLayoutComponent implements OnInit, OnDestroy {
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
      console.log(this.userLoggedIn);
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
