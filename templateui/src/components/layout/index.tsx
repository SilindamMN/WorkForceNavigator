import useAuth from '../../hooks/useAuth.hook';
import { Outlet, useLocation } from 'react-router-dom';
import Header from './header';
import NavSideBar from '../../pages/dashboard/NavSideBar';

const Layout = () => {
    const {isAuthenticated}=useAuth();
    const {pathname} = useLocation();

    
    const sideBarRender=()=>{
        if(isAuthenticated && pathname.toLocaleLowerCase().startsWith('/dashboard')){
            return <NavSideBar/>
        }
        return null;
    }
  return (
    <div>
        <div>
            <div className='flex'>
                {sideBarRender()}
                <Outlet/>
            </div>
        </div>
    </div>
  )
}

export default Layout