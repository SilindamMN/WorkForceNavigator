export interface IUpdateTeamDto{
    teamName:string;
    description:string;
  }
  
  export interface ITeamDto extends IUpdateTeamDto{
    id:number;
  }
  
  export interface ITeamDetails {
    firstName: string;
    lastName: string;
    email: string;
    jobTitle: string;
    team: string;
  }