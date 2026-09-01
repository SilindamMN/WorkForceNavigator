import { BaseEntity } from "./Constant/baseEntity";

export interface JobTitle extends BaseEntity<number> {
  jobTitleId: number;
  title: string;
  departmentId: number;
  departmentName: string;
  description: string;
  seniority: 'Junior' | 'Mid' | 'Senior' | 'Lead';
}