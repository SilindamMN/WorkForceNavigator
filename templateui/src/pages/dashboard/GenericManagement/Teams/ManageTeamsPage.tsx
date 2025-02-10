import React, { useState } from "react";
import { Grid, Segment, Container, Header, GridColumn } from "semantic-ui-react";
import TeamsPage from "./TeamsPage";
import TeamDetails from "./TeamsDetails";

const ManageTeamPage = () => {
  const [selectedTeamId, setSelectedTeamId] = useState<number | null>(null);

  const handleTeamSelect = (departmentId: number | null) => {
    setSelectedTeamId(departmentId);
  };

  return (
    <Container fluid className="pageTemplate3">
      <Grid columns={2}>
        <GridColumn width={8}>
          <Segment raised>
            <Header as="h2" textAlign="center">
              DEPARTMENTS
            </Header>
            <TeamsPage selectedTeamId={handleTeamSelect} />
          </Segment>
        </GridColumn>
        <GridColumn width={8}>
          <Segment raised>
            <Header as="h2" textAlign="center">
              DEPARTMENT DETAILS
            </Header>
            {selectedTeamId && <TeamDetails selectedTeam={selectedTeamId} />}
          </Segment>
        </GridColumn>
      </Grid>
    </Container>
  );
};

export default ManageTeamPage;