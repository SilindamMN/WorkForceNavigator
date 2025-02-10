import React from 'react';
import { Tab } from 'semantic-ui-react';
import MyLeaveAllocationsPage from './MyLeaveAllocationsPage';
import MyLeaveRequestPage from '../LeaveRequests/MyLeaveRequestPage';
import AllAllocationPage from './AllAllocationPage';
import UpcomingLeaveRequestPage from '../LeaveRequests/UpcomingLeaveRequestPage';
import ProcessLeaveRequestPage from '../LeaveRequests/ProcessLeaveRequestPage';
import useAuth from '../../../hooks/useAuth.hook';

const ManageLeavesPage = () => {
  const { user } = useAuth(); // Assuming you have a user object from your authentication context

  // Define panes excluding the "PROCESS REQUESTS" tab if the user is not an admin
  const panes = [
    {
      menuItem: 'MY LEAVE ALLOCATIONS',
      pane: { key: 'tab1', content: <MyLeaveAllocationsPage /> },
    },
    {
      menuItem: 'MY LEAVE REQUESTS',
      pane: { key: 'tab2', content: <MyLeaveRequestPage />, textAlign: 'center' },
    },
    {
      menuItem: 'UPCOMING LEAVES',
      pane: { key: 'tab3', content: <UpcomingLeaveRequestPage /> },
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

export default ManageLeavesPage;
