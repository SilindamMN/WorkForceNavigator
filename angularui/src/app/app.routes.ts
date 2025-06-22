import { Routes } from '@angular/router';
import { AuthGuard } from './AuthGuards/auth.guard';
import { GuestGuard } from './AuthGuards/guest.guard'; // <-- Import
import { AuthLayoutComponent } from './layouts/auth-layout/auth-layout.component';
import { DashboardLayoutComponent } from './layouts/dashboard-layout/dashboard-layout.component';
import { DashboardComponent } from './pages/dashboard/dashboard.component';
import { LoginComponent } from './pages/login/login.component';
import { RegisterComponent } from './pages/register/register.component';
import { UsersComponent } from './pages/users/users.component';
import { DepartmentComponent } from './pages/department/department.component';
import { JobtitesComponent } from './pages/jobtites/jobtites.component';

export const routes: Routes = [
  {
    path: '',
    component: AuthLayoutComponent,
    children: [
      { path: 'login', component: LoginComponent, canActivate: [GuestGuard] },
      { path: 'register', component: RegisterComponent, canActivate: [GuestGuard] },
      { path: '', redirectTo: 'dashboard', pathMatch: 'full' }
    ]
  },
  {
    path: 'dashboard',
    component: DashboardLayoutComponent,
    canActivate: [AuthGuard],
    children: [
      { path: '', component: DashboardComponent },
      { path: 'departments', component: DepartmentComponent },
      { path: 'jobtites', component: JobtitesComponent },
      { path: 'users', component: UsersComponent }
    ]
  },
  { path: '**', redirectTo: 'dashboard' }
];
