import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Constant } from '../../models/Constant/constant';
import { BaseEntity } from '../../models/Constant/baseEntity';

export class GenericCrudService<
  T extends BaseEntity,
  TDto = T
>{
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
  create(entity: T, action?: any): Observable<T> {
    const url = action ? `${this.baseUrl}${action}` : this.baseUrl;
    return this.http.post<T>(url, entity);
  }

  // Update entity, requires id, optionally with an action path before the id
  update(entity: T, action?: string): Observable<T> {
  if (!entity.id) {
    throw new Error('Entity must have an ID for update operation.');
  }
  const base = this.baseUrl.endsWith('/') ? this.baseUrl.slice(0, -1) : this.baseUrl;
  const url = action
    ? `${base}${action}/${entity.id}`
    : `${base}/${entity.id}`;
  return this.http.patch<T>(url, entity);
} // Update entity, requires id, optionally with an action path before the id
 updateByKey<T>(
  entity: T,
  key: string,
  action?: string
): Observable<T> {

  if (!key) {
    throw new Error('Key is required for update operation.');
  }

  const url = action
    ? `${this.baseUrl}${action}update?updateUsername=${key}`
    : `${this.baseUrl}update?updateUsername=${key}`;

  return this.http.post<T>(url, entity);
}

  // Delete entity by id, optionally with an action path before the id
  // Delete entity by id, optionally with an action path before the id
delete(id: number | string, action?: string): Observable<any> {
  const url = action ? `${this.baseUrl}/${action}/${id}` : `${this.baseUrl}/${id}`;
  return this.http.delete<any>(url);
}
}