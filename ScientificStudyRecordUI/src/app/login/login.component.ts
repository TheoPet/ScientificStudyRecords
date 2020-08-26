import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { AuthenticationService } from '../shared/authorization/auth.service';
import { BasicUserResponse } from './user-response.model';
import { User } from './user.model';
import { Router, ActivatedRoute } from '@angular/router';
import { MatSnackBar } from '@angular/material';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  loginForm: FormGroup;
  loginInvalid: boolean;

  constructor(private authService: AuthenticationService,
              private router: Router,
              private route: ActivatedRoute,
              private snackBar: MatSnackBar) { }

  ngOnInit() {
    this.loginForm = new FormGroup(
      {
        username: new FormControl('', Validators.email),
        password: new FormControl('', Validators.required)
      }
    );
  }


  onSubmit() {
    const email = this.loginForm.get('username').value;
    const password = this.loginForm.get('password').value;
    const userData = new User(email, password);
    this.authService.login(userData).subscribe(
      (response) => {
        this.router.navigate(['../home'], { relativeTo: this.route });
      }
    );
  }

}
