export interface User {
  id: string;
  firstName: string;
  lastName: string;
  email: string;
  userName: string;
  normalizedUserName: string;
  createdAt: string;
  phoneNumber: string | null;
  gender: string | null;
  jobTitle: string | null;
  lineManager: string | null;
  salary: number | null;
  seniority: string | null;
}