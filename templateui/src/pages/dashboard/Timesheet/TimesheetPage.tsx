import React, { useEffect, useState } from "react";
import { Container, Grid, GridColumn, Segment, Header, Button, Icon } from "semantic-ui-react";
import TableField from "../../../components/general/TableField";
import { GenericCrudOperations } from "../../../components/general/GenericCrudOperations";
import { DELETE_DEPARTMENT_URL, MY_TIMESHEETS } from "../../../utils/globalConfig";
import { format, startOfWeek, addWeeks, addDays } from 'date-fns';
import { TimesheetDto } from "../../../types/Timesheet.type";

interface IProps {
  selectedTimesheetDate: (timesheetDate: Date) => void;
}

const TimesheetPage = ({ selectedTimesheetDate }: IProps) => {
  const [timesheets, setTimesheets] = useState<TimesheetDto[]>([]);
  const [selectedTimesheet, setSelectedTimesheet] = useState<TimesheetDto | null>(null);
  const [isOpen, setIsOpen] = useState(false);
  const [loading, setLoading] = useState<boolean>(false);
  const [weekOffset, setWeekOffset] = useState<number>(0);

  const handleOpenModal = () => {
    setIsOpen(true);
  };

  const handleCloseModal = () => {
    setIsOpen(false);
  };

  const getWeeklyTimesheet = async (weekOffset: number) => {
    setLoading(true);
    const baseDate = new Date();
    const startDate = startOfWeek(addWeeks(baseDate, weekOffset), { weekStartsOn: 1 });
    console.log("Fetching timesheets for week starting:", startDate);

    try {
      const response = await GenericCrudOperations.getDetailed(
        MY_TIMESHEETS,
        { weekOffset }, // Ensure the date is correctly formatted for the API
        setTimesheets,
        setLoading
      );
      console.log("Timesheets fetched:", response); // Add logging to check the response
    } catch (error) {
      console.error("Error fetching timesheets:", error);
    }

    setLoading(false);
  };

  const deleteTimesheet = async (id: number) => {
    setLoading(true);
    try {
      await GenericCrudOperations.remove(DELETE_DEPARTMENT_URL, id, setLoading);
      console.log("Deleted timesheet with ID:", id);
    } catch (error) {
      console.error("Error deleting timesheet:", error);
    }
    setLoading(false);
  };

  const handleEdit = (updatedData: TimesheetDto) => {
    setSelectedTimesheet(updatedData);
    handleOpenModal();
  };

  const handleRowClick = (timesheet: TimesheetDto) => {
    console.log("Timesheet clicked:", timesheet); // Add logging
    selectedTimesheetDate(timesheet.date);
  };

  const handleDelete = async (id: number) => {
    await deleteTimesheet(id);
    getWeeklyTimesheet(weekOffset); // Refresh the timesheets after deletion
  };

  const columns = [
    { key: "dayName", label: "Day" },
    { key: "date", label: "Date" },
    { key: "totalHours", label: "Total Hours" },
    { key: "projectNames", label: "Project Names" },
  ];

  const handlePreviousWeek = () => {
    setWeekOffset((prev) => prev - 1);
    console.log("Previous week offset:", weekOffset - 1); // Add logging
  };

  const handleNextWeek = () => {
    setWeekOffset((prev) => prev + 1);
    console.log("Next week offset:", weekOffset + 1); // Add logging
  };

  useEffect(() => {
    console.log("Current week offset:", weekOffset); // Add logging
    getWeeklyTimesheet(weekOffset);
  }, [weekOffset]);

  return (
    <Container fluid>
      <Grid columns={2}>
        <GridColumn width={16}>
          <Segment raised>
            <Header as="h2" textAlign="center">
              <Button variant="outlined" sx={{ height: "40px" }} startIcon={<Icon name="add" />}>
                New Timesheet Entry
              </Button>
              <br />
              <Button onClick={handlePreviousWeek}>
                <Icon name="arrow left" />
              </Button>
              {`${format(startOfWeek(addWeeks(new Date(), weekOffset), { weekStartsOn: 1 }), 'MMMM do, yyyy')} - ${format(addDays(startOfWeek(addWeeks(new Date(), weekOffset), { weekStartsOn: 1 }), 4), 'MMMM do, yyyy')}`}
              <Button onClick={handleNextWeek}>
                <Icon name="arrow right" />
              </Button>
            </Header>
            <TableField
              rows={timesheets}
              columns={columns}
              onEdit={handleEdit}
              onDelete={handleDelete}
              onRowClick={handleRowClick}
            />
          </Segment>
        </GridColumn>
      </Grid>
    </Container>
  );
};

export default TimesheetPage;
