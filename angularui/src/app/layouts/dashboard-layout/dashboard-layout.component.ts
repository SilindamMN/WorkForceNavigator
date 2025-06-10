import { Component } from '@angular/core';
import { RouterModule } from '@angular/router';
import { SidebarComponent } from '../../pages/sidebar/sidebar.component';
import { NavbarComponent } from '../../pages/navbar/navbar.component';

@Component({
  selector: 'app-dashboard-layout',
  imports: [RouterModule, SidebarComponent, NavbarComponent],
  standalone: true,
  providers: [],
  templateUrl: './dashboard-layout.component.html',
  styleUrl: './dashboard-layout.component.css'
})
export class DashboardLayoutComponent {
}
