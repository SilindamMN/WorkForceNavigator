type WeekDay = "Monday" | "Tuesday" | "Wednesday" | "Thursday" | "Friday";

export interface TimesheetDto{
    date: Date;
    dayName: WeekDay;
    totalhours: number;
    projectNames: Set<string>;
}

export interface TimesheetDetailsDto{
  description:string;
  timeSpent:number;
  projectName:string;
}