import React, { useEffect, useState } from "react";
import { Button, Grid } from "@mui/material";
import AddIcon from "@mui/icons-material/Add";
import { useForm } from "react-hook-form";
import { GridColumn, Segment, Header, Container } from "semantic-ui-react";
import { GenericCrudOperations } from "../../../../components/general/GenericCrudOperations";
import TableField from "../../../../components/general/TableField";
import {  NEW_TEAM_URL,ALL_TEAMS, UPDATE_TEAM_URL, DELETE_TEAM_URL } from "../../../../utils/globalConfig";
import GenericModal from "../GenericModal";
import { ITeamDto, IUpdateTeamDto } from "../../../../types/Team.type";

interface IProps {
  selectedTeamId: (teamId: number | null) => void;
}

const TeamsPage = ({ selectedTeamId }: IProps) => {
  const { control } = useForm();
  const [loading, setLoading] = useState<boolean>(false);
  const [isOpen, setIsOpen] = useState(false);
  const [teams, setTeams] = useState<ITeamDto[]>([]);
  const [selectedTeam, setSelectedTeam] = useState<ITeamDto | null>(null);
  const [teamName, setTeamName] = useState<string | null>(null);
  const [description, setDescription] = useState<string>("");

  const handleOpenModal = () => {
    setIsOpen(true);
    setSelectedTeam(null); // Reset selectedTeam to null
    setTeamName(null); // Reset form fields to empty
    setDescription("");
  };

  const handleCloseModal = () => {
    setIsOpen(false);
  };

  const getTeams = async () => {
    await GenericCrudOperations.getAll(
      ALL_TEAMS,
      setTeams,
      setLoading
    );
  };

  const AddTeam = async (newData: ITeamDto) => {
    await GenericCrudOperations.add(NEW_TEAM_URL, newData, setLoading);
    getTeams();
  };

  const handleRowClick = (team: ITeamDto) => {
    selectedTeamId(team.id);
    setSelectedTeam(team);
  };

  const UpdateTeam = async (
    id: number,
    updatedData: IUpdateTeamDto
  ) => {
    await GenericCrudOperations.update(
      UPDATE_TEAM_URL,
      id,
      updatedData,
      setLoading
    );
  };

  const DeleteTeam = async (id: number) => {
    await GenericCrudOperations.remove(DELETE_TEAM_URL, id, setLoading);
  };

  const handleEdit = (updatedData: ITeamDto) => {
    setSelectedTeam(updatedData);
    setTeamName(updatedData.teamName|| "");
    setDescription(updatedData.description||"");
    handleOpenModal();
  };

  const handleDelete = (id: number) => {
    DeleteTeam(id);
  };

  const handleSubmit = () => {
    handleCloseModal();
  };

  useEffect(() => {
    getTeams();
  }, []);

  const columns = [
    { key: "teamName", label: "Team Name"},
    { key: "description", label: "Description"},
    ];

    const initialValues = {
      id: selectedTeam?.id || null,
      teamName: selectedTeam?.teamName || "",
      description: selectedTeam?.description || "",
    };    
    
  return (
    <div>
      <Container fluid>
        <Grid columns={2}>
          <GridColumn width={16}>
            <Segment raised>
              <Header as="h2" textAlign="center">
                <Button
                  variant="outlined"
                  sx={{ height: "40px" }}
                  startIcon={<AddIcon />}
                  onClick={() => handleOpenModal()}
                >
                  New
                </Button>
              </Header>
              <TableField
                columns={columns}
                rows={teams}
                onEdit={handleEdit}
                onDelete={handleDelete}
                onRowClick={handleRowClick} 
                showActions={true}
                />
            </Segment>
          </GridColumn>
        </Grid>
      </Container>

        <GenericModal
        isOpen={isOpen}
        closeModal={handleCloseModal}
        title="Add"
        formFields={[
          {
            controlId: "teamName",
            label: "team Name",
            value: teamName,
            onChange: setTeamName,
          },
          {
            controlId: "description",
            label: "description",
            value: description,
            onChange: setDescription,
          },
        ]}
        handleSubmit={handleSubmit}
        mode={selectedTeam ? "edit" : "add"}
        selectedEntity={selectedTeam}
        updateEntity={UpdateTeam}
        addEntity={AddTeam}
        initialValues={initialValues}
      />
    </div>
  );
};

export default TeamsPage;