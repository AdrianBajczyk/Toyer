import { createBrowserRouter, RouterProvider } from "react-router-dom";
import ErrorPage from "../Pages/ErrorPage/ErrorPage.jsx";
import RootLayout from "../Components/Layouts/RootLayout/RootLayout.jsx";
import HomePage from "../Pages/HomePage/HomePage.jsx";
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
import Spinner from "../Components/Spinner/Spinner.jsx";
import ProfilePage from "../Pages/ProfilePage/ProfilePage/ProfilePage.jsx";
import PersonalDataPage from "../Pages/ProfilePage/PersonalDataPage/PersonalDataPage.jsx";
import AddressDataPage from "../Pages/ProfilePage/AddressDataPage/AddressDataPage.jsx";
import EmailConfirmed from "../Pages/EmailConfirmed/EmailConfirmed.jsx";

function App() {
  const router = createBrowserRouter([
    {
      path: "/",
      errorElement: <ErrorPage />,
      element: <RootLayout />,
      children: [
        //PUBLIC ROUTES
        { index: true, element: <HomePage /> },
        { path: "register", element: <RegisterPage />, action: registerAction },
        { path: "register/success", element: <RegisterSuccess /> },
        { path: "unauthorized", element: <Unauthorized /> },
        { path: "spinner", element: <Spinner /> },
        { path: "email/confirm", element: <EmailConfirmed/>},

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
          element: <RequireAuth allowedRoles={import.meta.env.VITE_EMPLOYEE_ROLE} />,
          children: [{ path: "user", element: <User /> }],
        },
        {
          element: (
            <RequireAuth
              allowedRoles={import.meta.env.VITE_USER_ROLE}
            />
          ),
          children: [
            { path: "profile", element: <ProfilePage /> },
            { path: "profile/personals", element: <PersonalDataPage /> },
            { path: "profile/address", element: <AddressDataPage /> },
          ],
        },
      ],
    },
  ]);

  return <RouterProvider router={router}></RouterProvider>;
}

export default App;
