import { RouterProvider, createBrowserRouter } from "react-router-dom";
import LoginPage from "../Pages/LoginPage/LoginPage.tsx";
import HomePage from "../Pages/HomePage/HomePage.tsx";
import DevicesPage, {loader as devicesLoader } from "../Pages/DevicesPage/DevicesPage.tsx";
import UserContextProvider from "../Store/user-context.tsx";
import RootLayout from "../Pages/RootLayout/RootLayout.tsx";
import ErrorPage from "../Pages/ErrorPage/ErrorPage.tsx";
import DeviceDetails from "../Pages/DeviceDetails/DeviceDetails.tsx";
import { ApiUrlProvider } from "../Store/api-url-context.tsx"


const router = createBrowserRouter([
  {
    path: "/",
    errorElement: <ErrorPage message="Page not found" />,
    element: <RootLayout />,
    children: [
      { path: "", element: <HomePage /> },
      { path: "/login", element: <LoginPage /> },
      // { path: "/register", element: <ResgisterPage /> },
      // { path: "/profile", element: <Profile /> },
      { path: "/devices", element: <DevicesPage />, loader:devicesLoader},
      { path: "/devices/:id", element: <DeviceDetails />},
      
    ],
  },
]);

function App() {
  return (
    <ApiUrlProvider>
    <UserContextProvider>
      <RouterProvider router={router} />
    </UserContextProvider>
    </ApiUrlProvider>
  );
}
export default App;
