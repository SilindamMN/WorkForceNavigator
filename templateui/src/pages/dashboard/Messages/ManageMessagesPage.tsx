import React from "react";
import {
 Grid,
 GridColumn,
 Segment,
 Container,
 Header,
 Button,
 Icon,
 GridRow,
} from "semantic-ui-react";
import InboxPage from "./InboxPage";
import SendMessagePage from "./SendMessagePage";
import UserDetails from "../User/UserDetails";

const ManageMessagesPage = () => {
 return (
    <Container fluid className="pageTemplate3">
      <Grid columns={2}>
        <GridColumn>
          <Segment raised>
            <Header as='h2' textAlign='center'>Inbox</Header>
            <InboxPage />
          </Segment>
        </GridColumn>

        <GridColumn>
          <Segment raised>
            <Header as='h2' textAlign='center'>Send Message</Header>
            <SendMessagePage/>
          </Segment>
          <Segment raised>
            <Header as='h2' textAlign='center'>User Details</Header>
            <UserDetails/>
          </Segment>
        </GridColumn>
      </Grid>
    </Container>
 );
};

export default ManageMessagesPage;
