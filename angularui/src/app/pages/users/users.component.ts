import { Component, inject, OnInit, AfterViewInit, ViewChild } from '@angular/core';
import { UsersService } from '../../services/Users/users.service';
import { User } from '../../models/Users/User';
import { CommonModule } from '@angular/common';
import { ModalField } from '../generic/basemodal/basemodal.component';
import { AddModalComponent } from '../generic/addmodal/addmodal.component';
import { EditModalComponent } from '../generic/editmodal/editmodal.component';
import { DetaisModalComponent } from '../generic/detailsmodal/detailsmodal.component';

@Component({
  selector: 'app-users',
  standalone: true,
  imports: [CommonModule, AddModalComponent, EditModalComponent, DetaisModalComponent],
  templateUrl: './users.component.html',
  styleUrl: './users.component.css'
})
export class UsersComponent implements OnInit, AfterViewInit {

  @ViewChild('addModal') addModal!: AddModalComponent;
  @ViewChild('editModal') editModal!: EditModalComponent;
  @ViewChild('viewModal') viewModal!: DetaisModalComponent;


  userService = inject(UsersService);
  users: User[] = [];
  isDataLoaded = false;

  userFields: ModalField[] = [
    { name: 'firstName', label: 'First Name', type: 'text', required: true },
    { name: 'lastName', label: 'Last Name', type: 'text', required: true },
    { name: 'email', label: 'Email', type: 'email', required: true },
    { name: 'isAdmin', label: 'Administrator', type: 'checkbox', defaultValue: false }
  ];

  editUser: User | null = null;
  editModalTitle: string = 'Edit User';

  ngOnInit(): void {
    this.GetUserList();
  }

  ngAfterViewInit(): void {
    const interval = setInterval(() => {
      if (this.isDataLoaded && $('#userTable').length) {
        ($('#userTable') as any).DataTable();
        clearInterval(interval);
      }
    }, 100);
  }

  GetUserList() {
    this.userService.GetAllUsers().subscribe({
      next: (response) => {
        this.users = response;
        this.isDataLoaded = true;
      },
      error: (err) => {
        console.error('Error fetching users:', err);
      }
    });
  }

  openEditUserModal(user: User) {
    this.editUser = { ...user }; // Pass a copy
    this.editModalTitle = `Edit User: ${user.firstName} ${user.lastName}`;
    this.editModal.open();
  }

  handleAddUser(newUser: any) {
   /* console.log('Add user:', newUser);
    this.userService.AddUser(newUser).subscribe({
      next: () => this.GetUserList(),
      error: (err) => console.error('Error adding user:', err)
    });*/
  }

  handleUpdateUser(updatedUser: any) {
    if (!this.editUser) return;

    const updated = { ...this.editUser, ...updatedUser };

    /*this.userService.UpdateUser(updated).subscribe({
      next: () => this.GetUserList(),
      error: (err) => console.error('Error updating user:', err)
    });*/
  }
  selectedUser: any;

openViewModal(user: any) {
  this.selectedUser = user;
  this.viewModal.open();  // You need to @ViewChild('viewModal') viewModal!: ViewModalComponent;
}
}
