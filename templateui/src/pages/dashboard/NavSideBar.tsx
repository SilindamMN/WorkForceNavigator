import * as React from 'react';
import { styled, createTheme, ThemeProvider } from '@mui/material/styles';
import CssBaseline from '@mui/material/CssBaseline';
import MuiDrawer from '@mui/material/Drawer';
import Box from '@mui/material/Box';
import MuiAppBar, { AppBarProps as MuiAppBarProps } from '@mui/material/AppBar';
import Toolbar from '@mui/material/Toolbar';
import Typography from '@mui/material/Typography';
import Divider from '@mui/material/Divider';
import IconButton from '@mui/material/IconButton';
import Badge from '@mui/material/Badge';
import Button from '@mui/material/Button';
import MenuIcon from '@mui/icons-material/Menu';
import ChevronLeftIcon from '@mui/icons-material/ChevronLeft';
import NotificationsIcon from '@mui/icons-material/Notifications';
import InboxIcon from '@mui/icons-material/MoveToInbox';
import FamilyRestroomIcon from '@mui/icons-material/FamilyRestroom';
import SendIcon from '@mui/icons-material/Send';
import ApartmentIcon from '@mui/icons-material/Apartment';
import { PATH_DASHBOARD } from '../../routes/path';
import CalendarMonthIcon from '@mui/icons-material/CalendarMonth';
import { useNavigate } from 'react-router-dom';
import Header from '../../components/layout/header';
import { boolean } from 'yup';
import SupervisorAccountIcon from '@mui/icons-material/SupervisorAccount';

const drawerWidth: number = 240;

interface AppBarProps extends MuiAppBarProps {
  open?: boolean;
}

const AppBar = styled(MuiAppBar, {
  shouldForwardProp: (prop) => prop !== 'open',
})<AppBarProps>(({ theme, open }) => ({
  zIndex: theme.zIndex.drawer + 1,
  transition: theme.transitions.create(['width', 'margin'], {
    easing: theme.transitions.easing.sharp,
    duration: theme.transitions.duration.leavingScreen,
  }),
  ...(open && {
    marginLeft: drawerWidth,
    width: `calc(100% - ${drawerWidth}px)`,
    transition: theme.transitions.create(['width', 'margin'], {
      easing: theme.transitions.easing.sharp,
      duration: theme.transitions.duration.enteringScreen,
    }),
  }),
}));

const Drawer = styled(MuiDrawer, { shouldForwardProp: (prop) => prop !== 'open' })(
  ({ theme, open }) => ({
    '& .MuiDrawer-paper': {
      position: 'relative',
      whiteSpace: 'nowrap',
      width: drawerWidth,
      transition: theme.transitions.create('width', {
        easing: theme.transitions.easing.sharp,
        duration: theme.transitions.duration.enteringScreen,
      }),
      boxSizing: 'border-box',
      ...(!open && {
        overflowX: 'hidden',
        transition: theme.transitions.create('width', {
          easing: theme.transitions.easing.sharp,
          duration: theme.transitions.duration.leavingScreen,
        }),
        width: theme.spacing(7),
        [theme.breakpoints.up('sm')]: {
          width: theme.spacing(9),
        },
      }),
    },
  }),
);

const defaultTheme = createTheme({
  palette: {
    mode: 'light', // Set theme mode to light
    background: {
      default: '#FFFFFF', // Set default background color to white
    },
  },
});

export default function NavSideBar() {
  const [open, setOpen] = React.useState(true);
  const navigate = useNavigate();
  const toggleDrawer = () => {
    setOpen(!open);
  };

  return (
    <ThemeProvider theme={defaultTheme}>
      <CssBaseline /> {/* Apply global CSS reset */}
      <Box sx={{ display: 'flex',bgcolor: 'background.default' }}>
        <AppBar position="absolute" open={open}>
          <Toolbar
            sx={{
              pr: '24px', // keep right padding when drawer closed
              background: 'white', // Set background color to white
            }}
          >
            <IconButton
              edge="start"
              color="inherit"
              aria-label="open drawer"
              onClick={toggleDrawer}
              sx={{
                marginRight: '36px',
                ...(open && { display: 'none' }),
              }}
            >
              <MenuIcon />
            </IconButton>
            <IconButton color="primary">
            <Header/>
            </IconButton>
          </Toolbar>
        </AppBar>
        <Drawer variant="permanent" open={open}>
          <Toolbar
            sx={{
              display: 'flex',
              alignItems: 'center',
              justifyContent: 'flex-end',
              px: [1],
            }}
          >
            <IconButton onClick={toggleDrawer}>
              <ChevronLeftIcon />
            </IconButton>
          </Toolbar>
          <Divider />
          <Box sx={{ p: 2 }}>
            {/* Button with icon */}
            <Button
              variant="outlined"
              fullWidth
              sx={{ height: '60px' }} // Adjust the height as needed
              startIcon={<SupervisorAccountIcon/>}
              onClick={() => {
                window.scrollTo({ top: 0, left: 0, behavior: "smooth" });
                navigate(PATH_DASHBOARD.manageUsers);
              }}
            >
              USER MANAGEMENT
            </Button>
          </Box>
          <Box sx={{ p: 2 }}>
            {/* Button with icon */}
            <Button
              variant="outlined"
              fullWidth
              sx={{ height: '60px' }} // Adjust the height as needed
              startIcon={<InboxIcon />}
              onClick={() => {
                window.scrollTo({ top: 0, left: 0, behavior: "smooth" });
                navigate(PATH_DASHBOARD.manageMessage);
              }}
            >
              Manage Messages
            </Button>
          </Box>
          <Box sx={{ p: 2 }}>
            {/* Button with icon */}
            <Button
              variant="outlined"
              fullWidth
              sx={{ height: '60px' }} // Adjust the height as needed
              startIcon={<FamilyRestroomIcon />}
              onClick={() => {
                window.scrollTo({ top: 0, left: 0, behavior: "smooth" });
                navigate(PATH_DASHBOARD.manageLeaves);
              }}
            >
            Leave Management
            </Button>
          </Box>
          <Box sx={{ p: 2 }}>
            {/* Button with icon */}
            <Button
              variant="outlined"
              fullWidth
              sx={{ height: '60px' }} // Adjust the height as needed
              startIcon={<ApartmentIcon />}
              onClick={() => {
                window.scrollTo({ top: 0, left: 0, behavior: "smooth" });
                navigate(PATH_DASHBOARD.genericManagement);
              }}
            >
            Generic Managament
            </Button>
          </Box>
          <Box sx={{ p: 2 }}>
            {/* Button with icon */}
            <Button
              variant="outlined"
              fullWidth
              sx={{ height: '60px' }} // Adjust the height as needed
              startIcon={<CalendarMonthIcon />}
              onClick={() => {
                window.scrollTo({ top: 0, left: 0, behavior: "smooth" });
                navigate(PATH_DASHBOARD.manageTimesheets);
              }}
            >
            Timesheet Managament
            </Button>
          </Box>
        </Drawer>
      </Box>
    </ThemeProvider>
  );
}