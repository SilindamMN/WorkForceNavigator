export class Department {
  id?: number;
  departmentName: string;
  description: string;
  seniorityLevel?: string;

  constructor() {
    this.id = undefined;
    this.departmentName = '';
    this.description = '';
    this.seniorityLevel = undefined;
  }
}

// DTO without id
export class DepartmentDto {
  departmentName: string;
  description: string;
   seniorityLevel?: string;

  constructor() {
    this.departmentName = '';
    this.description = '';
    this.seniorityLevel = undefined;
  }
}