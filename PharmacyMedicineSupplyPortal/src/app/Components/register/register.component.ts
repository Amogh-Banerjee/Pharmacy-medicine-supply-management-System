import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AuthService } from 'src/Services/auth.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  registerForm: FormGroup;

  registerError: boolean = false; // For showing the alert
  registerErrorMessage: string = ''; // Holds the error message
  registerSuccess: boolean = false; // For showing the alert
  registerSuccessMessage: string = ''; // Holds the message

  constructor(private fb: FormBuilder, private authService: AuthService) {
    this.registerForm = this.fb.group({
      username: ['', [Validators.required, Validators.minLength(3)]],
      password: ['', [Validators.required, Validators.minLength(6)]],
    });
  }

  ngOnInit(): void { }

  onSubmit() {
    if (this.registerForm.valid) {
      this.authService.register(this.registerForm.value).subscribe(
        response => {
          console.log('User registered', response);
          this.closeAlert();
          this.registerSuccess = true;
          this.registerSuccessMessage = 'Registration Successful.';
        },
        error => {
          this.closeAlert();
          if (error.status === 409) {
            this.registerError = true;
            this.registerErrorMessage = 'Username already exists.';
          } else {
            this.registerError = true;
            this.registerErrorMessage = 'An error occurred during registration. Please try again later.';
          }
        }
      );
    }
  }

  closeAlert() {
    this.registerError = false;
    this.registerSuccess = false;
  }

}
