import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Project } from '../../models/project';
import { GenericCrudService } from './generic.service';

@Injectable({ providedIn: 'root' })
export class ProjectsService extends GenericCrudService<Project> {
  constructor(http: HttpClient) {
    super(http, 'projects/');
  }

  getUserProjectByUserName(username:string): Observable<any>{
   return this.http.get<any>(
    `${this.baseUrl}${username}`
   );
  }
}