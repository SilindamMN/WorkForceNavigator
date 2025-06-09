import { Component, inject } from '@angular/core';
import { Router } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { UserLogin } from '../../../models/user-login';
import { AuthserviceService } from '../../../services/authservice.service';

@Component({
  selector: 'app-login',
  imports: [FormsModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css',
  standalone: true,  
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