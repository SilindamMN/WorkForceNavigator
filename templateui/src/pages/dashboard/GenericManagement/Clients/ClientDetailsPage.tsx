import React, { useEffect, useState } from 'react';
import { GenericCrudOperations } from '../../../../components/general/GenericCrudOperations';
import TableField from '../../../../components/general/TableField';
import { IClientDetails } from '../../../../types/Client.type';
import { CLIENT_DETAILS } from '../../../../utils/globalConfig';

interface IProps {
  selectedClient: number;
}

const ClientDetailsPage = ({ selectedClient }: IProps) => {
  const [clientDetails, setClientDetails] = useState<IClientDetails[]>([]);
  const [loading, setLoading] = useState<boolean>(false);

  const getClientDetails = async (id: number) => {
    await GenericCrudOperations.getDetails(CLIENT_DETAILS, id, setClientDetails, setLoading);
  };

  const columns = [
    { key: "projectName", label: "Project Name" },
  ];

  useEffect(() => {
      getClientDetails(selectedClient);
  }, [selectedClient]);

  return (
    <TableField
      rows={clientDetails}
      columns={columns}
      showActions={false}
    />
  );
};

export default ClientDetailsPage;