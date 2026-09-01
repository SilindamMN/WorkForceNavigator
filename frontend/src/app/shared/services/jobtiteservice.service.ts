import { Injectable } from '@angular/core';
import { GenericCrudService } from './generic.service';
import { HttpClient } from '@angular/common/http';
import { JobTitle } from '../../models/jobtitle';

@Injectable({
  providedIn: 'root',
})
export class JobtiteserviceService extends GenericCrudService<JobTitle> {
  constructor(http: HttpClient) {
    super(http, 'jobtitles');
  }
  
}
