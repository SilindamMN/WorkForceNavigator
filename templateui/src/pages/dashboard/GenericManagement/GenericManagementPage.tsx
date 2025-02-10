import React from 'react';
import { Tab } from 'semantic-ui-react';
import UpcomingLeaveRequestPage from '../LeaveRequests/UpcomingLeaveRequestPage';
import ProcessLeaveRequestPage from '../LeaveRequests/ProcessLeaveRequestPage';
import useAuth from '../../../hooks/useAuth.hook';
import AllAllocationPage from '../LeaveAllocations/AllAllocationPage';
import MyLeaveAllocationsPage from '../LeaveAllocations/MyLeaveAllocationsPage';
import MyLeaveRequestPage from '../LeaveRequests/MyLeaveRequestPage';
import ClientsPage from './Clients/ClientsPage';
import ProjectsPage from './Projects/ProjectsPage';
import TeamsPage from './Teams/TeamsPage';
import ManageClientPage from './Clients/ManageClientPage';
import ManageDepartmentPage from './Departments/ManageDepartmentPage';
import ManageProjectPage from './Projects/ManageProjectPage';
import ManageTeamPage from './Teams/ManageTeamPage';
import ManageJobTitlePage from './JobTitles/ManageJobTitlePage';

const GenericManagementPage = () => {
  const { user } = useAuth(); // Assuming you have a user object from your authentication context

  // Define panes excluding the "PROCESS REQUESTS" tab if the user is not an admin
  const panes = [
    {
      menuItem: 'DEPARTMENTS',
      pane: { key: 'tab3', content: <ManageDepartmentPage /> },
    },
    {
      menuItem: 'CLIENTS',
      pane: { key: 'tab1', content: <ManageClientPage /> },
    },
    {
      menuItem: 'PROJECTS',
      pane: { key: 'tab2', content: <ManageProjectPage />, textAlign: 'center' },
    },
    {
      menuItem: 'TEAMS',
      pane: { key: 'tab3', content: <ManageTeamPage /> },
    },
    {
      menuItem: 'JOBTITLES',
      pane: { key: 'tab4', content: <ManageJobTitlePage /> },
    },
  ];

  // If the user is an admin, include the "PROCESS REQUESTS AND ALL ALLOCATIONS" tab
  if (user && (user.roles.includes('ADMIN') || user.roles.includes('OWNER'))) {
    panes.push({
      menuItem: 'PROCESS REQUESTS',
      pane: { key: 'tab5', content: <ProcessLeaveRequestPage />, textAlign: 'center' },
    },
    {
      menuItem: 'LEAVE ALLOCATIONS',
      pane: { key: 'tab4', content: <AllAllocationPage />, textAlign: 'center' },
    },
  );
  }

  return (
    <div className='pageTemplate3 '>
      <Tab panes={panes} renderActiveOnly={false} />
    </div>
  );
};

export default GenericManagementPage;
