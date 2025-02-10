import React, { useEffect, useState } from 'react';
import axiosInstance from '../../../utils/axiosInstance';
import { MY_MESSAGE_URL } from '../../../utils/globalConfig';
import { toast } from 'react-hot-toast';
import Spinner from '../../../components/general/Spinner';
import moment from 'moment';
import { MdInput, MdOutput } from 'react-icons/md';
import useAuth from '../../../hooks/useAuth.hook';
import { IMessageDto } from '../../../types/message.type';
import { TableContainer, Paper, TableHead, Link } from '@mui/material';
import { Table, TableRow, TableCell, TableBody } from 'semantic-ui-react';
import { Title } from 'chart.js';

const InboxPage = () => {
  const { user } = useAuth();
  const [messages, setMessages] = useState<IMessageDto[]>([]);
  const [loading, setLoading] = useState<boolean>(false);

  const getMyMessages = async () => {
    try {
      setLoading(true);
      const response = await axiosInstance.get<IMessageDto[]>(MY_MESSAGE_URL);
      const { data } = response;
      setMessages(data);
      setLoading(false);
    } catch (error) {
      toast.error('An Error happened. Please Contact admins');
      setLoading(false);
    }
  };

  useEffect(() => {
    getMyMessages();
  }, []);

  if (loading) {
    return (
      <div className='w-full'>
        <Spinner />
      </div>
    );
  }

  return (
    <React.Fragment>
    <Table size="small">
      <TableHead>
        <TableRow>
          <TableCell>Id</TableCell>
          <TableCell>Receiver Name</TableCell>
          <TableCell>Sender Name</TableCell>
          <TableCell align="right">Message</TableCell>
          <TableCell>Date Sent</TableCell>
        </TableRow>
      </TableHead>
      <TableBody>
        {messages.map((row) => (
          <TableRow key={row.id}>
            <TableCell>{row.id}</TableCell>
            <TableCell>{row.receiverUserName}</TableCell>
            <TableCell>{row.senderUserName}</TableCell>
            <TableCell>{row.text}</TableCell>
            <TableCell>{row.createdAt}</TableCell>
          </TableRow>
        ))}
      </TableBody>
    </Table>
  </React.Fragment>
  );
};

export default InboxPage;
