import { Navigate, Route, Routes } from 'react-router-dom';
import Layout from '../components/layout';
import { PATH_DASHBOARD, PATH_PUBLIC } from './path';
import LoginPage from '../pages/public/LoginPage';
import UnauthorizedPage from '../pages/public/UnauthorizedPage';
import AuthGuard from '../auth/AuthGuard';
import { adminAccessRoles, allAccessRoles, managerAccessRoles, ownerAccessRoles } from '../auth/auth.utils';
import DashboardPage from '../pages/dashboard/DashboardPage';
import UserPage from './UserPage';
import InboxPage from '../pages/dashboard/Messages/InboxPage';
import SendMessagePage from '../pages/dashboard/Messages/SendMessagePage';
import ManagerPage from '../pages/dashboard/ManagerPage';
import SystemLogsPage from '../pages/dashboard/SystemLogsPage';
import AllMessagesPage from '../pages/dashboard/Messages/AllMessagesPage';
import UpdateRolePage from '../pages/dashboard/UpdateRolePage';
import OwnerPage from '../pages/dashboard/OwnerPage';
import MyLeaveAllocationsPage from '../pages/dashboard/LeaveAllocations/MyLeaveAllocationsPage';
import LeaveAllocationByUserNamePage from '../pages/dashboard/LeaveAllocations/LeaveAllocationByUserNamePage';
import AllocationByLeaveNamePage from '../pages/dashboard/LeaveAllocations/AllocationByLeaveNamePage';
import AllAllocationPage from '../pages/dashboard/LeaveAllocations/AllAllocationPage';
import ManageMessagesPage from '../pages/dashboard/Messages/ManageMessagesPage';
import SignUpPage from '../pages/public/SignUpPage';
import ManageLeavesPage from '../pages/dashboard/LeaveAllocations/ManageLeavesPage';
import ProcessLeaveRequestPage from '../pages/dashboard/LeaveRequests/ProcessLeaveRequestPage';
import ManageUsersPage from '../pages/dashboard/User/ManageUsersPage';
import UserDetails from '../pages/dashboard/User/UserDetails';
import GenericManagementPage from '../pages/dashboard/GenericManagement/GenericManagementPage';
import TeamsPage from '../pages/dashboard/GenericManagement/Teams/TeamsPage';
import ProjectsPage from '../pages/dashboard/GenericManagement/Projects/ProjectsPage';
import ManageTimesheetPage from '../pages/dashboard/Timesheet/ManageTimesheetPage';
import ManageDepartmentPage from '../pages/dashboard/GenericManagement/Departments/ManageDepartmentPage';
import ManageProjectPage from '../pages/dashboard/GenericManagement/Projects/ManageProjectPage';
import ManageJobTitlePage from '../pages/dashboard/GenericManagement/JobTitles/ManageJobTitlePage';
import ManageTeamPage from '../pages/dashboard/GenericManagement/Teams/ManageTeamPage';
import AdminPage from '../pages/dashboard/User/AdminPage';

const GlobalRouter = () => {
    return (
      <Routes>
        {/* Wrap all routes with the Layout component */}
        <Route element={<Layout />}>
          {/* Public routes */}
          <Route index element={<LoginPage />} />
          <Route path={PATH_PUBLIC.login} element={<LoginPage />} />
          <Route path={PATH_PUBLIC.register} element={<SignUpPage />} />
          <Route path={PATH_PUBLIC.unauthorized} element={<UnauthorizedPage />} />

          {/* Protected routes */}
          {/* All access roles */}
          <Route element={<AuthGuard roles={allAccessRoles} />}>
            <Route path={PATH_DASHBOARD.dashboard} element={<DashboardPage />} />
            <Route path={PATH_DASHBOARD.sendMessage} element={<SendMessagePage />} />
            <Route path={PATH_DASHBOARD.manageMessage} element={<ManageMessagesPage />} />
            <Route path={PATH_DASHBOARD.genericManagement} element={<GenericManagementPage />} />
            <Route path={PATH_DASHBOARD.manageLeaves} element={<ManageLeavesPage />} />
            <Route path={PATH_DASHBOARD.inbox} element={<InboxPage />} />
            <Route path={PATH_DASHBOARD.user} element={<UserPage />} />

            
            <Route path={PATH_DASHBOARD.user} element={<UserPage />} />
            <Route path={PATH_DASHBOARD.user} element={<UserPage />} />
            
            <Route path={PATH_DASHBOARD.manageDepartments} element={<ManageDepartmentPage />} />
            
            <Route path={PATH_DASHBOARD.jobTitleRequest} element={<ManageJobTitlePage />} />
            <Route path={PATH_DASHBOARD.teamRequest} element={<ManageTeamPage />} />

            <Route path={PATH_DASHBOARD.allocationByLeaveName} element={<AllocationByLeaveNamePage/>} />
            <Route path={PATH_DASHBOARD.myAllocation} element={<MyLeaveAllocationsPage/>} />
            <Route path={PATH_DASHBOARD.allocationByusername} element={<LeaveAllocationByUserNamePage />} />
            <Route path={PATH_DASHBOARD.manageTimesheets} element={<ManageTimesheetPage />} />
            <Route path={PATH_DASHBOARD.manageProjects} element={<ManageProjectPage />} />
          </Route>

          {/* Manager access roles */}
          <Route element={<AuthGuard roles={managerAccessRoles} />}>
            <Route path={PATH_DASHBOARD.manager} element={<ManagerPage />} />
          </Route>

          {/* Admin access roles */}
          <Route element={<AuthGuard roles={adminAccessRoles} />}>
            <Route path={PATH_DASHBOARD.allLeaveAllocations} element={<AllAllocationPage />} />
            <Route path={PATH_DASHBOARD.updateRole} element={<UpdateRolePage />} />
            <Route path={PATH_DASHBOARD.allMessages} element={<AllMessagesPage />} />
            <Route path={PATH_DASHBOARD.systemLogs} element={<SystemLogsPage />} />
            <Route path={PATH_DASHBOARD.admin} element={<AdminPage />} />
            <Route path={PATH_DASHBOARD.manageUsers} element={<ManageUsersPage />} />
            <Route path={PATH_DASHBOARD.updateUserDetails} element={<UserDetails username={''}  />} />
            <Route path={PATH_DASHBOARD.manageLeaves} element={<ProcessLeaveRequestPage />} />
          </Route>

          {/* Owner access roles */}
          <Route element={<AuthGuard roles={ownerAccessRoles} />}>
            <Route path={PATH_DASHBOARD.owner} element={<OwnerPage />} />
          </Route>

          {/* Catch all (404) */}
       {/*   <Route path={PATH_PUBLIC.notFound} element={<LoginPage />} />*/}
          <Route path={PATH_PUBLIC.login} element={<LoginPage />} />
          <Route path='*' element={<Navigate to={PATH_PUBLIC.login} replace />} />
        </Route>
      </Routes>
    );
};

export default GlobalRouter;