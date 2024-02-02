import { createBrowserRouter, RouterProvider } from 'react-router-dom';
import ErrorPage from "../Pages/ErrorPage/ErrorPage.jsx";
import RootLayout from "../Pages/RootLayout/RootLayout.jsx";
import HomePage from "../Pages/HomePage/HomePage.jsx";
import LoginPage, {action as loginAction} from "../Pages/LoginPage/LoginPage.jsx";
import RegisterPage, {action as registerAction} from "../Pages/RegisterPage/RegisterPage.jsx";
import DevicesRootLayout from "../Pages/DevicesRoot/DevicesRoot.jsx";
import DevicesPage, {loader as devicesLoader} from "../Pages/DevicesPage/DevicesPage.jsx";
import DeviceDetails, {loader as deviceLoader} from "../Pages/DeviceDetails/DeviceDetails.jsx";
import RegisterSuccess from '../Pages/RegisterSuccess/RegisterSuccess.jsx';
import  useUserContext  from '../Hooks/useUserContext.js';
import User, {loader as usersLoader} from '../Pages/UserPage/User.jsx';

function App() {

  const userCtx = useUserContext();

  const router = createBrowserRouter([
    {
      path: '/',
      errorElement: <ErrorPage />,
      element: <RootLayout />,
      children: [
        { index:true, element: <HomePage /> },
        { path: 'login', element: <LoginPage />, action:loginAction(userCtx) },
        { path: "register", element: <RegisterPage />, action:registerAction },
        { path: 'register/success', element:<RegisterSuccess/>},
        { path:'user', element:<User/>, loader:usersLoader },
        
      //   { path: "/profile", element: <Profile /> },
        {path: 'devices', element: <DevicesRootLayout/>, children:[
          { index:true, element: <DevicesPage />, loader:devicesLoader },
          { path: ':id', element: <DeviceDetails />, loader:deviceLoader},
        ]}
  
      ],
    },
  ]);

  return (
      <RouterProvider router={router} />
  );
}

export default App;
