import { createBrowserRouter, RouterProvider } from 'react-router-dom';
// import UserContextProvider from '../Store/user-context.jsx';
import ErrorPage from "../Pages/ErrorPage/ErrorPage.jsx";
import RootLayout from "../Pages/RootLayout/RootLayout.jsx";
import HomePage from "../Pages/HomePage/HomePage.jsx";
import LoginPage from "../Pages/LoginPage/LoginPage.jsx";
import RegisterPage from "../Pages/RegisterPage/RegisterPage.jsx";
import DevicesRootLayout from "../Pages/DevicesRoot/DevicesRoot.jsx";
import DevicesPage, {loader as devicesLoader} from "../Pages/DevicesPage/DevicesPage.jsx";
import DeviceDetails, {loader as deviceLoader} from "../Pages/DeviceDetails/DeviceDetails.jsx";

const router = createBrowserRouter([
  {
    path: '/',
    errorElement: <ErrorPage />,
    element: <RootLayout />,
    children: [
      { index:true, element: <HomePage /> },
      { path: 'login', element: <LoginPage /> },
      { path: "/register", element: <RegisterPage /> },
    //   { path: "/profile", element: <Profile /> },
      {path: 'devices', element: <DevicesRootLayout/>, children:[
        { index:true, element: <DevicesPage />, loader:devicesLoader },
        { path: ':id', element: <DeviceDetails />, loader:deviceLoader},
      ]}

    ],
  },
]);

function App() {
  return (
    
      <RouterProvider router={router} />

  );
}

export default App;
