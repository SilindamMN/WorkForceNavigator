export interface UserDetailsDto {
    id: string;
    firstName: string;
    lastName: string;
    email: string;
    username: string;
    roles: string[];
    gender: string;
    jobTitle?: string;
    salary?: number;
    phoneNumber?:string;
    lineManager?: string;
    department: string;
    seniority: string;
    joiningDate: Date;
}

export interface DepartmentDto{
    id:number;
    departmentName:string;
    description:string;
}

export interface UserDetailsUpdateDto {
    firstName: string;
    lastName: string;
    gender: string;
    jobTitle?: string;
    salary?: number;
    phoneNumber:string;
}

export interface DepartmentUserJobTitleTeam{
    firstName: string;
    lastName: string;
    email: string;
    jobTitle: string;
    team:string;
}