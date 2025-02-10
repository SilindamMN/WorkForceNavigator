import { PATH_PUBLIC, PATH_DASHBOARD } from "../routes/path";

// AUTH
export const HOST_API_KEY = 'https://localhost:7148/api';
export const REGISTER_URL = '/Auth/register';
export const LOGIN_URL = '/Auth/login';
export const ME_URL = '/Auth/me';
export const UPDATE_ROLE_URL = '/Auth/UpdateRoles';
export const UPDATE_USER_URL = '/Auth/update';

//USERS

export const USERNAMES_LIST_URL = '/Auth/usernames';
export const USERS_LIST_URL = '/Auth/users';
export const USER_DETAILS_URL = '/Auth/userDetails';
export const UPDATE_USER_DETAILS ='/Auth/update'

//MESSAGES

export const ALL_MESSAGES_URL = '/Message';
export const CREATE_MESSAGE_URL = '/Message/create';
export const MY_MESSAGE_URL = '/Message/mine';

//LOGS

export const LOGS_URL = '/Logs';
export const MY_LOGS_URL = '/Logs/mine';

// LEAVE ALLOCATION
export const ALL_LEAVE_ALLOCATIONS = 'LeaveAllocation/LeaveAllocations'
export const MY_LEAVE_ALLOCATIONS = '/LeaveAllocation/MyLeaveAllocations'
export const USERNAME_LEAVE_ALLOCATIONS = '/LeaveAllocations/username'
export const LEAVE_ALLOCATION_BYTYPE = '/LeaveAllocation/type'

//LEAVE REQUEST
export const MY_LEAVE_REQUESTS = '/LeaveRequest/MyLeaveRequests'
export const UPCOMNG_LEAVE_REQUESTS = '/LeaveRequest/UpcomingLeaveRequest'
export const UPDATE_LEAVE_REQUEST_URL = '/LeaveRequest/UpdateLeaveRequest';
export const NEW_LEAVE_REQUEST_URL = '/LeaveRequest/Create';
export const PROCESS_LEAVE_REQUEST_URL = '/LeaveRequest/ProcessLeaveRequest';
export const ALL_LEAVE_REQUEST_URL = '/LeaveRequest/LeaveRequests';
export const USER_LEAVE_REQUEST_URL ='/LeaveRequest/LeaveRequestsByUsereName'

// Auth Routes
export const PATH_AFTER_REGISTER = PATH_PUBLIC.login;
export const PATH_AFTER_LOGIN = PATH_DASHBOARD.dashboard;
export const PATH_AFTER_LOGOUT = PATH_PUBLIC.home;

//DEPARTMENT
export const NEW_DEPARTMENT_URL = '/Department/CreateDepartment';
export const UPDATE_DEPARTMENT_URL = '/Department/UpdateDepartment';
export const DELETE_DEPARTMENT_URL = '/Department/DeleteDepartment';
export const ALL_DEPARTMENTS = '/Department';
export const DEPARTMENT_JOBTITLE_TEAM='/Department/DepartmentUserDetailJobTitle';

//TIMESHEET
export const MY_TIMESHEETS = '/Timesheet';
export const TIMESHEET_DAY_DETAILS = '/Timesheet/DAte';

//CLIENTS
export const NEW_CLIENT_URL = '/Client/CreateClient';
export const UPDATE_CLIENT_URL = '/Client/UpdateClient';
export const DELETE_CLIENT_URL = '/Client/DeleteClient';
export const ALL_CLIENTS = '/Client';
export const CLIENT_DETAILS = '/Client/ClientProjectDetails';

// PROJECTS
export const NEW_PROJECT_URL = '/Project/CreateProject';
export const UPDATE_PROJECT_URL = '/Project/UpdateProject';
export const DELETE_PROJECT_URL = '/Project/DeleteProject';
export const ALL_PROJECTS = '/Project';
export const PROJECT_DETAILS = '/Project/ProjectDetails';

// TEAM
export const NEW_TEAM_URL = '/Team/CreateTeam';
export const UPDATE_TEAM_URL = '/Team/UpdateTeam';
export const DELETE_TEAM_URL = '/Team/DeleteTeam';
export const ALL_TEAMS = '/Team';
export const TEAM_DETAILS = '/Team/TeamUserDetailJobTitle';

// JOBTITLE
export const NEW_JOBTITLE_URL = '/JobTitle/CreateJobTitle';
export const UPDATE_JOBTITLE_URL = '/JobTitle/UpdateJobTitle';
export const DELETE_JOBTITLE_URL = '/JobTitle/DeleteJobTitle';
export const ALL_JOBTITLES = '/JobTitle';
export const JOBTITLE_DETAILS = '/JobTitle/JobTitleUserDetailJobTitle';

//OTHERS
export const GENERIC_MANAGEMENT = '/GenericManagement'