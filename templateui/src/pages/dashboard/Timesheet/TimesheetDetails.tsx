import React, { useEffect, useState } from 'react';
import TableField from '../../../components/general/TableField';
import { GenericCrudOperations } from '../../../components/general/GenericCrudOperations';
import { TIMESHEET_DAY_DETAILS } from '../../../utils/globalConfig';
import { TimesheetDetailsDto } from '../../../types/Timesheet.type';

interface IProps {
  selectedTimesheet: Date ;
}

const TimesheetDetails = ({ selectedTimesheet }: IProps) => {
  const [timesheetDetail, setTimesheetDetail] = useState<TimesheetDetailsDto[]>([]);
  const [loading, setLoading] = useState<boolean>(false);

  const getDayTimesheet = async (date: Date) => {
    setLoading(true);
    console.log("Fetching timesheet details for date:", date);
    try {
      const response = await GenericCrudOperations.getDetailed(TIMESHEET_DAY_DETAILS, { date }, setTimesheetDetail, setLoading);
      console.log("Timesheet details fetched:", response);
      console.log("Fetched Timesheet Detail:", timesheetDetail); // Add this line
    } catch (error) {
      console.error("Error fetching timesheet details:", error); // Log any errors
    }
    setLoading(false);
  };

  const columns = [
    { key: "projectName", label: "Project Name" },
    { key: "timeSpent", label: "Time Spent" },
    { key: "description", label: "Description" },
  ];

  useEffect(() => {
    console.log("Selected Timesheet Date:", selectedTimesheet); // Add this line
    getDayTimesheet(selectedTimesheet);
  }, [selectedTimesheet]);

  useEffect(() => {
    console.log("Updated Timesheet Detail:", timesheetDetail); // Log state updates
  }, [timesheetDetail]);

  return (
    <>
      {loading && <div>Loading...</div>}
      {!loading && timesheetDetail.length > 0 ? (
        <TableField
          rows={timesheetDetail}
          columns={columns}
          showActions={false}
        />
      ) : (
        <div>No timesheet details available.</div>
      )}
    </>
  );
};

export default TimesheetDetails;
