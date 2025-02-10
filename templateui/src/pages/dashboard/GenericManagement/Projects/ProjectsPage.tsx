import React, { useEffect, useState } from "react";
import { Button, Grid } from "@mui/material";
import AddIcon from "@mui/icons-material/Add";
import { useForm } from "react-hook-form";
import { GridColumn, Segment, Header, Container } from "semantic-ui-react";
import { GenericCrudOperations } from "../../../../components/general/GenericCrudOperations";
import TableField from "../../../../components/general/TableField";
import GenericModal from "../GenericModal";
import {
  ICreateProjectDto,
  IProjectDto,
  IUpdateProjectDto,
} from "../../../../types/Project.type";
import {
  ALL_CLIENTS,
  ALL_PROJECTS,
  ALL_TEAMS,
  DELETE_PROJECT_URL,
  NEW_PROJECT_URL,
  UPDATE_PROJECT_URL,
} from "../../../../utils/globalConfig";
import { ITeamDto } from "../../../../types/Team.type";
import { IClientDto } from "../../../../types/Client.type";

interface IProps {
  selectedProjectId: (projectId: number | null) => void;
}

const ProjectsPage = ({ selectedProjectId }: IProps) => {
  const { control } = useForm();
  const [loading, setLoading] = useState<boolean>(false);
  const [isOpen, setIsOpen] = useState(false);
  const [projects, setProjects] = useState<IProjectDto[]>([]);
  const [teams, setTeams] = useState<ITeamDto[]>([]);
  const [clients, setClients] = useState<IClientDto[]>([]);
  const [selectedProject, setSelectedProject] = useState<IProjectDto | null>(null);
  const [projectName, setProjectName] = useState<string | null>(null);
  const [description, setDescription] = useState<string>("");
  const [startDate, setStartDate] = useState<Date | null>(null);
  const [endDate, setEndDate] = useState<Date | null>(null);
  const [selectedTeamId, setSelectedTeamId] = useState<number | null>(null);
  const [selectedClientId, setSelectedClientId] = useState<number | null>(null);

  const handleOpenModal = () => {
    setIsOpen(true);
  };

  const handleCloseModal = () => {
    setIsOpen(false);
    setSelectedProject(null); // Reset selected project after closing modal
  };

  const getProjects = async () => {
    await GenericCrudOperations.getAll(ALL_PROJECTS, setProjects, setLoading);
  };

  const getTeams = async () => {
    await GenericCrudOperations.getAll(ALL_TEAMS, setTeams, setLoading);
  };

  const getClients = async () => {
    await GenericCrudOperations.getAll(ALL_CLIENTS, setClients, setLoading);
  };

  const AddProject = async (newData: ICreateProjectDto) => {
    await GenericCrudOperations.add(NEW_PROJECT_URL, newData, setLoading);
    await getProjects(); // Refresh projects list
  };

  const UpdateProject = async (id: number, updatedData: IUpdateProjectDto) => {
    await GenericCrudOperations.update(
      UPDATE_PROJECT_URL,
      id,
      updatedData,
      setLoading
    );
    await getProjects(); // Refresh projects list
  };

  const DeleteProject = async (id: number) => {
    await GenericCrudOperations.remove(DELETE_PROJECT_URL, id, setLoading);
    await getProjects(); // Refresh projects list
  };

  const handleOpenForAdd = () => {
    setSelectedProject(null);
    setProjectName("");
    setDescription("");
    setStartDate(null);
    setEndDate(null);
    setSelectedTeamId(null);
    setSelectedClientId(null);
    handleOpenModal();
  };

  const handleEdit = (updatedData: IProjectDto) => {
    setSelectedProject(updatedData);
    setProjectName(updatedData.projectName || "");
    setDescription(updatedData.description || "");
    setStartDate(new Date(updatedData.startDate));
    setEndDate(new Date(updatedData.endDate));
    setSelectedTeamId(teams.find((team) => team.teamName === updatedData.teamName)?.id || null);
    setSelectedClientId(clients.find((client) => client.clientName === updatedData.clientName)?.id || null);
    handleOpenModal();
  };

  const handleDelete = (id: number) => {
    DeleteProject(id);
  };

  const handleSubmit = () => {
    handleCloseModal();
  };

  useEffect(() => {
    getProjects();
    getTeams();
    getClients();
  }, []);

  const columns = [
    { key: "projectName", label: "Project Name" },
    { key: "clientName", label: "Client Name" },
    { key: "teamName", label: "Team Name" },
    { key: "description", label: "Description" },
    { key: "startDate", label: "Start Date" },
    { key: "endDate", label: "End Date" },
  ];

  const initialValues = {
    id: selectedProject?.id || null,
    projectName: projectName || "",
    clientId: selectedClientId || null,
    teamId: selectedTeamId || null,
    description: description || "",
    startDate: startDate || null,
    endDate: endDate || null,
  };

  return (
    <div>
      <Container fluid >
        <Grid columns={2}>
          <GridColumn width={16}>
            <Segment raised>
              <Header as="h2" textAlign="center">
                <Button
                  variant="outlined"
                  sx={{ height: "40px" }}
                  startIcon={<AddIcon />}
                  onClick={() => handleOpenForAdd()}
                >
                  New
                </Button>
              </Header>
              <TableField
                columns={columns}
                rows={projects}
                onEdit={handleEdit}
                onDelete={handleDelete}
                showActions={true}
              />
            </Segment>
          </GridColumn>
        </Grid>
      </Container>

      <GenericModal
        isOpen={isOpen}
        closeModal={handleCloseModal}
        title={selectedProject ? "Edit Project" : "Add Project"}
        formFields={[
          {
            controlId: "projectName",
            label: "Project Name",
            value: projectName,
            onChange: setProjectName,
          },
          {
            controlId: "teamId",
            label: "Team",
            value: selectedTeamId || "",
            onChange: (value: any) => setSelectedTeamId(Number(value)),
            options: teams.map((team) => ({
              value: team.id,
              label: team.teamName,
            })),
            type: "select",
          },
          {
            controlId: "clientId",
            label: "Client",
            value: selectedClientId || "",
            onChange: (value: any) => setSelectedClientId(Number(value)),
            options: clients.map((client) => ({
              value: client.id,
              label: client.clientName,
            })),
            type: "select",
          },
          {
            controlId: "description",
            label: "Description",
            value: description,
            onChange: setDescription,
          },
          {
            controlId: "startDate",
            label: "Start Date",
            value: startDate,
            onChange: setStartDate,
            type: "date",
          },
          {
            controlId: "endDate",
            label: "End Date",
            value: endDate,
            onChange: setEndDate,
            type: "date",
          },
        ]}
        handleSubmit={handleSubmit}
        mode={selectedProject ? "edit" : "add"}
        selectedEntity={selectedProject}
        updateEntity={UpdateProject}
        addEntity={AddProject}
        initialValues={initialValues}
      />
    </div>
  );
};

export default ProjectsPage;