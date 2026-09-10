import { CommonModule } from '@angular/common';
import { Component, OnInit, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { ModalComponent } from '../../shared/components/ui/modal/modal.component';

@Component({
  selector: 'app-user-profile',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    ModalComponent
  ],
  templateUrl: './userprofile.component.html',
})
export class UserProfileComponent implements OnInit {

  private http = inject(HttpClient);

  apiUrl = 'https://workforcenavigatorexpress.runasp.net/api';

  user: any = {};

  username = '';

  // Modal
  isModalOpen = false;

  editSection = '';

  // Editable user
  editUser: any = {};

  ngOnInit(): void {
    this.loadUser();
  }

  loadUser(): void {

    const storedUser = localStorage.getItem('userInfo');

    if (!storedUser) {
      console.error('No userInfo found in localStorage');
      return;
    }

    try {

      const userInfo = JSON.parse(storedUser);

      this.username = userInfo.username;

      this.http
        .get<any>(
          `${this.apiUrl}/users/${this.username}`
        )
        .subscribe({

          next: (data) => {

            console.log('USER PROFILE:', data);

            this.user = data;

          },

          error: (error) => {

            console.error(
              'ERROR LOADING USER PROFILE:',
              error
            );

            this.user = userInfo;

          }

        });

    } catch (error) {

      console.error(
        'ERROR READING USER INFO:',
        error
      );

    }

  }

  openEdit(section: string): void {

    this.editSection = section;

    this.editUser = {
      ...this.user
    };

    this.isModalOpen = true;
  }

  closeModal(): void {

    this.isModalOpen = false;

    this.editSection = '';

    this.editUser = {};
  }

  saveChanges(): void {

    if (!this.user?.id) {
      console.error('User ID not found');
      return;
    }

    const updateData = {
      firstName: this.editUser.firstName,
      lastName: this.editUser.lastName,
      email: this.editUser.email,
      phoneNumber: this.editUser.phoneNumber,
      gender: this.editUser.gender,
      updateUsername: this.editUser.username,
      departmentId: this.editUser.departmentId
    };

    console.log('UPDATE USER:', updateData);

    this.http
      .patch<any>(
        `${this.apiUrl}/users/${this.user.id}`,
        updateData
      )
      .subscribe({

        next: (data) => {

          console.log('USER UPDATED:', data);

          this.user = {
            ...this.user,
            ...this.editUser
          };

          const storedUser =
            localStorage.getItem('userInfo');

          if (storedUser) {

            try {

              const userInfo = JSON.parse(storedUser);

              const updatedUserInfo = {
                ...userInfo,
                ...this.editUser
              };

              localStorage.setItem(
                'userInfo',
                JSON.stringify(updatedUserInfo)
              );

            } catch (error) {

              console.error(
                'ERROR UPDATING LOCAL USER:',
                error
              );

            }

          }

          this.closeModal();

        },

        error: (error) => {

          console.error(
            'ERROR UPDATING USER:',
            error
          );

        }

      });

  }

  getInitials(): string {

    const firstName =
      this.user?.firstName?.charAt(0) ?? '';

    const lastName =
      this.user?.lastName?.charAt(0) ?? '';

    return (
      firstName +
      lastName
    ).toUpperCase();

  }

}