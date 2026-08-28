import { Gender } from "./Constant/enums/gender";

export interface User {
  id?: any;
  firstName: string;
  lastName: string;
  email: string;
  username: string;
  phoneNumber?: string | null;
  salary?: string | null;
  gender?: Gender | null;
  roles: string[];
}

// DTO without id and createdAt
export interface UserDto {
  departmentId?: number | null;
  jobTitleId?: number | null;
  teamId?: number | null;
  firstName: string;
  lastName: string;
  email: string;
  username: string;
  phoneNumber?: string | null;
  salary?: string | null;
  jobTitle?: string | null;
  gender?: string | null;
  roles: string[];  
}

export interface UpdateUserDetailsDto {
  firstName: string;
  lastName: string;
  gender: string;
  jobTitleId: number;
  teamId?: number | null;
  seniority?: string | null;
  salary?: number | null;
  phonenumber: string;
}