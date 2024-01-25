import { RouterProvider, createBrowserRouter } from "react-router-dom";
import LoginPage from "../Pages/LoginPage/LoginPage.tsx";
import HomePage from "../Pages/HomePage/HomePage.tsx";
import DevicesPage from "../Pages/DevicesPage/DevicesPage.tsx";
import UserContextProvider from "../Store/user-context.tsx";
import RootLayout from "../Pages/RootLayout/RootLayout.tsx";
import ErrorPage from "../Pages/ErrorPage/ErrorPage.tsx";
import DeviceDetails from "../Pages/DeviceDetails/DeviceDetails.tsx"

const router = createBrowserRouter([
  {
    path: "/",
    errorElement: <ErrorPage message="Page not found" />,
    element: <RootLayout />,
    children: [
      { path: "/", element: <HomePage /> },
      { path: "/devices", element: <DevicesPage />},
      { path: "/devices/:id", element: <DeviceDetails />},
      { path: "/login", element: <LoginPage /> },
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
