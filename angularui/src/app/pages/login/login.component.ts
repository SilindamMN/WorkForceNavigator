import { Component, inject } from '@angular/core';
import { UserLogin } from '../../models/user-login';
import { AuthserviceService } from '../../services/authservice.service';
import { Router, RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-login',
  imports: [FormsModule,CommonModule,RouterModule],
  standalone: true,
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {
  loginObj : UserLogin = new UserLogin();
  authService = inject(AuthserviceService);
  router = inject(Router);

UserLogin() {
  this.authService.Login(this.loginObj).subscribe({
    next: response => {
      this.authService.saveAuthData(response);
      this.router.navigateByUrl('/dashboard');
    },
    error: err => {
      alert(err?.error?.message || 'Login failed. Please try again.');
    }
  });
}
}