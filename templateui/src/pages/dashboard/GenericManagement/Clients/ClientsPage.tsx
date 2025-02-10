import React, { useEffect, useState } from "react";
import { Button, Grid } from "@mui/material";
import AddIcon from "@mui/icons-material/Add";
import { useForm } from "react-hook-form";
import { GridColumn, Segment, Header, Container } from "semantic-ui-react";
import { GenericCrudOperations } from "../../../../components/general/GenericCrudOperations";
import TableField from "../../../../components/general/TableField";
import GenericModal from "../GenericModal";
import { IClientDto, IUpdateClientDto } from "../../../../types/Client.type";
import { ALL_CLIENTS, DELETE_CLIENT_URL, NEW_CLIENT_URL, UPDATE_CLIENT_URL } from "../../../../utils/globalConfig";

interface IProps {
  selectedClientId: (clientId: number | null) => void;
}

const ClientsPage = ({ selectedClientId }: IProps) => {
  const { control } = useForm();
  const [loading, setLoading] = useState<boolean>(false);
  const [isOpen, setIsOpen] = useState(false);
  const [clients, setClients] = useState<IClientDto[]>([]);
  const [selectedClient, setSelectedClient] = useState<IClientDto | null>(null);
  const [clientName, setClientName] = useState<string | null>(null);
  const [phone, setPhone] = useState<string | null>(null);
  const [email, setEmail] = useState<string | null>(null);

  const handleOpenModal = () => {
    setIsOpen(true);
    setSelectedClient(null); // Reset selectedClient to null
    setClientName(null); // Reset form fields to empty
    setPhone(null); // Reset form fields to empty
    setEmail(null); // Reset form fields to empty
    setEmail("");
  };

  const handleCloseModal = () => {
    setIsOpen(false);
  };

  const getClients = async () => {
    await GenericCrudOperations.getAll(
      ALL_CLIENTS,
      setClients,
      setLoading
    );
  };

  const AddClient = async (newData: IClientDto) => {
    await GenericCrudOperations.add(NEW_CLIENT_URL, newData, setLoading);
    getClients();
  };

  const handleRowClick = (client: IClientDto) => {
    selectedClientId(client.id);
    setSelectedClient(client);
  };

  const UpdateClient = async (
    id: number,
    updatedData: IUpdateClientDto
  ) => {
    await GenericCrudOperations.update(
      UPDATE_CLIENT_URL,
      id,
      updatedData,
      setLoading
    );
  };

  const DeleteClient = async (id: number) => {
    await GenericCrudOperations.remove(DELETE_CLIENT_URL, id, setLoading);
  };

  const handleEdit = (updatedData: IClientDto) => {
    setSelectedClient(updatedData);
    setClientName(updatedData.clientName|| "");
    setEmail(updatedData.email||"");
    setPhone(updatedData.phone||"");
    handleOpenModal();
  };

  const handleDelete = (id: number) => {
    DeleteClient(id);
  };

  const handleSubmit = () => {
    handleCloseModal();
  };

  useEffect(() => {
    getClients();
  }, []);

  const columns = [
    { key: "clientName", label: "Client Name"},
    { key: "email", label: "Email"},
    { key: "phone", label: "Phone"},
    ];

    const initialValues = {
      id: selectedClient?.id || null,
      clientName: selectedClient?.clientName || "",
      email: selectedClient?.email || "",
      phone: selectedClient?.phone || "",
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
                rows={clients}
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
            controlId: "clientName",
            label: "client Name",
            value: clientName,
            onChange: setClientName,
          },
          {
            controlId: "email",
            label: "email",
            value: email,
            onChange: setEmail,
          },
          {
            controlId: "phone",
            label: "phone",
            value: phone,
            onChange: setPhone,
          },
        ]}
        handleSubmit={handleSubmit}
        mode={selectedClient ? "edit" : "add"}
        selectedEntity={selectedClient}
        updateEntity={UpdateClient}
        addEntity={AddClient}
        initialValues={initialValues}
      />
    </div>
  );
};

export default ClientsPage;