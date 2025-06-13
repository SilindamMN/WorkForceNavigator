import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Constant } from '../constant';
import { BaseEntity } from '../models/BaseEntity';

export class GenericCrudService<T extends BaseEntity> {
  protected baseUrl: string;

  constructor(protected http: HttpClient, endpoint: string) {
    this.baseUrl = Constant.ApiUrl + endpoint;
  }

  // Get all entities, optionally with an action path
  getAll(action?: string): Observable<T[]> {
    const url = action ? `${this.baseUrl}${action}` : this.baseUrl;
    return this.http.get<T[]>(url);
  }

  // Get entity by ID, optionally with an action path before the ID
  getById(id: string, action?: string): Observable<T> {
    const url = action ? `${this.baseUrl}${action}/${id}` : `${this.baseUrl}/${id}`;
    return this.http.get<T>(url);
  }

  // Create entity, optionally with an action path
  create(entity: T, action?: string): Observable<T> {
    const url = action ? `${this.baseUrl}${action}` : this.baseUrl;
    return this.http.post<T>(url, entity);
  }

  // Update entity, requires id, optionally with an action path before the id
  update(entity: T, action?: string): Observable<T> {
    if (!entity.id) {
      throw new Error('Entity must have an ID for update operation.');
    }
    const url = action
      ? `${this.baseUrl}${action}/${entity.id}`
      : `${this.baseUrl}/${entity.id}`;
    return this.http.put<T>(url, entity);
  }

  // Delete entity by id, optionally with an action path before the id
  delete(id: number | string, action?: string): Observable<any> {
    const url = action ? `${this.baseUrl}${action}` : `${this.baseUrl}/${id}`;
    return this.http.delete<any>(url);
  }
}