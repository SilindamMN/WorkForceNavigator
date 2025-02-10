import React, { useEffect, useState } from "react";
import { Grid, Segment, Container, Header, GridColumn } from "semantic-ui-react";
import TimesheetPage from "./TimesheetPage";
import TimesheetDetails from "./TimesheetDetails";

const ManageTimesheetPage = () => {
  const [selectedTimesheetDate, setSelectedTimesheetDate] = useState<Date | null>(null);

  const handleTimesheetSelect = (timesheetDate: Date) => {
    console.log("Selected Timesheet Date in ManageTimesheetPage:", timesheetDate); // Add this line
    setSelectedTimesheetDate(timesheetDate);
  };

  return (
    <Container fluid className="pageTemplate3">
      <Grid columns={2}>
        <GridColumn width={10}>
          <Segment raised>
            <Header as="h2" textAlign="center">
              {/* WEEKLY TIMESHEET */}
            </Header>
            <TimesheetPage selectedTimesheetDate={handleTimesheetSelect} />
          </Segment>
        </GridColumn>

        <GridColumn width={6}>
          <Segment raised>
            <Header as="h2" textAlign="center">
              {/* TIMESHEET DETAILS */}
            </Header>
            {selectedTimesheetDate && <TimesheetDetails selectedTimesheet={selectedTimesheetDate} />}
          </Segment>
        </GridColumn>
      </Grid>
    </Container>
  );
};

export default ManageTimesheetPage;
