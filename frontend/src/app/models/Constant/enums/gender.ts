export const GenderOptions = [
  'Female',
  'Male'
] as const;

export type Gender = typeof GenderOptions[number];

export const LeaveStatusOptions = [
  'Pending',
  'Approved',
  'Declined'
] as const;

export type LeaveStatus = typeof LeaveStatusOptions[number];