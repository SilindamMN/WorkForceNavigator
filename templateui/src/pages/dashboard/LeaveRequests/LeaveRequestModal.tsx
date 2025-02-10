// LeaveRequestModal.js
import React, { useEffect, useState } from "react";
import Button from "react-bootstrap/Button";
import Form from "react-bootstrap/Form";
import Modal from "react-bootstrap/Modal";
import { IMyLeaveRequestDto, IUpdateLeaveRequestDto, IAddLeaveRequestDto } from "../../../types/leaveRequest.type";
import axiosInstance from "../../../utils/axiosInstance";
import { MY_LEAVE_ALLOCATIONS } from "../../../utils/globalConfig";
import toast from "react-hot-toast";
import DatePicker from "react-datepicker";
import "react-datepicker/dist/react-datepicker.css";
import { useNavigate } from 'react-router-dom';
import ManageLeavesPage from "../LeaveAllocations/ManageLeavesPage";
import { PATH_DASHBOARD } from "../../../routes/path";

interface IProps {
  isOpen: boolean;
  closeModal: () => void;
  mode: 'edit' | 'add'; // Indicates whether it's in edit mode or add mode
  selectedRequest?: IMyLeaveRequestDto | null; // Only used in edit mode
  updateLeaveRequest?: (id: number, updatedData: IUpdateLeaveRequestDto) => Promise<void>; // Only used in edit mode
  addLeaveRequest?: (newData: IAddLeaveRequestDto) => Promise<void>; // Only used in add mode
}

const LeaveRequestModal = ({ isOpen, closeModal, mode, selectedRequest, updateLeaveRequest, addLeaveRequest }: IProps) => {
  const [loading, setLoading] = useState<boolean>(false);
  const [leaveAllocations, setLeaveAllocations] = useState<IMyLeaveRequestDto[]>([]);
  const [startDate, setStartDate] = useState<Date>(new Date());
  const [endDate, setEndDate] = useState<Date>(new Date());
  const [leaveName, setLeaveName] = useState<string>("");
  const [comments, setComments] = useState<string>("");
  const [numberOfDays, setNumberOfDays] = useState<number>(0);
  const [status, setStatus] = useState<string>("Pending");
  const navigate = useNavigate();
  const [leaveTypeId, setLeaveTypeId] = useState<number>(0);

  const myLeaveAllocations = async () => {
    try {
      setLoading(true);
      const response = await axiosInstance.get<IMyLeaveRequestDto[]>(
        MY_LEAVE_ALLOCATIONS
      );
      const { data } = response;
      setLeaveAllocations(data);
      setLoading(false);
    } catch (error) {
      toast.error("Failed To Fetch Your Allocated Leaves");
      setLoading(false);
    }
  };

  const handleClose = () => {
    closeModal();
  };

  const handleSave = () => {
    const timeDifference = endDate.getTime() - startDate.getTime();
    const numberOfDays = Math.ceil(timeDifference / (1000 * 60 * 60 * 24));

    if (mode === 'edit' && updateLeaveRequest) {
      const updateData: IUpdateLeaveRequestDto = {
        startDate,
        endDate,
        comments,
        // Assuming status should not be updated during editing
      };
      updateLeaveRequest(selectedRequest!.id, updateData);
    } else if (mode === 'add' && addLeaveRequest) {
      const newData: IAddLeaveRequestDto = {
        startDate,
        endDate,
        leaveTypeid: leaveTypeId// Change this to your actual leave type ID
      };
      addLeaveRequest(newData);
    }

    closeModal();
  };

  useEffect(() => {
    if (mode === 'edit' && selectedRequest) {
      setStartDate(new Date(selectedRequest.startDate));
      setEndDate(new Date(selectedRequest.endDate));
      setLeaveName(selectedRequest.leaveName);
      setComments(selectedRequest.comments);
      setNumberOfDays(selectedRequest.numberOfDays);
      setStatus(selectedRequest.status);
    }
    myLeaveAllocations();
  }, [mode, selectedRequest]);

  return (
    <>
      <Modal show={isOpen} onHide={handleClose} className="custom-modal">
        <Modal.Header closeButton>
          <Modal.Title>{mode === 'edit' ? 'Edit Leave Request' : 'Add Leave Request'}</Modal.Title>
        </Modal.Header>
        <Modal.Body>
          <Form>
            <Form.Group className="mb-3" controlId="formStartDate">
              <Form.Label>Start Date</Form.Label>
              <DatePicker
                selected={startDate}
                onChange={(date: Date) => setStartDate(date)}
                dateFormat="MM/dd/yyyy"
              />
            </Form.Group>
            <Form.Group className="mb-3" controlId="formEndDate">
              <Form.Label>End Date</Form.Label>
              <DatePicker
                selected={endDate}
                onChange={(date: Date) => setEndDate(date)}
                dateFormat="MM/dd/yyyy"
              />
            </Form.Group>

            {mode === 'add' && (
             <Form.Group className="mb-3" controlId="formLeaveName">
             <Form.Label>Leave Name</Form.Label>
             <Form.Select
                value={leaveTypeId} // Use leaveTypeId as the value
                onChange={(e) => setLeaveTypeId(Number(e.target.value))} // Convert the value to a number
             >
                {leaveAllocations.map((allocation, index) => (
                  <option key={index} value={allocation.id}>
                    {allocation.leaveName}
                  </option>
                ))}
             </Form.Select>
            </Form.Group>
            )}

            <Form.Group className="mb-3" controlId="formComments">
              <Form.Label>Comments</Form.Label>
              <Form.Control
                as="textarea"
                rows={3}
                value={comments}
                onChange={(e) => setComments(e.target.value)}
              />
            </Form.Group>
            <Form.Group className="mb-3" controlId="formNumberOfDays">
              <Form.Label>Number of Days</Form.Label>
              <Form.Control type="text" value={numberOfDays} disabled />
            </Form.Group>
            <Form.Group className="mb-3" controlId="formStatus">
              <Form.Label>Status</Form.Label>
              <Form.Control type="text" value={status} disabled />
            </Form.Group>
          </Form>
        </Modal.Body>
        <Modal.Footer>
          <Button variant="secondary" onClick={handleClose}>
            Close
          </Button>
          <Button variant="primary" onClick={handleSave}>
            Save Changes
          </Button>
        </Modal.Footer>
      </Modal>
    </>
  );
};

export default LeaveRequestModal;