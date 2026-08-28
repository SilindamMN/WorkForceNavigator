import { CommonModule } from '@angular/common';
import { Component, inject, OnInit } from '@angular/core';
import { TableColumn, BasicTableThreeComponent } from '../../shared/components/tables/basic-tables/basic-table-three/basic-table-three.component';
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

  columns: TableColumn[] = [
    { key: 'firstName', label: 'First Name' },
    { key: 'lastName', label: 'Last Name' },
    { key: 'email', label: 'Email' },
    { key: 'username', label: 'Username' },
    { key: 'jobTitle', label: 'Job Title' },
    { key: 'gender', label: 'Gender' }
  ];

  ngOnInit(): void {
    this.loadUsers();
  }

  loadUsers(): void {
    this.usersService.getAll().subscribe(data => {
      this.users = data;
    });
  }

  viewUser(user: any): void {
    console.log('View User:', user);
  }

  deleteUser(user: any): void {
    console.log('Delete User:', user);
  }
}