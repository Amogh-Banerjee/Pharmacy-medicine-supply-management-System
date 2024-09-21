import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from 'src/Services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  loginForm: FormGroup;

  loginError: boolean = false; // For showing the alert
  loginErrorMessage: string = ''; // Holds the error message

  constructor(private fb: FormBuilder, private authService: AuthService, private router: Router) {
    this.loginForm = this.fb.group({
      username: ['', [Validators.required, Validators.minLength(3)]],
      password: ['', [Validators.required, Validators.minLength(6)]],
    });
  }

  ngOnInit(): void {}

  onSubmit() {
    if (this.loginForm.valid) {
      this.authService.login(this.loginForm.value).subscribe(
        (response: any) => {
          localStorage.setItem('token', response.token);
          this.authService.loginEmit(response.token); // Emit authentication change
          this.router.navigate(['/dashboard']);
          this.loginError = false; // Clear any previous errors
        },
        error => {
          if (error.status === 401) {
            this.loginError = true;
            this.loginErrorMessage = 'Invalid credentials. Please try again.';
          } else {
            this.loginError = true;
            this.loginErrorMessage = 'An error occurred during login. Please try again later.';
          }
        }
      );
    }
  }

  closeAlert() {
    this.loginError = false;
  }

}
