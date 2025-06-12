import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Constant } from '../constant';
import { BaseEntity } from '../models/BaseEntity';

export class GenericCrudService<T extends BaseEntity> {
  protected baseUrl: string;

  constructor(protected http: HttpClient, endpoint: string) {
    this.baseUrl = Constant.ApiUrl + endpoint;
  }

  getAll(): Observable<T[]> {
    return this.http.get<T[]>(this.baseUrl);
  }

  getById(id: string): Observable<T> {
    return this.http.get<T>(`${this.baseUrl}/${id}`);
  }

  create(entity: T): Observable<T> {
    return this.http.post<T>(this.baseUrl, entity);
  }

  update(entity: T): Observable<T> {
    if (!entity.id) {
      throw new Error('Entity must have an ID for update operation.');
    }
    return this.http.put<T>(`${this.baseUrl}/${entity.id}`, entity);
  }

  delete(id: number): Observable<any> {
    return this.http.delete<any>(`${this.baseUrl}/${id}`);
  }
}