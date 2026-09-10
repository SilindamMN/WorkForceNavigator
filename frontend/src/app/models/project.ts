export class Project {
  id?: any;
  projectName: string;
  clientName: string;
  teamName: string;
  description: string;
  startDate: string;
  endDate: string;

  constructor() {
    this.id = undefined;
    this.projectName = '';
    this.clientName = '';
    this.teamName = '';
    this.description = '';
    this.startDate = '';
    this.endDate = '';
  }
}

// DTO without id
export class ProjectDto {
  projectName: string;
  clientName: string;
  teamName: string;
  description: string;
  startDate: string;
  endDate: string;

  constructor() {
    this.projectName = '';
    this.clientName = '';
    this.teamName = '';
    this.description = '';
    this.startDate = '';
    this.endDate = '';
  }
}