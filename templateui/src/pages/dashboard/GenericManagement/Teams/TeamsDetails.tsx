import React, { useEffect, useState } from 'react';
import { GenericCrudOperations } from '../../../../components/general/GenericCrudOperations';
import TableField from '../../../../components/general/TableField';
import { ITeamDetails } from '../../../../types/Team.type';
import { TEAM_DETAILS } from '../../../../utils/globalConfig';

interface IProps {
  selectedTeam: number;
}

const TeamDetails = ({ selectedTeam }: IProps) => {
  const [departmentUserJobTitleTeam, setTeamUserJobTitleTeam] = useState<ITeamDetails[]>([]);
  const [loading, setLoading] = useState<boolean>(false);

  const getTeamDetails = async (id: number) => {
    await GenericCrudOperations.getDetails(TEAM_DETAILS, id, setTeamUserJobTitleTeam, setLoading);
  };

  const columns = [
    { key: "firstName", label: "First Name" },
    { key: "lastName", label: "Last Name" },
    { key: "email", label: "Email" },
    { key: "jobTitle", label: "Job Title" },
    { key: "team", label: "Team" },
  ];

  useEffect(() => {
      getTeamDetails(selectedTeam);
  }, [selectedTeam]);

  return (
    <TableField
    rows={departmentUserJobTitleTeam}
    columns={columns}
      showActions={false}
    />
  );
};

export default TeamDetails;