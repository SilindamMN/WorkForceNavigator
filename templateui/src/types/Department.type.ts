export interface IUpdateDepartmentDto{
  departmentName:string;
  description:string;
}

export interface IDepartmentDto extends IUpdateDepartmentDto{
  id:number;
}

export interface IDepartmentTeamJobTitle {
  firstName: string;
  lastName: string;
  email: string;
  jobTitle: string;
  team: string;
}