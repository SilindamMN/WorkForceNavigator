import React, { useEffect, useState } from "react";
import {
  Segment,
  Header,
  List,
  Icon,
  Grid,
  Button,
  ButtonGroup,
  Table,
  TableBody,
  TableRow,
  Divider,
  Form,
  Dropdown,
} from "semantic-ui-react";
import {
  DepartmentDto,
  UserDetailsDto,
  UserDetailsUpdateDto,
} from "../../../types/userDetails.type";
import axiosInstance from "../../../utils/axiosInstance";
import {
  ALL_DEPARTMENTS,
  ALL_JOBTITLES,
  MY_LEAVE_REQUESTS,
  UPDATE_USER_DETAILS,
  USER_DETAILS_URL,
} from "../../../utils/globalConfig";
import toast from "react-hot-toast";
import { IMyLeaveAllocationDto } from "../../../types/leaveAllocation.type";
import { IMyLeaveRequestDto, Status } from "../../../types/leaveRequest.type";
import { RolesEnum } from "../../../types/auth.type";
import { GenericCrudOperations } from "../../../components/general/GenericCrudOperations";
import { IJobTitleDto } from "../../../types/JobTitle.type";
import useAuth from "../../../hooks/useAuth.hook";
import UserUpComingLeaveRequestsPage from "../LeaveRequests/UserUpComingLeaveRequestsPage";

interface IProps {
  username: string;
}

