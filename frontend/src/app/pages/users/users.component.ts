import { Component, inject, OnInit } from '@angular/core';

import {
  TableColumn,
  BasicTableThreeComponent
} from '../../shared/components/tables/basic-tables/basic-table-three/basic-table-three.component';

import { UsersService } from '../../shared/services/users.service';
import { UserDto } from '../../models/user';
import { JobtiteserviceService } from '../../shared/services/jobtiteservice.service';
import { GenderOptions } from '../../models/Constant/enums/gender';

@Component({
  selector: 'app-users',
  standalone: true,
  imports: [BasicTableThreeComponent],
  templateUrl: './users.component.html'
})
export class UsersComponent implements OnInit {

  usersService = inject(UsersService);
  jobTitleService = inject(JobtiteserviceService);

  users: UserDto[] = [];

  isAdmin = false;

  columns: TableColumn[] = [
    { key: 'firstName', label: 'First Name' },
    { key: 'lastName', label: 'Last Name' },
    { key: 'email', label: 'Email' },
    { key: 'username', label: 'Username' },

    {
      key: 'jobTitleName',
      label: 'Job Title',
      type: 'select',
      valueKey: 'jobTitleId',
      options: []
    },
{
  key: 'gender',
  label: 'Gender',
  type: 'select',
  options: GenderOptions.map(gender => ({
    value: gender,
    label: gender
  }))
}
  ];

  ngOnInit(): void {

    const roles = JSON.parse(
      localStorage.getItem('userInfo') || '{}'
    );

    this.isAdmin = roles.roles?.includes('ADMIN') ?? false;

    this.loadUsers();
    this.getJobTitles();
  }

  getJobTitles(): void {

    this.jobTitleService.getAll().subscribe(jobTitles => {

      const jobTitleColumn = this.columns.find(
        column => column.key === 'jobTitleName'
      );

      if (!jobTitleColumn) {
        return;
      }

      jobTitleColumn.options = jobTitles.map(jobTitle => ({
        value: jobTitle.jobTitleId.toString(),
        label: jobTitle.title
      }));

      console.log(
        'JOB TITLE OPTIONS:',
        jobTitleColumn.options
      );
    });
  }

  loadUsers(): void {

    this.usersService.getAll().subscribe(data => {

      this.users = data;

      console.log('USERS:', this.users);
    });
  }

  createUser(): void {
    console.log('Create User');
  }

  editUser(user: UserDto): void {

    console.log('Edit User:', user);

    console.log(
      'Existing Job Title:',
      'ID:',
      user.jobTitleId
    );
  }

  deleteUser(user: UserDto): void {
    console.log('Delete User:', user);
  }

  handleSave(event: { mode: 'add' | 'edit'; data: any }): void {

    console.log('SAVE DATA:', event.data);

    if (event.mode === 'add') {

      this.usersService
        .create(event.data)
        .subscribe(() => this.loadUsers());

    } else {

      this.usersService
        .update(event.data.id, event.data)
        .subscribe(() => this.loadUsers());
    }
  }
}