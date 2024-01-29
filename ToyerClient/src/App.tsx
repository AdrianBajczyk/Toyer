import { RouterProvider, createBrowserRouter } from "react-router-dom";
import LoginPage from "../Pages/LoginPage/LoginPage.tsx";
import HomePage from "../Pages/HomePage/HomePage.tsx";
import DevicesPage, {loader as devicesLoader } from "../Pages/DevicesPage/DevicesPage.tsx";
import UserContextProvider from "../Store/user-context.tsx";
import RootLayout from "../Pages/RootLayout/RootLayout.tsx";
import ErrorPage from "../Pages/ErrorPage/ErrorPage.tsx";
import DeviceDetails, {loader as deviceLoader} from "../Pages/DeviceDetails/DeviceDetails.tsx";
import DevicesRootLayout from "../Pages/DevicesRoot/DevicesRoot.tsx";


const router = createBrowserRouter([
  {
    path: '/',
    errorElement: <ErrorPage />,
    element: <RootLayout />,
    children: [
      { index:true, element: <HomePage /> },
      { path: 'login', element: <LoginPage /> },
      // { path: "/register", element: <ResgisterPage /> },
      // { path: "/profile", element: <Profile /> },
      {path: 'devices', element: <DevicesRootLayout/>, children:[
        { index:true, element: <DevicesPage />, loader:devicesLoader },
        { path: ':id', element: <DeviceDetails />, loader:deviceLoader},
      ]}
      
    ],
  },
]);

function App() {
  return (
    <UserContextProvider>
      <RouterProvider router={router} />
    </UserContextProvider>
  );
}
export default App;
