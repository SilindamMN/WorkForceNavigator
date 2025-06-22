import { CommonModule } from '@angular/common';
import { Component, inject } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-sidebar',
  imports: [CommonModule],
  standalone: true,
  templateUrl: './sidebar.component.html',
  styleUrl: './sidebar.component.css'
})
export class SidebarComponent {
  router = inject(Router);

  LoadDepartments() {
    this.router.navigate(['/dashboard/departments']);
  }
  LoadJobTitles() {
    this.router.navigate(['/dashboard/jobtites']);
  }

  LoadUsers() {
    this.router.navigate(['/dashboard/users']);
  }

  LoadJobTitlesa() {
    this.router.navigate(['/dashboard/jobtitles']);
  }

  LoadDashboard() {
    this.router.navigate(['/dashboard']);
  }
}
