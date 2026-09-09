import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { LeaveStatus } from '../../models/Constant/enums/gender';
import { GenericCrudService } from './generic.service';
import { LeaveRequest, LeaveRequestDto, UpdateLeaveRequestDto, CreateLeaveRequestDto } from '../../models/leaverequest';

@Injectable({ providedIn: 'root' })
export class LeaverequestService extends GenericCrudService<LeaveRequest, LeaveRequestDto> {
  constructor(http: HttpClient) {
    super(http, 'leave-requests');
  }

processLeaveRequest(
  leaveRequestId: number,
  status: LeaveStatus
): Observable<LeaveRequestDto> {
  return this.http.post<LeaveRequestDto>(
    `${this.baseUrl}/process?leaveRequestId=${leaveRequestId}&status=${status}`,
    null
  );
}

updateLeaveRequest(
  leaveRequestId: number,
  leaveRequest: UpdateLeaveRequestDto
) {
  return this.http.patch(
    `${this.baseUrl}/${leaveRequestId}`,
    leaveRequest
  );
}
createLeaveRequest(leaveRequest: CreateLeaveRequestDto): Observable<CreateLeaveRequestDto> {
  return this.http.post<CreateLeaveRequestDto>(`${this.baseUrl}/create`, leaveRequest);
}
}