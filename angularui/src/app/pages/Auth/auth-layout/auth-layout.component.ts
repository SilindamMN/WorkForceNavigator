import { Component } from '@angular/core';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-auth-layout',
  standalone: true,
  imports: [RouterModule],
  template: `
    <div class="auth-container">
      <h2>Auth Layout Loaded</h2>
      <router-outlet></router-outlet>
    </div>
  `
})
export class AuthLayoutComponent {}
