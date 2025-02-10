import React, { useState } from "react";
import { Grid, Segment, Container, Header, GridColumn } from "semantic-ui-react";
import ProjectsPage from "./ProjectsPage";

const ManageProjectPage = () => {
  const [selectedProjectId, setSelectedProjectId] = useState<number | null>(null);

  const handleProjectSelect = (projectId: number | null) => {
    setSelectedProjectId(projectId);
  };

  return (
    <Container fluid>
      <Grid columns={2}>
        <GridColumn width={16}>
          <Segment raised>
            <Header as="h2" textAlign="center">
              PROJECTS
            </Header>
            <ProjectsPage selectedProjectId={handleProjectSelect} />
          </Segment>
        </GridColumn>
      </Grid>
    </Container>
  );
};

export default ManageProjectPage;
