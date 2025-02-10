import React from 'react';
import { TableContainer, Table, TableHead, TableRow, TableCell, TableBody } from '@mui/material';
import moment from 'moment';
import { IAuthUser } from '../../../types/auth.type';

interface IProps {
  usersList: IAuthUser[];
  onUserSelected: (username: string | null) => void;
}

const UsersTableSection = ({ usersList, onUserSelected }: IProps) => {
  
  const handleUserClick = (username: string) => {
    console.log('Clicked on user:', username); // Log clicked username to console
    onUserSelected(username);
  }
  
  return (
    <TableContainer component="div">
      <Table size="small">
        <TableHead>
          <TableRow>
            <TableCell>No</TableCell>
            <TableCell>First Name</TableCell>
            <TableCell>Last Name</TableCell>
            <TableCell>User Name</TableCell>
          </TableRow>
        </TableHead>
        <TableBody>
          {usersList.map((user, index) => (
            <TableRow 
              key={index} 
              onClick={() => handleUserClick(user.userName)} 
              style={{ cursor: 'pointer' }} // Set cursor to pointer
            >
              <TableCell>{index + 1}</TableCell>
              <TableCell>{user.firstName}</TableCell>
              <TableCell>{user.lastName}</TableCell>
              <TableCell>{user.userName}</TableCell>
            </TableRow>
          ))}
        </TableBody>
      </Table>
    </TableContainer>
  );
};

export default UsersTableSection;
