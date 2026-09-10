  import { Department } from "./department";
import { Project } from "./project";

  export class Team {
    id: number;
    teamName: string;
    description: string;
    departmentId: number;
    department?: Department;
  departmentName?: string;
    projects: Project[];

    constructor() {
      this.id = 0;
      this.teamName = '';
      this.description = '';
      this.departmentId = 0;
    this.departmentName = '' ;
      this.projects = [];
    }
  }

  // DTO (for create/update)
  export class TeamDto {
    id: number;
    teamName: string;
    description: string;
    departmentName: string;

    constructor() {
      this.id = 0;
      this.teamName = '';
      this.description = '';
      this.departmentName = '';
    }
  }
  export interface TeamMemberDto {
    firstName: string;
    lastName: string;
    jobTitle: string;
  }

  export interface TeamMemberDetailsDto {
    members: TeamMemberDto[];
    projectList: string[];
  }
  // DTO (for create/update)
  export class UserTeamListDto {
    teamId?: number;
    userName: string;
    teamName: string;
    teamLeader: string;

    constructor() {
      this.teamId = undefined;
      this.userName = '';
      this.teamName = '';
      this.teamLeader = '';
    }
  }export class AddMemberDto {
    teamId: number;
    userId: string;
    isRemoved?: boolean;

    constructor() {
      this.teamId = 0;
      this.userId = '';
  this.isRemoved = false;
    }
  }