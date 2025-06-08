import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Constant } from '../constant';
import { UserRegister } from '../models/user-register';

@Injectable({
  providedIn: 'root'
})
export class AuthserviceService {

  constructor(private http:HttpClient) 
  { }

  Register(user: UserRegister):Observable<any>{
    return this.http.post(`${Constant.ApiUrl}Auth/register`,user,{observe:'response'});
  }
}
