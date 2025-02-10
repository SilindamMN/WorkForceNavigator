import React, { useEffect, useState } from 'react'
import useAuth from '../../../hooks/useAuth.hook'
import { ILeaveAllocationDto, IMyLeaveAllocationDto } from '../../../types/leaveAllocation.type';
import axiosInstance from '../../../utils/axiosInstance';
import { MY_LEAVE_ALLOCATIONS } from '../../../utils/globalConfig';
import toast from 'react-hot-toast';
import Spinner from '../../../components/general/Spinner';
import { TableHead } from '@mui/material';
import { Table, TableRow, TableCell, TableBody } from 'semantic-ui-react';

const MyLeaveAllocationsPage = () => {
  const {user} = useAuth();
  const [leaves,setLeaves] = useState<IMyLeaveAllocationDto[]>([]);
  const [loading,setLoading] = useState<boolean>(false);

  const getMyLeaveAllocations = async()=>{
    try {
      setLoading(true);
      const response = await axiosInstance.get<IMyLeaveAllocationDto[]>(MY_LEAVE_ALLOCATIONS);
      const {data} = response;
      setLeaves(data);
      setLoading(false);
    } catch (error) {
      toast.error("An Error Occured");
      setLoading(false);
    }
  };
  useEffect(()=>{
    getMyLeaveAllocations();
  },[]);

  if(loading){
    return(
      <div className='w-full'>
        return <Spinner/>
      </div>
    )
  }
  return (
     <React.Fragment>
    <Table size="small">
      <TableHead>
        <TableRow>
          <TableCell>Id</TableCell>
          <TableCell>Leave Name</TableCell>
          <TableCell>Number Of Days</TableCell>
        </TableRow>
      </TableHead>
      <TableBody>
        {leaves.map((row) => (
          <TableRow key={row.id}>
            <TableCell>{row.id}</TableCell>
            <TableCell>{row.leaveName} Leave</TableCell>
            <TableCell>{row.numberOfDays}</TableCell>
          </TableRow>
        ))}
      </TableBody>
    </Table>
  </React.Fragment>
  );
};
export default MyLeaveAllocationsPage;