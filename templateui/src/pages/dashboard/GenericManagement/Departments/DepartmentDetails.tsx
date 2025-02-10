import React, { useEffect, useState } from 'react';
import { GenericCrudOperations } from '../../../../components/general/GenericCrudOperations';
import TableField from '../../../../components/general/TableField';
import { IDepartmentTeamJobTitle } from '../../../../types/Department.type';
import { DEPARTMENT_JOBTITLE_TEAM } from '../../../../utils/globalConfig';

interface IProps {
  selectedDepartment: number;
}

const DepartmentDetails = ({ selectedDepartment }: IProps) => {
  const [departmentUserJobTitleTeam, setDepartmentUserJobTitleTeam] = useState<IDepartmentTeamJobTitle[]>([]);
  const [loading, setLoading] = useState<boolean>(false);

  const getDepartmentDetails = async (id: number) => {
    await GenericCrudOperations.getDetails(DEPARTMENT_JOBTITLE_TEAM, id, setDepartmentUserJobTitleTeam, setLoading);
  };

  const columns = [
    { key: "firstName", label: "First Name" },
    { key: "lastName", label: "Last Name" },
    { key: "email", label: "Email" },
    { key: "jobTitle", label: "Job Title" },
    { key: "team", label: "Team" },
  ];

  useEffect(() => {
      getDepartmentDetails(selectedDepartment);
  }, [selectedDepartment]);

  return (
    <TableField
      rows={departmentUserJobTitleTeam}
      columns={columns}
      showActions={false}
    />
  );
};

export default DepartmentDetails;