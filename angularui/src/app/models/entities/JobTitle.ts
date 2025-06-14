// dtos/UpdateCreateJobTitleDto.ts
import { Seniority } from '../enums/Seniority';

export interface JobTitle {
  title: string;
  description: string;
  seniority: Seniority;
  departmentId: number;
}