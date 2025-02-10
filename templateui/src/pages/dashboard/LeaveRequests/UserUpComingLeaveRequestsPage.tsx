import React, { useEffect, useState } from 'react';
import { ILeaveRequestDto } from '../../../types/leaveRequest.type';
import toast from 'react-hot-toast';
import Spinner from '../../../components/general/Spinner';
import { Table } from "semantic-ui-react";
import { USER_LEAVE_REQUEST_URL } from '../../../utils/globalConfig';
import { GenericCrudOperations } from '../../../components/general/GenericCrudOperations';

interface IProps {
  username: string;
}

const UserUpComingLeaveRequestsPage = ({ username }: IProps) => {
  const [leaveRequest, setLeaveRequest] = useState<ILeaveRequestDto[]>([]);
  const [loading, setLoading] = useState<boolean>(false);
  const [hasLeaveRequests, setHasLeaveRequests] = useState<boolean>(true);

  const getUserUpComingLeaves = async (username: string) => {
    setLoading(true);
    try {
      await GenericCrudOperations.getDetailed(
        USER_LEAVE_REQUEST_URL,
        { userName: username },
        (data: ILeaveRequestDto[]) => {
          setLeaveRequest(data);
          setHasLeaveRequests(data.length > 0);
        },
        setLoading
      );
    } catch (error) {
      toast.error('Error fetching leave requests');
      setHasLeaveRequests(false);
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    getUserUpComingLeaves(username);
  }, [username]);

  if (loading) {
    return (
      <div className='w-full'><Spinner /></div>
    );
  }

  return (
    <>
        {hasLeaveRequests ? (
          <Table size="small" bordered>
            <Table.Header>
              <Table.Row>
                <Table.HeaderCell>Id</Table.HeaderCell>
                <Table.HeaderCell>FirstName</Table.HeaderCell>
                <Table.HeaderCell>LastName</Table.HeaderCell>
                <Table.HeaderCell>Leave Name</Table.HeaderCell>
                <Table.HeaderCell>No Of Days</Table.HeaderCell>
                <Table.HeaderCell>Start Date</Table.HeaderCell>
                <Table.HeaderCell>End Date</Table.HeaderCell>
                <Table.HeaderCell>Applied On</Table.HeaderCell>
                <Table.HeaderCell>Status</Table.HeaderCell>
              </Table.Row>
            </Table.Header>
            <Table.Body>
              {leaveRequest.map((item) => (
                <Table.Row key={item.id}>
                  <Table.Cell>{item.id}</Table.Cell>
                  <Table.Cell>{item.firstName}</Table.Cell>
                  <Table.Cell>{item.lastName}</Table.Cell>
                  <Table.Cell>{item.leaveName}</Table.Cell>
                  <Table.Cell>{item.numberOfDays}</Table.Cell>
                  <Table.Cell>{new Date(item.startDate).toLocaleDateString()}</Table.Cell>
                  <Table.Cell>{new Date(item.endDate).toLocaleDateString()}</Table.Cell>
                  <Table.Cell>{new Date(item.requestedDate).toLocaleDateString()}</Table.Cell>
                  <Table.Cell>{item.status}</Table.Cell>
                </Table.Row>
              ))}
            </Table.Body>
          </Table>
        ) : (
          <div>No upcoming leave requests found for {username}.</div>
        )}
    </>
  );
};

export default UserUpComingLeaveRequestsPage;
