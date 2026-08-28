import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, Router } from '@angular/router';

@Injectable({ providedIn: 'root' })
export class AuthGuard implements CanActivate {

  constructor(private router: Router) { }

  canActivate(route: ActivatedRouteSnapshot): boolean {

    const token = localStorage.getItem('token');
    const tokens = localStorage.getItem('departments');
    const roles = JSON.parse(localStorage.getItem('userInfo') || '[]');

    if (!token) {
      this.router.navigate(['/login']);
      return false;
    }

    const requiredRole = route.data['roles'];

    console.log(roles);
    console.log(token);

    if (requiredRole && !roles.includes(requiredRole)) {
      this.router.navigate(['/']);
      return false;
    }

    return true;
  }
}