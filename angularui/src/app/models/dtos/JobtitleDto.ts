export class JobTitleDto {
  id: number;
  title: string;
  description: string;
  seniority: string;
  departmentName: string;

  constructor() {
    this.id = 0;
    this.title = '';
    this.description = '';
    this.seniority = '';
    this.departmentName = '';
  }
}