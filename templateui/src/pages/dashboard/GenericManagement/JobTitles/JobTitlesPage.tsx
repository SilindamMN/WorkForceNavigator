import React, { useEffect, useState } from "react";
import { Button, Grid } from "@mui/material";
import AddIcon from "@mui/icons-material/Add";
import { useForm } from "react-hook-form";
import { GridColumn, Segment, Header, Container } from "semantic-ui-react";
import { GenericCrudOperations } from "../../../../components/general/GenericCrudOperations";
import TableField from "../../../../components/general/TableField";
import { IJobTitleDto, IUpdateJobTitleDto } from "../../../../types/JobTitle.type";
import {  NEW_JOBTITLE_URL, UPDATE_JOBTITLE_URL, DELETE_JOBTITLE_URL, ALL_JOBTITLES } from "../../../../utils/globalConfig";
import GenericModal from "../GenericModal";

interface IProps {
  selectedJobTitleId: (departmentId: number | null) => void;
}

const JobTitlesPage = ({ selectedJobTitleId }: IProps) => {
  const { control } = useForm();
  const [loading, setLoading] = useState<boolean>(false);
  const [isOpen, setIsOpen] = useState(false);
  const [jobTitles, setJobTitles] = useState<IJobTitleDto[]>([]);
  const [selectedJobTitle, setSelectedJobTitle] = useState<IJobTitleDto | null>(null);
  const [departmentName, setJobTitleName] = useState<string | null>(null);
  const [description, setDescription] = useState<string>("");

  const handleOpenModal = () => {
    setIsOpen(true);
    setSelectedJobTitle(null); // Reset selectedJobTitle to null
    setJobTitleName(null); // Reset form fields to empty
    setDescription("");
  };

  const handleCloseModal = () => {
    setIsOpen(false);
  };

  const getJobTitles = async () => {
    await GenericCrudOperations.getAll(
      ALL_JOBTITLES,
      setJobTitles,
      setLoading
    );
  };

  const AddJobTitle = async (newData: IJobTitleDto) => {
    await GenericCrudOperations.add(NEW_JOBTITLE_URL, newData, setLoading);
    getJobTitles();
  };

  const handleRowClick = (department: IJobTitleDto) => {
    selectedJobTitleId(department.id);
    setSelectedJobTitle(department);
  };

  const UpdateJobTitle = async (
    id: number,
    updatedData: IUpdateJobTitleDto
  ) => {
    await GenericCrudOperations.update(
      UPDATE_JOBTITLE_URL,
      id,
      updatedData,
      setLoading
    );
  };

  const DeleteJobTitle = async (id: number) => {
    await GenericCrudOperations.remove(DELETE_JOBTITLE_URL, id, setLoading);
  };

  const handleEdit = (updatedData: IJobTitleDto) => {
    setSelectedJobTitle(updatedData);
    setJobTitleName(updatedData.title|| "");
    setDescription(updatedData.description||"");
    handleOpenModal();
  };

  const handleDelete = (id: number) => {
    DeleteJobTitle(id);
  };

  const handleSubmit = () => {
    handleCloseModal();
  };

  useEffect(() => {
    getJobTitles();
  }, []);

  const columns = [
    { key: "title", label: "Title"},
    { key: "departmentName", label: "Department"},
    { key: "seniority", label: "Seniority"},
    { key: "description", label: "Description"},
    ];

    const initialValues = {
      id: selectedJobTitle?.id || null,
      departmentName: selectedJobTitle?.description || "",
      description: selectedJobTitle?.description || "",
      title: selectedJobTitle?.title || "",
      seniority: selectedJobTitle?.seniority || "",
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
                  onClick={() => handleOpenModal()}
                >
                  New
                </Button>
              </Header>
              <TableField
                columns={columns}
                rows={jobTitles}
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
            controlId: "departmentName",
            label: "department Name",
            value: departmentName,
            onChange: setJobTitleName,
          },
          {
            controlId: "description",
            label: "description",
            value: description,
            onChange: setDescription,
          },
        ]}
        handleSubmit={handleSubmit}
        mode={selectedJobTitle ? "edit" : "add"}
        selectedEntity={selectedJobTitle}
        updateEntity={UpdateJobTitle}
        addEntity={AddJobTitle}
        initialValues={initialValues}
      />
    </div>
  );
};

export default JobTitlesPage;