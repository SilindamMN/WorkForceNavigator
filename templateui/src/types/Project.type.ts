export interface IUpdateProjectDto {
    projectName: string;
    clientId: number;
    teamId: number;
    description:string;
    startDate: Date;
    endDate: Date;
}

export interface IProjectDto {
    id: number;
    projectName: string;
    description: string;
    startDate: string; // ISO format date string
    endDate: string; // ISO format date string
    clientName: string; // Optional, for display purposes
    teamName: string; // Optional, for display purposes
  }

export interface ICreateProjectDto {
    projectName: string;
    clientId: number;
    teamId: number;
    description: string;
    startDate: Date;
    endDate: Date;
  }