const UserDetails = ({ username }: IProps) => {
  const { user } = useAuth();
  const [loading, setLoading] = useState<boolean>(false);
  const [userDetails, setUserDetails] = useState<UserDetailsDto>();
  const [firstName, setFirstName] = useState<string>("");
  const [lastName, setLastName] = useState<string>("");
  const [email, setEmail] = useState<string>("");
  const [gender, setGender] = useState<string>("");
  const [seniority, setSeniority] = useState<string>("");
  const [phoneNumber, setPhoneNumber] = useState<string>("");
  const [salary, setSalary] = useState<number>(0);
  const [lineManager, setLineManager] = useState<string>("");
  const [jobTitle, setJobtitle] = useState<string>("");
  const [roles, setRoles] = useState<RolesEnum[]>([]);
  const [jobTitles, setJobTitles] = useState<IJobTitleDto[]>([]);

  // For updating user details based on the username
  const UpdateUserDetails = async (
    updateUsername: string,
    userDetail: UserDetailsUpdateDto
  ) => {
    await GenericCrudOperations.updateString(
      UPDATE_USER_DETAILS,
      { updateUsername: updateUsername },
      userDetail,
      setLoading
    );
  };

  const getJobTitles = async () => {
    await GenericCrudOperations.getAll(ALL_JOBTITLES, setJobTitles, setLoading);
  };

  const getUserDetails = async (username: string) => {
    await GenericCrudOperations.getDetails(
      USER_DETAILS_URL,
      username,
      setUserDetails,
      setLoading
    );
  };

  useEffect(() => {
    getUserDetails(username);
    getJobTitles();
  }, [username]);

  useEffect(() => {
    if (userDetails) {
      setFirstName(userDetails.firstName || "");
      setLastName(userDetails.lastName || "");
      setEmail(userDetails.email || "");
      setGender(userDetails.gender || "");
      setLineManager(userDetails.lineManager || "");
      setJobtitle(userDetails.jobTitle || "");
      setPhoneNumber(userDetails.phoneNumber || "");
      setSalary(userDetails.salary || 0);
      setSeniority(userDetails.seniority || "");
    }
  }, [userDetails]);

  const Gender = [
    { key: "Male", text: "Male", value: "Male" },
    { key: "Female", text: "Female", value: "Female" },
  ];

  const handleInputChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    const { name, value } = event.target;
    switch (name) {
      case "firstName":
        setFirstName(value);
        break;
      case "lastName":
        setLastName(value);
        break;
      case "email":
        setEmail(value);
        break;
      case "gender":
        setGender(value);
        break;
      case "phoneNumber":
        setPhoneNumber(value);
        break;
      case "lineManager":
        setLineManager(value);
        break;
      case "jobTitle":
        setJobtitle(value);
        break;
      case "salary":
        setSalary(Number(value));
        break;
      case "seniority":
        setSeniority(value);
        break;
      default:
        break;
    }
  };

  const handleUpdate = () => {
    const updateData: UserDetailsUpdateDto = {
      firstName,
      lastName,
      gender,
      jobTitle,
      salary,
      phoneNumber,
    };
    console.log(username, "And ", updateData); // Log the data before sending
    UpdateUserDetails(username, updateData);
  };

  return (
    <div style={{ padding: "20px" }}>
      <Segment>
        <Form>
          <Form.Group widths="equal">
            <Form.Field>
              <label>First Name</label>
              <input
                type="text"
                name="firstName"
                value={firstName}
                onChange={handleInputChange}
              />
            </Form.Field>
            <Form.Field>
              <label>Last Name</label>
              <input
                type="text"
                name="lastName"
                value={lastName}
                onChange={handleInputChange}
              />
            </Form.Field>
            <Form.Field>
              <label>Gender</label>
              <Dropdown
                selection
                options={Gender}
                value={gender}
                onChange={(e, { value }) => setGender(value as string)}
              />
            </Form.Field>
          </Form.Group>
          <Form.Group widths="equal">
            <Form.Field>
              <label>User Name</label>
              <input type="text" value={userDetails?.username} disabled />
            </Form.Field>
            <Form.Field>
              <label>Line Manager</label>
              <input type="text" value={lineManager} disabled />
            </Form.Field>
            <Form.Field>
              <label>Email</label>
              <input
                type="email"
                name="email"
                value={email}
                onChange={handleInputChange}
              />
            </Form.Field>
          </Form.Group>
          <Form.Group widths="equal">
            <Form.Field>
              <label>Job Title</label>
              <Dropdown
                selection
                options={jobTitles.map((title) => ({
                  key: title.id,
                  text: title.title,
                  value: title.title,
                }))}
                value={jobTitle}
                onChange={(e, { value }) => setJobtitle(value as string)}
              />
            </Form.Field>
            <Form.Field>
              <label>Phone Number</label>
              <input
                type="phoneNumber"
                name="phoneNumber"
                value={phoneNumber}
                onChange={handleInputChange}
              />
            </Form.Field>
            <Form.Field>
              <label>Seniority</label>
              <input
                type="seniority"
                name="seniority"
                value={seniority}
                onChange={handleInputChange}
              />
            </Form.Field>
          </Form.Group>
          <Form.Group widths="equal">
            <Form.Field>
              <label>Salary</label>
              <input
                type="text"
                name="salary"
                value={salary}
                onChange={handleInputChange}
              />
            </Form.Field>
            <Form.Field>
              <label>Assigned Roles</label>
              <input
                placeholder="Assigned Roles"
                type="text"
                value={roles.join(", ")}
                disabled
              />
            </Form.Field>
            <Form.Field>
              <label>Joining Date</label>
              <input
                placeholder="Joining Date"
                type="date"
                value={
                  userDetails?.joiningDate
                    ? new Date(userDetails.joiningDate)
                        .toISOString()
                        .substring(0, 10)
                    : ""
                }
                disabled
              />
            </Form.Field>
          </Form.Group>
          <Button primary type="submit" onClick={handleUpdate}>
            Update User Details
          </Button>
        </Form>
        <Divider horizontal>Upcoming Leaves</Divider>
        <Table size="small" bordered>
          <TableBody>
            <TableRow>
              <Table.Cell>
                <UserUpComingLeaveRequestsPage username={username} />
              </Table.Cell>
            </TableRow>
          </TableBody>
        </Table>
      </Segment>
    </div>
  );
};

export default UserDetails;
