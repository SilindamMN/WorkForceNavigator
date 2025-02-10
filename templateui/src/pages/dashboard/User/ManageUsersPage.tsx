import React, { useEffect, useState } from "react";
import { Grid, Segment, Container, Header, GridColumn } from "semantic-ui-react";
import UsersManagementPage from "./UserManagementPage";
import UserDetails from "./UserDetails";

const ManageUsersPage = () => {
  const [selectedUser, setSelectedUser] = useState<string | null>(null);

  const handleUserSelect = (username: string |null) => {
    setSelectedUser(username);
  };
  useEffect(() => {
    return () => {
      setSelectedUser(null);
    };
  }, []);
  
  return (
    <Container fluid className="pageTemplate3">
      <Grid columns={2}>
        <GridColumn width={5} >
          <Segment raised>
            <Header as="h4" textAlign="center" style={{ marginTop: '0%'}}>
              USERS LIST
            </Header>
            <UsersManagementPage selectedUsername={handleUserSelect}/>
          </Segment>
        </GridColumn>

        <GridColumn width={11}>
          <Segment raised>
            <Header as="h4" textAlign="center">
              USER DETAIL
            </Header>
            {selectedUser && <UserDetails username={selectedUser} />}
          </Segment>
        </GridColumn>
      </Grid>
    </Container>
  );
};

export default ManageUsersPage;