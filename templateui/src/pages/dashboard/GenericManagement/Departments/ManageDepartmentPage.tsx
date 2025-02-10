import React, { useState } from "react";
import { Grid, Segment, Container, Header, GridColumn } from "semantic-ui-react";
import DepartmentsPage from "./DepartmentsPage";
import DepartmentDetails from "./DepartmentDetails";

const ManageDepartmentPage = () => {
  const [selectedDepartmentId, setSelectedDepartmentId] = useState<number | null>(null);

  const handleDepartmentSelect = (departmentId: number | null) => {
    setSelectedDepartmentId(departmentId);
  };

  return (
    <Container fluid>
      <Grid columns={2}>
        <GridColumn width={8}>
          <Segment raised>
            <Header as="h2" textAlign="center">
              DEPARTMENTS
            </Header>
            <DepartmentsPage selectedDepartmentId={handleDepartmentSelect} />
          </Segment>
        </GridColumn>
        <GridColumn width={8}>
          <Segment raised>
            <Header as="h2" textAlign="center">
              DEPARTMENT DETAILS
            </Header>
            {selectedDepartmentId && <DepartmentDetails selectedDepartment={selectedDepartmentId} />}
          </Segment>
        </GridColumn>
      </Grid>
    </Container>
  );
};

export default ManageDepartmentPage;