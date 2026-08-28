import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { GenericCrudService } from './generic.service';
import { User, UpdateUserDetailsDto } from '../../models/user';

@Injectable({ providedIn: 'root' })
export class UsersService extends GenericCrudService<User> {
  constructor(http: HttpClient) {
    super(http, 'users');
  }
updateUserDetails(
  username: string,
  departmentId: number,
  userId: string,
  dto: UpdateUserDetailsDto
): Observable<UpdateUserDetailsDto> {
  return this.http.patch<UpdateUserDetailsDto>(
    `${this.baseUrl}/${userId}?updateUsername=${username}&departmentId=${departmentId}`,
    dto
  );
}
getUserByUsername(username: string): Observable<User> {
  const authUrl = this.baseUrl.replace('users', 'auth');
  return this.http.get<User>(`${authUrl}/${username}`);
}
}