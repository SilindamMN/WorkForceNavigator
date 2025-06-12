import { HttpClient } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { GenericCrudService } from './genericcrud.service'; 
import { BaseEntity } from '../models/BaseEntity'; 

@Injectable({
  providedIn: 'root' 
})
export class GenericCrudFactoryService {
  private http = inject(HttpClient);

  create<T extends BaseEntity>(endpoint: string): GenericCrudService<T> {
    return new GenericCrudService<T>(this.http, endpoint);
  }
}