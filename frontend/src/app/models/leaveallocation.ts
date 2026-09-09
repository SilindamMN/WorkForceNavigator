export class LeaveAllocation {
  id?: any;
  leaveTypeId: number;
  numberOfDays: number;
  username: string;
  leaveName: string;
  firstName: string;
  lastName: string;

  constructor() {
    this.id = undefined;
    this.leaveTypeId = 0;
    this.numberOfDays = 0;
    this.username = '';
    this.leaveName = '';
    this.firstName = '';
    this.lastName = '';
  }
}

// DTO without id
export class LeaveAllocationDto {
  leaveTypeId?: number;
  numberOfDays?: number;
  username?: string;
  leaveName?: string;
  firstName?: string;
  lastName?: string;

  constructor() {
    this.leaveTypeId = 0;
    this.numberOfDays = 0;
    this.username = '';
    this.leaveName = '';
    this.firstName = '';
    this.lastName = '';
  }
}