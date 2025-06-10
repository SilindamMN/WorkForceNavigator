import { Component, inject } from '@angular/core';
import { AuthserviceService } from '../../services/authservice.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-navbar',
  imports: [],
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.css'
})
export class NavbarComponent {

 authService = inject(AuthserviceService);
 router = inject(Router);

  logout(){
      this.authService.logout();
      this.router.navigateByUrl('/login');
  }
}
