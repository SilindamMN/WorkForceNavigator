import React, { useState } from "react";
import { Grid, Segment, Container, Header, GridColumn } from "semantic-ui-react";
import ClientsPage from "./ClientsPage";
import ClientDetailsPage from "./ClientDetailsPage";

const ManageClientPage = () => {
  const [selectedClientId, setSelectedClientId] = useState<number | null>(null);

  const handleClientSelect = (clientId: number | null) => {
    setSelectedClientId(clientId);
  };

  return (
    <Container fluid >
      <Grid columns={2}>
        <GridColumn width={11}>
          <Segment raised>
            <Header as="h2" textAlign="center">
              CLIENTS
            </Header>
            <ClientsPage selectedClientId={handleClientSelect} />
          </Segment>
        </GridColumn>
        <GridColumn width={5}>
          <Segment raised>
            <Header as="h2" textAlign="center">
              CLIENT DETAILS
            </Header>
            {selectedClientId && <ClientDetailsPage selectedClient={selectedClientId} />}
          </Segment>
        </GridColumn>
      </Grid>
    </Container>
  );
};

export default ManageClientPage;