export const SeniorityOptions = [
  'Junior',
  'Mid',
  'Senior',
  'Lead'
] as const;

export type Seniority = typeof SeniorityOptions[number];