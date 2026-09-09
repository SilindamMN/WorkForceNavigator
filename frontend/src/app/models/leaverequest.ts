export class LeaveRequest {
  id = 0;
  startDate = new Date();
  endDate = new Date();
  leaveTypeId = 0;
  dateRequested = new Date();
  requestComments = '';
  status = 0;
  numberOfDays = 0;
  userName = '';
}

export class LeaveRequestDto {
  id = 0;
  leaveName = '';
  startDate = new Date();
  endDate = new Date();
  requestedDate = new Date();
  status = 0;
  firstName = '';
  lastName = '';
  userName = '';
  numberOfDays = 0;
}

export class CreateLeaveRequestDto {
  leaveTypeId = 0;
  startDate = new Date();
  endDate = new Date();
}

export class MyLeaveRequestDto {
  id = 0;
  startDate = new Date();
  endDate = new Date();
  requestedDate = new Date();
  leaveName = '';
  comments = '';
  status = 0;
  numberOfDays = 0;
}

export class UpdateLeaveRequestDto {
  startDate = new Date();
  endDate = new Date();
  comment = '';
}