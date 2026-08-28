export interface UserLogin{
    username: string;
    password: string;
}
export interface UserRegister{
    username: string;
    password: string;
    email: string;
    firstName: string;
    lastName: string;
}
export interface UserInfoResult {
  id: string;
  firstName: string;
  lastName: string;
  email: string;
  username: string;
  createdAt: string; // Or Date if you're converting it
  phoneNumber?: string;
  gender?: 'Male' | 'Female' | 'Other' | null; // Adjust based on your `Gender` enum
  roles: string[];
}

export interface LoginServiceResponseDto {
  newToken: string;
  userInfo: UserInfoResult;
}
