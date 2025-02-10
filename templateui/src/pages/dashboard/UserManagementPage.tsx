import { useEffect, useState } from 'react';
import axiosInstance from '../../utils/axiosInstance';
import { USERS_LIST_URL } from '../../utils/globalConfig';
import { toast } from 'react-hot-toast';
import Spinner from '../../components/general/Spinner';
import LatestUsersSection from '../../components/dashboard/usermanagement/LatestUsersSection';
import UserChartSection from '../../components/dashboard/usermanagement/UserChartSection';
import UserCountSection from '../../components/dashboard/usermanagement/UserCountSection';
import UsersTableSection from '../../components/dashboard/usermanagement/UsersTableSection';
import { IAuthUser } from '../../types/auth.type';
import { TableContainer, TableHead } from '@mui/material';
import { Table, TableRow, TableCell, TableBody } from 'semantic-ui-react';
import { GenericCrudOperations } from '../../components/general/GenericCrudOperations';

interface IProps{
  selectedUsername :(username:string|null) =>void;
}

const UsersManagementPage = ({selectedUsername}:IProps) => {
  const [users, setUsers] = useState<IAuthUser[]>([]);
  const [loading, setLoading] = useState<boolean>(false);

  const getUsersList = async () => {
    await GenericCrudOperations.getAll(USERS_LIST_URL,setUsers,setLoading);
  };

  useEffect(() => {
    getUsersList();
  }, []);

  if (loading) {
    return (
      <div className='w-full'>
        <Spinner />
      </div>
    );
  }

  const handleSelectedUser = (username:string | null) => {
    selectedUsername(username);
  }

  return (
    <div className='pageTemplate2'>
      <TableContainer component="div">
        <Table size="small">
          <TableHead>
            <TableRow>
              <TableCell>No</TableCell>
              <TableCell>First Name</TableCell>
              <TableCell>Last Name</TableCell>
            </TableRow>
          </TableHead>
          <TableBody>
            {users.map((user, index) => (
              <TableRow 
                key={index} 
                onClick={() => handleSelectedUser(user.username)} 
                style={{ cursor: 'pointer' }}
              >
                <TableCell>{index + 1}</TableCell>
                <TableCell>{user.firstName}</TableCell>
                <TableCell>{user.lastName}</TableCell>
              </TableRow>
            ))}
          </TableBody>
        </Table>
      </TableContainer>
    </div>
  );
};

export default UsersManagementPage;