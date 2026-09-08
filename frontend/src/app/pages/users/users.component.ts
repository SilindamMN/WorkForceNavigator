import { Component, inject, OnInit } from '@angular/core';

import {
  TableColumn,
  BasicTableThreeComponent
} from '../../shared/components/tables/basic-tables/basic-table-three/basic-table-three.component';

import { UsersService } from '../../shared/services/users.service';

import {
  UserDto,
  UpdateUserDetailsDto
} from '../../models/user';

import {
  JobtiteserviceService
} from '../../shared/services/jobtiteservice.service';

import {
  GenderOptions
} from '../../models/Constant/enums/gender';

import { Seniority } from '../../models/Constant/enums/seniority';

import { JobTitle } from '../../models/jobtitle';

@Component({
  selector: 'app-users',
  standalone: true,
  imports: [
    BasicTableThreeComponent
  ],
  templateUrl: './users.component.html'
})
export class UsersComponent implements OnInit {

  usersService = inject(UsersService);

  jobTitleService = inject(
    JobtiteserviceService
  );

  jobTitles: JobTitle[] = [];

  users: UserDto[] = [];

  isAdmin = false;

  columns: TableColumn[] = [
    {
      key: 'firstName',
      label: 'First Name'
    },
    {
      key: 'lastName',
      label: 'Last Name'
    },
    {
      key: 'email',
      label: 'Email'
    },
    {
      key: 'username',
      label: 'Username'
    },
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

    const userInfo = JSON.parse(
      localStorage.getItem('userInfo') || '{}'
    );

    this.isAdmin =
      userInfo.roles?.includes('ADMIN') ?? false;

    this.loadUsers();
  }

  getJobTitleByDepartmentId(
    departmentId: number,
    seniority: Seniority
  ): void {

    this.jobTitleService
      .getJobTitleByDepartmentId(
        departmentId,
        seniority
      )
      .subscribe({
        next: (data) => {

          console.log(
            'JOB TITLES FOR DEPARTMENT:',
            data
          );

          this.jobTitles = data;

          const field = this.columns.find(
            column =>
              column.key === 'jobTitleName'
          );

          if (!field) {
            return;
          }

          field.options = data.map(
            (jobTitle: JobTitle) => ({
              value:
                jobTitle.jobTitleId.toString(),

              label:
                jobTitle.title
            })
          );
        },

        error: (error) => {

          console.error(
            'ERROR GETTING JOB TITLES BY DEPARTMENT:',
            error
          );
        }
      });
  }

  loadUsers(): void {

    this.usersService
      .getAll()
      .subscribe({
        next: (data) => {

          this.users = data;

          console.log(
            'USERS:',
            this.users
          );
        },

        error: (error) => {

          console.error(
            'ERROR LOADING USERS:',
            error
          );
        }
      });
  }

  createUser(): void {

    console.log(
      'CREATE USER CLICKED'
    );
  }

  editUser(
    user: UserDto
  ): void {

    console.log(
      'EDIT USER:',
      user
    );

    console.log(
      'DEPARTMENT ID:',
      user.departmentId
    );

    console.log(
      'SENIORITY:',
      user.seniority
    );

    if (
      user.departmentId &&
      user.seniority
    ) {

      this.getJobTitleByDepartmentId(
        user.departmentId,
        user.seniority
      );
    }
  }

  deleteUser(
    user: UserDto
  ): void {

    console.log(
      'DELETE USER:',
      user
    );
  }

  handleSave(
    event: {
      mode: 'add' | 'edit';
      data: UserDto;
    }
  ): void {

    if (
      event.mode === 'add'
    ) {

      this.usersService
        .create(event.data)
        .subscribe({
          next: () => {

            this.loadUsers();
          },

          error: (error) => {

            console.error(
              'CREATE USER FAILED:',
              error
            );
          }
        });

      return;
    }

    const dto: UpdateUserDetailsDto = {

      firstName:
        event.data.firstName,

      lastName:
        event.data.lastName,

      gender:
        event.data.gender ?? '',

      jobTitleId:
        Number(
          event.data.jobTitleId
        ),

      teamId:
        event.data.teamId,

      salary:
        event.data.salary
          ? Number(event.data.salary)
          : 0,

      phonenumber:
        event.data.phoneNumber ?? ''
    };

    this.usersService
      .updateUserDetails(
        event.data.username,
        event.data.departmentId ?? 0,
        event.data.id,
        dto
      )
      .subscribe({
        next: () => {

          this.loadUsers();
        },

        error: (error) => {

          console.error(
            'UPDATE USER FAILED:',
            error
          );
        }
      });
  }
}