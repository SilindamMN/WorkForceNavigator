import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Department } from '../../models/department';
import { GenericCrudService } from './generic.service';

@Injectable({
  providedIn: 'root'
})
export class DepartmentsService extends GenericCrudService<Department> {

  constructor(http: HttpClient) {
    super(http,'departments');
  }
}