import { AuthserviceService } from './../../services/authservice.service';
import { Component, inject } from '@angular/core';
import { Router, RouterLink, RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { UserRegister } from '../../models/user-register';

@Component({
  selector: 'app-register',
  imports: [FormsModule,RouterModule],
  standalone: true,
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent {
  registerObj: UserRegister = new UserRegister();
  authService = inject(AuthserviceService);
  router = inject(Router);


  RegisterUser() {
    this.authService.Register(this.registerObj).subscribe({
      next: response => {
        (response.status === 200 || response.status === 201)
          ? this.router.navigateByUrl("login")
          : alert(response.status);
      },
      error: err => {
        alert(err?.error.message || 'Failed To Register');
      }
    });
  }
}