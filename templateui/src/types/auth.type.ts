export interface IRegisterDto {
  firstName: string;
  lastName: string;
  userName: string;
  email: string;
  password: string;
}

export interface ILoginDto {
  userName: string;
  password: string;
}

export interface IUpdateRoleDto {
  userName: string;
  newRole: string;
}

export interface IAuthUser {
  id: string;
  firstName: string;
  lastName: string;
  username: string;
  email: string;
  createdAt: string;
  roles: string[];
}

export interface ILoginResponseDto {
  newToken: string;
  userInfo: IAuthUser;
}

export interface IAuthContextState {
  isAuthenticated: boolean;
  isAuthLoading: boolean;
  user?: IAuthUser;
}

export enum IAuthContextActionTypes {
  INITIAL = 'INITIAL',
  LOGIN = 'LOGIN',
  LOGOUT = 'LOGOUT',
}

export interface IAuthContextAction {
  type: IAuthContextActionTypes;
  payload?: IAuthUser;
}

export interface IAuthContext {
  isAuthenticated: boolean;
  isAuthLoading: boolean;
  user?: IAuthUser;

  login: (userName: string, password: string) => Promise<void>;
  
  register: (
    firstName: string,
    lastName: string,
    userName: string,
    email: string,
    password: string
  ) => Promise<void>;
  logout: () => void;
}

export enum RolesEnum {
  OWNER = 'OWNER',
  ADMIN = 'ADMIN',
  MANAGER = 'MANAGER',
  USER = 'USER',
}

export enum Seniority {
  Junior = "Junior",
  Mid = "Mid",
  Senior = "Senior",
  Lead = "Lead"
}

enum Gender {
  Male,
  Female,
  Other // Assuming there's a need for other options
}

export interface IUpdateUserDetailsDto {
  firstName: string;
  lastName: string;
  roles: string[];
  gender: Gender;
  jobTitle?: string;
  salary?: number;
  lineManager?: string;
  department: string;
  seniority: Seniority;
}