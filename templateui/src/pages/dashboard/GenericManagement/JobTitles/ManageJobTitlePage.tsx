import React, { useState } from "react";
import { Grid, Segment, Container, Header, GridColumn } from "semantic-ui-react";
import JobTitleDetails from "./JobTitleDetails";
import JobTitlesPage from "./JobTitlesPage";

const ManageJobTitlePage = () => {
  const [selectedJobTitleId, setSelectedJobTitleId] = useState<number | null>(null);

  const handleJobTitleSelect = (jobTitleId: number | null) => {
    setSelectedJobTitleId(jobTitleId);
  };

  return (
    <Container fluid>
      <Grid columns={2}>
        <GridColumn width={8}>
          <Segment raised>
            <Header as="h2" textAlign="center">
              JOB TITLES
            </Header>
            <JobTitlesPage selectedJobTitleId={handleJobTitleSelect} />
          </Segment>
        </GridColumn>
        <GridColumn width={8}>
          <Segment raised>
            <Header as="h2" textAlign="center">
              JOB TITLE DETAILS
            </Header>
            {selectedJobTitleId && <JobTitleDetails selectedJobTitle={selectedJobTitleId} />}
          </Segment>
        </GridColumn>
      </Grid>
    </Container>
  );
};

export default ManageJobTitlePage;