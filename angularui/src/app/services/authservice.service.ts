import { HttpClient } from '@angular/common/http';
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

  private tokenKey = 'token';
  private userInfoKey = 'userInfo';

  // Observable to track user state
  private userSubject = new BehaviorSubject<any>(this.getUserFromStorage());
  user$ = this.userSubject.asObservable();

  constructor(private http: HttpClient) {}

  Register(user: UserRegister): Observable<any> {
    return this.http.post(`${Constant.ApiUrl}register`, user, { observe: 'response' });
  }

  Login(user: UserLogin): Observable<LoginServiceResponseDto> {
    return this.http.post<LoginServiceResponseDto>(`${Constant.ApiUrl}login`, user);
  }

  saveAuthData(loginResponse: LoginServiceResponseDto): void {
    localStorage.setItem(this.tokenKey, loginResponse.newToken);
    localStorage.setItem(this.userInfoKey, JSON.stringify(loginResponse.userInfo));
    this.userSubject.next(loginResponse.userInfo);
  }

  getToken(): string | null {
    return localStorage.getItem(this.tokenKey);
  }

  getUserFromStorage(): any {
    const userJson = localStorage.getItem(this.userInfoKey);
    return userJson ? JSON.parse(userJson) : null;
  }

  logout(): void {
    localStorage.removeItem(this.tokenKey);
    localStorage.removeItem(this.userInfoKey);
    this.userSubject.next(null);
  }

  isTokenExpired(token: string): boolean {
  try {
    const payload = JSON.parse(atob(token.split('.')[1]));
    const expiry = payload.exp;
    return Math.floor(Date.now() / 1000) >= expiry;
  } catch (e) {
    return true; // Invalid token format
  }
}

isLoggedIn(): boolean {
  const token = this.getToken();
  if (!token) return false;

  try {
    const payload = JSON.parse(atob(token.split('.')[1]));
    const isExpired = Math.floor(Date.now() / 1000) >= payload.exp;
    return !isExpired;
  } catch (e) {
    // Token is invalid
    return false;
  }
}
}
