import { Gender } from "./Constant/enums/gender";
import { Seniority } from "./Constant/enums/seniority";

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

export interface UserDto {

  id?: any;

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

  gender?: Gender | null;

  seniority?: Seniority | null;

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