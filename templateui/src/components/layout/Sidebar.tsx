import React from "react";
import useAuth from "../../hooks/useAuth.hook";
import { useNavigate } from "react-router-dom";
import { CiUser } from "react-icons/ci";
import Button from "../general/Button";
import { PATH_DASHBOARD } from "../../routes/path";

const Sidebar = () => {
  const { user } = useAuth();
  const navigate = useNavigate();

  const handleClick = (url:string) => {
    window.scrollTo({ top: 0, left: 0, behavior: "smooth" });
    navigate(url);
  };

  return (
    <div className="shrink-0 bg-[#3e4cb3] w-60 p-2 min-h-[calc(100vh-48px)] flex flex-col items-stretch gap-8">
      <div className="self-center flex flex-col items-center">
        <CiUser className="w-10 h-10 text-white" />
        <h4 className="text-white">
          {user?.firstName}
          {user?.lastName}
        </h4>
      </div>

      <Button
        label="Manage Message"
        onClick={() => handleClick(PATH_DASHBOARD.manageMessage)}
        type="button"
        variant="secondary"
      />
      <Button
        label="My Logs"
        onClick={() => handleClick(PATH_DASHBOARD.myLogs)}
        type="button"
        variant="secondary"
      />
      <Button
        label="All Logs"
        onClick={() => handleClick(PATH_DASHBOARD.systemLogs)}
        type="button"
        variant="secondary"
      />
      <Button
        label="Leave Allocations"
        onClick={() => handleClick(PATH_DASHBOARD.allLeaveAllocations)}
        type="button"
        variant="primary"
      />
      <Button
        label="By Leave Name"
        onClick={() => handleClick(PATH_DASHBOARD.allocationByLeaveName)}
        type="button"
        variant="secondary"
      />
      <Button
        label="By User Name"
        onClick={() => handleClick(PATH_DASHBOARD.allocationByusername)}
        type="button"
        variant="secondary"
      />
      <Button
        label="My Allocation"
        onClick={() => handleClick(PATH_DASHBOARD.myAllocation)}
        type="button"
        variant="secondary"
      />
      <Button
        label="Owner Page"
        onClick={() => handleClick(PATH_DASHBOARD.owner)}
        type="button"
        variant="secondary"
      />
      <Button
        label="Admin Page"
        onClick={() => handleClick(PATH_DASHBOARD.admin)}
        type="button"
        variant="secondary"
      />
      <Button
        label="Manager Page"
        onClick={() => handleClick(PATH_DASHBOARD.manager)}
        type="button"
        variant="secondary"
      />
      <Button
        label="User Page"
        onClick={() => handleClick(PATH_DASHBOARD.user)}
        type="button"
        variant="secondary"
      />
    </div>
  );
};

export default Sidebar;