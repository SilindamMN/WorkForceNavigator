import { HttpClient, HttpResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { Constant } from '../constant';
import { UserRegister } from '../models/user-register';
import { UserLogin } from '../models/user-login';
import { LoginServiceResponseDto } from '../interface/UserInfoResult';

@Injectable({
  providedIn: 'root'
})
export class AuthserviceService {

  private tokenKey= 'token';
  private userInfoKey = 'userInfo';

    // Observable to track user state
  private userSubject = new BehaviorSubject<any>(this.getUserFromStorage());
  user$ = this.userSubject.asObservable();

  constructor(private http:HttpClient) 
  { }

  Register(user: UserRegister):Observable<any>{
    return this.http.post(`${Constant.ApiUrl}Auth/register`,user,{observe:'response'});
  }

  Login(user:UserLogin):Observable<LoginServiceResponseDto>{
      return this.http.post<LoginServiceResponseDto>(`${Constant.ApiUrl}Auth/login`,user);
  }

  saveAuthData(loginResponse:LoginServiceResponseDto): void{
    localStorage.setItem(this.tokenKey,loginResponse.newToken);
    localStorage.setItem(this.userInfoKey,JSON.stringify(loginResponse.userInfo));
    this.userSubject.next(this.userInfoKey);
  }

  getToken():string|null{
    return localStorage.getItem(this.tokenKey);
  }

  getUserFromStorage():any{
    const userJson = localStorage.getItem(this.tokenKey);
    return userJson? JSON.parse(userJson) : null;
  }

  logout(){
    localStorage.removeItem(this.tokenKey);
    localStorage.removeItem(this.userInfoKey);
    this.userSubject.next(null);
  }
}