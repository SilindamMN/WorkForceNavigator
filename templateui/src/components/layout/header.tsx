import useAuth from "../../hooks/useAuth.hook";
import { Button } from "@mui/material";
import { AiOutlineHome } from "react-icons/ai";
import { FiLock, FiUnlock } from "react-icons/fi";
import { useNavigate } from "react-router-dom";
import { PATH_DASHBOARD, PATH_PUBLIC } from "../../routes/path";
import SendIcon from '@mui/icons-material/Send';
import LogoutIcon from '@mui/icons-material/Logout';
import DashboardIcon from '@mui/icons-material/Dashboard';
import LockOpenIcon from '@mui/icons-material/LockOpen';
import LockPersonIcon from '@mui/icons-material/LockPerson';

const Header = () => {
 const { isAuthLoading, isAuthenticated, user, logout } = useAuth();
 const navigate = useNavigate();

 const userRolesLabelCreator = () => {
    if (user) {
      let result = "";
      user.roles.forEach((role, index) => {
        result += role;
        if (index < user.roles.length - 1) {
          result += ", ";
        }
      });
      return result;
    }
    return "--";
 };

 return (
    <div className="flex justify-between items-center h-12 px-4">
      <div className="flex items-center gap-4">
        {isAuthenticated ? (
          <div className="flex gap-1">
            <h1 className="px-1 border border-dashed border-purple-300 rounded-lg flex items-center gap-1">
              Auth:
              {isAuthenticated ? (
                <LockOpenIcon className="text-green-600" />
              ) : (
                <LockPersonIcon className="text-red-600" />
              )}
            </h1>
            <h1 className="px-1 border border-dashed border-purple-300 rounded-lg">
              UserName: {user ? user.userName : "--"}
            </h1>
            <h1 className="px-1 border border-dashed border-purple-300 rounded-lg">
              UserRoles: {userRolesLabelCreator()}
            </h1>
          </div>
        ) : (
          ""
        )}
      </div>
      <div className="flex items-center gap-2">
        {isAuthenticated && (
          <>
            <Button
              variant="outlined"
              fullWidth
              sx={{ height: "40px" }} // Adjust the height as needed
              startIcon={<DashboardIcon />}
              onClick={() => navigate(PATH_DASHBOARD.dashboard)}
            >
              Dashboard
            </Button>
            <Button
              variant="outlined"
              fullWidth
              sx={{ height: "40px" }} // Adjust the height as needed
              startIcon={<LogoutIcon />}
              onClick={logout}
            >
              Logout
            </Button>
          </>
        )}
      </div>
    </div>
 );
};

export default Header;
