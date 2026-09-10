import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Timesheet, TimesheetSummary } from '../../models/timesheet';
import { GenericCrudService } from './generic.service';

@Injectable({ providedIn: 'root' })
export class TimesheetService extends GenericCrudService<Timesheet,TimesheetSummary | Timesheet> {
  constructor(http: HttpClient) {
    super(http, 'timesheet/');
  }

getTimesheetSummary(weekOffSet?: number,) {
  return this.http.get<TimesheetSummary[]>(
    `${this.baseUrl}week-off-set?weekOffSet=${weekOffSet}`
  );
}
getTimesheetDetails(timesheetDate: string) {
  return this.http.get<Timesheet[]>(
    `${this.baseUrl}by-date?date=${timesheetDate}`
  );
}
}