import { Component, inject, OnInit, AfterViewInit } from '@angular/core';
import { UsersService } from '../../services/Users/users.service';
import { User } from '../../models/Users/User';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-users',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './users.component.html',
  styleUrl: './users.component.css'
})
export class UsersComponent implements OnInit, AfterViewInit {

  userService = inject(UsersService);
  users: User[] = [];
  isDataLoaded = false;

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
}
