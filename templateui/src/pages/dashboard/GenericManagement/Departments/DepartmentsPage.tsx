import React, { useEffect, useState } from "react";
import { Button, Grid } from "@mui/material";
import AddIcon from "@mui/icons-material/Add";
import { useForm } from "react-hook-form";
import { GridColumn, Segment, Header, Container } from "semantic-ui-react";
import { GenericCrudOperations } from "../../../../components/general/GenericCrudOperations";
import TableField from "../../../../components/general/TableField";
import { IDepartmentDto, IUpdateDepartmentDto } from "../../../../types/Department.type";
import { ALL_DEPARTMENTS, NEW_DEPARTMENT_URL, UPDATE_DEPARTMENT_URL, DELETE_DEPARTMENT_URL } from "../../../../utils/globalConfig";
import GenericModal from "../GenericModal";

interface IProps {
  selectedDepartmentId: (departmentId: number | null) => void;
}

const DepartmentsPage = ({ selectedDepartmentId }: IProps) => {
  const { control } = useForm();
  const [loading, setLoading] = useState<boolean>(false);
  const [isOpen, setIsOpen] = useState(false);
  const [departments, setDepartments] = useState<IDepartmentDto[]>([]);
  const [selectedDepartment, setSelectedDepartment] = useState<IDepartmentDto | null>(null);
  const [departmentName, setDepartmentName] = useState<string | null>(null);
  const [description, setDescription] = useState<string | null>(null);

  const handleOpenModal = () => {
    setIsOpen(true);
    setSelectedDepartment(null); // Reset selectedDepartment to null
    setDepartmentName(null); // Reset form fields to empty
    setDescription(null);
  };

  const handleCloseModal = () => {
    setIsOpen(false);
  };

  const getDepartments = async () => {
    await GenericCrudOperations.getAll(
      ALL_DEPARTMENTS,
      setDepartments,
      setLoading
    );
  };

  const AddDepartment = async (newData: IDepartmentDto) => {
    await GenericCrudOperations.add(NEW_DEPARTMENT_URL, newData, setLoading);
    getDepartments();
  };

  const handleRowClick = (department: IDepartmentDto) => {
    selectedDepartmentId(department.id);
    setSelectedDepartment(department);
  };

  const UpdateDepartment = async (
    id: number,
    updatedData: IUpdateDepartmentDto
  ) => {
    await GenericCrudOperations.update(
      UPDATE_DEPARTMENT_URL,
      id,
      updatedData,
      setLoading
    );
  };

  const DeleteDepartment = async (id: number) => {
    await GenericCrudOperations.remove(DELETE_DEPARTMENT_URL, id, setLoading);
  };

  const handleEdit = (updatedData: IDepartmentDto) => {
    setSelectedDepartment(updatedData);
    setDepartmentName(updatedData.departmentName|| "");
    setDescription(updatedData.description||"");
    handleOpenModal();
  };

  const handleDelete = (id: number) => {
    DeleteDepartment(id);
  };

  const handleSubmit = () => {
    handleCloseModal();
  };

  useEffect(() => {
    getDepartments();
  }, []);

  const columns = [
    { key: "departmentName", label: "Department Name"},
    { key: "description", label: "Description"},
    ];

    const initialValues = {
      id: selectedDepartment?.id || null,
      departmentName: selectedDepartment?.departmentName || "",
      description: selectedDepartment?.description || "",
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
                rows={departments}
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
            onChange: setDepartmentName,
          },
          {
            controlId: "description",
            label: "description",
            value: description,
            onChange: setDescription,
          },
        ]}
        handleSubmit={handleSubmit}
        mode={selectedDepartment ? "edit" : "add"}
        selectedEntity={selectedDepartment}
        updateEntity={UpdateDepartment}
        addEntity={AddDepartment}
        initialValues={initialValues}
      />
    </div>
  );
};

export default DepartmentsPage;