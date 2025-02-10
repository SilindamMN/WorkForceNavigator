import React, { useEffect } from "react";
import { Modal, Button, Form } from "react-bootstrap";
import { useForm } from "react-hook-form";
import DatePicker from "react-datepicker";

interface IProps {
  isOpen: boolean;
  closeModal: () => void;
  title: string;
  formFields: {
    controlId: string;
    label: string;
    value: any;
    onChange: (value: any) => void;
    options?: { value: any; label: string }[];
    type?: string;
  }[];
  handleSubmit: () => void;
  selectedEntity?: any;
  updateEntity?: (id: any, data: any) => Promise<void>;
  addEntity?: (data: any) => Promise<void>;
  mode: "edit" | "add";
  initialValues?: { [key: string]: any } | null;
}

const GenericModal = ({
  isOpen,
  closeModal,
  title,
  formFields,
  handleSubmit,
  selectedEntity,
  updateEntity,
  addEntity,
  mode,
  initialValues
}: IProps) => {
  const { register, setValue, control } = useForm();

  const handleSave = async () => {
    if (mode === "edit" && updateEntity && selectedEntity) {
      const updateData: { [key: string]: any } = {};
      formFields.forEach((field) => {
        updateData[field.controlId] = field.value;
      });
      updateEntity(selectedEntity.id, updateData);
    } else if (mode === "add" && addEntity) {
      const newData: { [key: string]: any } = {};
      formFields.forEach((field) => {
        newData[field.controlId] = field.value;
      });
      await addEntity(newData);
    }
    closeModal();
  };

  const initialFormData: { [key: string]: any } = {};
  formFields.forEach((field) => {
    initialFormData[field.controlId] = "";
  });

  useEffect(() => {
    if (mode === "edit" && selectedEntity) {
      formFields.forEach((field) => {
        register(field.controlId, { required: true });
        if (selectedEntity.hasOwnProperty(field.controlId)) {
          setValue(field.controlId, selectedEntity[field.controlId], { shouldValidate: true });
        }
      });
    } else if (mode === "edit" && initialValues) {
      formFields.forEach((field) => {
        setValue(field.controlId, initialValues[field.controlId]);
      });
    } else {
      formFields.forEach((field) => {
        setValue(field.controlId, "");
      });
    }
  }, [isOpen, selectedEntity, mode, formFields, register, setValue, initialValues]);

  return (
    <Modal show={isOpen} onHide={closeModal} centered>
      <Modal.Header closeButton>
        <Modal.Title>{title}</Modal.Title>
      </Modal.Header>
      <Modal.Body className="d-flex justify-content-center align-items-center">
        <Form>
          {formFields.map((field, index) => (
            <Form.Group key={index} className="mb-3" controlId={field.controlId}>
              {field.type === "select" && field.options ? (
                <Form.Select defaultValue={field.value} onChange={(e) => field.onChange(e.target.value)}>
                  {field.options.map((option, index) => (
                    <option key={index} value={option.value}>
                      {option.label}
                    </option>
                  ))}
                </Form.Select>
              ) : field.type === "date" ? (
                <DatePicker
                  selected={field.value}
                  onChange={(date) => field.onChange(date)}
                  dateFormat="yyyy-MM-dd"
                  className="form-control"
                />
              ) : (
                <Form.Control
                  type="text"
                  placeholder={field.label}
                  defaultValue={mode === "edit" ? initialValues?.[field.controlId] : field.value}
                  onChange={(e) => field.onChange(e.target.value)}
                />
              )}
            </Form.Group>
          ))}
        </Form>
      </Modal.Body>
      <Modal.Footer className="justify-content-center">
        <Button variant="secondary" onClick={closeModal}>
          Close
        </Button>
        <Button variant="primary" onClick={handleSave}>
          Save Changes
        </Button>
      </Modal.Footer>
    </Modal>
  );
};

export default GenericModal;