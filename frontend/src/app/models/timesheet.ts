export class TimesheetSummary {
  date = '';
  totalHours = 0;
  dayName = '';
  projectNames: string[] = [];
}


export class Timesheet {
  id = 0;
  timesheetDate = new Date();
  dayName = '';
  username = '';
  description = '';
  timeSpent = 0;
  projectId = 0;
  projectNames = '';
}