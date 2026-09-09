import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { GenericCrudService } from './generic.service';
import { LeaveAllocation, LeaveAllocationDto } from '../../models/leaveallocation';

@Injectable({ providedIn: 'root' })
export class LeaveAllocationsService extends GenericCrudService<LeaveAllocation> {
  constructor(http: HttpClient) {
    super(http, 'leave-allocations/');
  }
  getLeaveAllocationsByUsername(username: string): Observable<LeaveAllocationDto[]> {
    return this.http.get<LeaveAllocationDto[]>(
      `${this.baseUrl}username?userName=${username}`
    );
  }
}