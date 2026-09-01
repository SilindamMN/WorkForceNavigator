import { Component, inject, OnInit } from '@angular/core';

import {
  TableColumn,
  BasicTableThreeComponent
} from '../../shared/components/tables/basic-tables/basic-table-three/basic-table-three.component';

import { UsersService } from '../../shared/services/users.service';
import { UserDto } from '../../models/user';

@Component({
  selector: 'app-users',
  standalone: true,
  imports: [BasicTableThreeComponent],
  templateUrl: './users.component.html'
})
export class UsersComponent implements OnInit {

  usersService = inject(UsersService);

  users: UserDto[] = [];

  isAdmin = false;

  columns: TableColumn[] = [
    { key: 'firstName', label: 'First Name' },
    { key: 'lastName', label: 'Last Name' },
    { key: 'email', label: 'Email' },
    { key: 'username', label: 'Username' },
    { key: 'jobTitle', label: 'Job Title' },
    { key: 'gender', label: 'Gender' }
  ];

  ngOnInit(): void {
    const roles = JSON.parse(
      localStorage.getItem('userInfo') || '[]'
    );
    this.isAdmin = roles.roles.includes('ADMIN');
    this.loadUsers();
  }

  loadUsers(): void {
    this.usersService.getAll().subscribe(data => {
      this.users = data;
    });
  }

  createUser(): void {
    console.log('Create User');
  }

  editUser(user: any): void {
    console.log('Edit User:', user);
  }

  deleteUser(user: any): void {
    console.log('Delete User:', user);
  }

  handleSave(event: { mode: 'add' | 'edit'; data: any }): void {
    if (event.mode === 'add') {
      this.usersService.create(event.data).subscribe(() => this.loadUsers());
    } else {
      this.usersService.update(event.data.id, event.data).subscribe(() => this.loadUsers());
    }
  }
}