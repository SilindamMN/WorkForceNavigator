import { Injectable } from '@angular/core';
import { GenericCrudService } from './generic.service';
import { HttpClient } from '@angular/common/http';
import { JobTitle } from '../../models/jobtitle';
import { Observable } from 'rxjs';
import { Seniority } from '../../models/Constant/enums/seniority';

@Injectable({
  providedIn: 'root',
})
export class JobtiteserviceService extends GenericCrudService<JobTitle> {
  constructor(http: HttpClient) {
    super(http, 'jobtitles');
  }  getJobTitleByDepartmentId(departmentId: number, seniority: Seniority): Observable<JobTitle[]> {
    return this.http.get<JobTitle[]>(
      `${this.baseUrl}/Department/${departmentId}?seniority=${seniority}`
    );
  }
  
}
