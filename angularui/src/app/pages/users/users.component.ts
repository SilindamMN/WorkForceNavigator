
import { Component, inject, OnInit } from '@angular/core';
import { UsersService } from '../../services/Users/users.service';
import { User } from '../../models/Users/User';
import { CommonModule } from '@angular/common';


@Component({
  selector: 'app-users',
  imports: [CommonModule],
  standalone: true,
  templateUrl: './users.component.html',
  styleUrl: './users.component.css'
})
export class UsersComponent implements OnInit {

  userService = inject(UsersService);
  users:User[] = [];

  ngOnInit(): void {
    this.GetUserList();
  }
  
  GetUserList() {
  this.userService.GetAllUsers().subscribe({
    next: (response) => {
      this.users = response;

      setTimeout(() => {
        if ($('#userTable').length) {
          ($('#userTable') as any).DataTable();
        }
      }, 0);
    },
    error: (err) => {
      console.error('Error fetching users:', err);
    }
  });
}
}