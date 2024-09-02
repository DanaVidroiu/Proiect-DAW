import {Component, OnInit} from '@angular/core';
import {FormBuilder, FormGroup, ReactiveFormsModule, Validators} from "@angular/forms";
import {AuthService} from '../../services/auth.service';
import {Router} from "@angular/router";
import {MatFormField, MatLabel} from "@angular/material/form-field";
import {MatCard, MatCardContent, MatCardTitle} from "@angular/material/card";
import {MatInput} from "@angular/material/input";
import {MatButton} from "@angular/material/button";
import {CommonModule} from "@angular/common";
// import * as console from "node:console";

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [
    MatFormField,
    MatCardContent,
    MatCardTitle,
    MatCard,
    ReactiveFormsModule,
    MatInput,
    MatButton,
    MatLabel,
    CommonModule
  ],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent implements OnInit {
  loginForm: FormGroup;
  errorMessage: string | null = null;

  constructor(private fb: FormBuilder, private authService: AuthService, private router: Router) {
    this.loginForm = this.fb.group({
      username: ['', Validators.required],
      password: ['', Validators.required]
    });
  }

  login(): void {
    if (this.loginForm.valid) {
      this.authService.login(this.loginForm.value).subscribe(
        response => {
          this.authService.saveToken(response.token);
          this.router.navigate(['/jobs']);
        },
        error => {
          console.error('Login failed', error);
          this.errorMessage = "Login failed: " + (error.error.message || "Unknown error");
        }
      );
    }
  }

  ngOnInit(): void {
  }
}