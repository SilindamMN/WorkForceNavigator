import React, { useEffect, useState } from 'react'
import { ILeaveRequestDto } from '../../../types/leaveRequest.type';
import axiosInstance from '../../../utils/axiosInstance';
import toast from 'react-hot-toast';
import Spinner from '../../../components/general/Spinner';
import { Table, TableRow, TableCell, TableBody } from "semantic-ui-react";
import { UPCOMNG_LEAVE_REQUESTS } from '../../../utils/globalConfig';

const UpcomingLeaveRequestPage = () => {
    const [leaveRequest,setLeaveRequest] = useState<ILeaveRequestDto[]>([]);
    const [loading,setLoading] = useState<boolean>(false);
    
    const getUpcomingLeaves = async()=>{
        try {
            setLoading(true);
            const response = await axiosInstance.get<ILeaveRequestDto[]>(UPCOMNG_LEAVE_REQUESTS);
            const {data} = response;
            setLeaveRequest(data);
            setLoading(false);
        } catch (error) {
          toast.error("Error Occured");
          setLoading(false);
        }
    }

    useEffect(()=>{
      getUpcomingLeaves();
    },[])

    if(loading){
        return(
            <div className='w-full'><Spinner/></div>
        )
    }
    return (
        <>
          <div className='items-stretch'>
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
              <tbody>
                {leaveRequest.map((item) => (
                  <tr key={item.id}>
                  <Table.Cell>{item.id}</Table.Cell>
                  <Table.Cell>{item.firstName}</Table.Cell>
                <Table.Cell>{item.lastName}</Table.Cell>
                 <Table.Cell>{item.leaveName}</Table.Cell>
                 <Table.Cell>{item.numberOfDays}</Table.Cell>
                <Table.Cell>{new Date(item.startDate).toLocaleDateString()}</Table.Cell>
                <Table.Cell>{new Date(item.endDate).toLocaleDateString()}</Table.Cell>
                <Table.Cell>{new Date(item.requestedDate).toLocaleDateString()}</Table.Cell>
                    <Table.Cell>{item.status}</Table.Cell>
                    <Table.Cell>
                </Table.Cell>
                  </tr>
                ))}
              </tbody>
            </Table>
          </div>
        </>
      );
    };
    
export default UpcomingLeaveRequestPage