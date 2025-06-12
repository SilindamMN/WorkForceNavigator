import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { User } from '../../models/Users/User';
import { Constant } from '../../constant';

@Injectable({
  providedIn: 'root'
})
export class UsersService {

  private Url = Constant.ApiUrl + 'Auth/users';

  constructor(private http: HttpClient) { }

  GetAllUsers():Observable<User[]> {
    return this.http.get<User[]>(this.Url);
  }
}