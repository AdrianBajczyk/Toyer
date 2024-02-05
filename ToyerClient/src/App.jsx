import { createBrowserRouter, RouterProvider } from "react-router-dom";
import ErrorPage from "../Pages/ErrorPage/ErrorPage.jsx";
import RootLayout from "../Components/Layouts/RootLayout/RootLayout.jsx";
import HomePage from "../Pages/HomePage/HomePage.jsx";
import LoginPage from "../Pages/LoginPage/LoginPage.jsx";
import RegisterPage, {
  action as registerAction,
} from "../Pages/RegisterPage/RegisterPage.jsx";
import DevicesRootLayout from "../Components/Layouts/DevicesRoot.jsx";
import DevicesPage, {
  loader as devicesLoader,
} from "../Pages/DevicesPage/DevicesPage.jsx";
import DeviceDetails, {
  loader as deviceLoader,
} from "../Pages/DeviceDetails/DeviceDetails.jsx";
import RegisterSuccess from "../Pages/RegisterSuccess/RegisterSuccess.jsx";
import User from "../Pages/UserPage/User.jsx";
import RequireAuth from "../Components/ReguireAuth.jsx";
import Unauthorized from "../Pages/UnauthorizedPage/Unauthorized.jsx";

function App() {
  const router = createBrowserRouter([
    {
      path: "/",
      errorElement: <ErrorPage />,
      element: <RootLayout />,
      children: [
        //PUBLIC ROUTES
        { index: true, element: <HomePage /> },
        { path: "login", element: <LoginPage /> },
        { path: "register", element: <RegisterPage />, action: registerAction },
        { path: "register/success", element: <RegisterSuccess /> },
        { path: "unauthorized", element: <Unauthorized /> },

        //   { path: "/profile", element: <Profile /> },
        {
          path: "devices",
          element: <DevicesRootLayout />,
          children: [
            { index: true, element: <DevicesPage />, loader: devicesLoader },
            { path: ":id", element: <DeviceDetails />, loader: deviceLoader },
          ],
        },

        // PROTECTED ROUTES
        {
          element: <RequireAuth allowedRoles={["Employee"]} />,
          children: [{ path: "user", element: <User /> }],
        },
      ],
    },
  ]);

  return <RouterProvider router={router} />;
}

export default App;
