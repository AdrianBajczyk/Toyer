import { createBrowserRouter } from "react-router-dom";
import ErrorElementPage from "../Pages/ErrorPage/ErrorElementPage/ErrorElementPage.jsx";
import RootLayout from "../Components/Layouts/RootLayout/RootLayout.jsx";
import HomePage from "../Pages/HomePage/HomePage.jsx";
import RegisterPage, {
  action as registerAction,
} from "../Pages/RegisterPage/RegisterPage.jsx";
import DevicesRootLayout from "../Components/Layouts/DevicesRoot/DevicesRoot.jsx";
import DevicesPage, {
  loader as devicesLoader,
} from "../Pages/DevicesPage/DevicesPage.jsx";
import RegisterSuccess from "../Pages/RegisterSuccess/RegisterSuccess.jsx";
import Users from "../Pages/UsersPage/Users.jsx";
import RequireAuth from "../Components/ReguireAuth.jsx";
import Unauthorized from "../Pages/UnauthorizedPage/Unauthorized.jsx";
import Spinner from "../Components/Spinner/Spinner.jsx";
import ProfilePage from "../Pages/ProfilePage/ProfilePage/ProfilePage.jsx";
import PersonalDataPage from "../Pages/ProfilePage/PersonalDataPage/PersonalDataPage.jsx";
import AddressDataPage from "../Pages/ProfilePage/AddressDataPage/AddressDataPage.jsx";
import EmailConfirmed from "../Pages/EmailConfirmed/EmailConfirmed.jsx";
import DeleteAccountPage from "../Pages/ProfilePage/DeleteAccountPage/DeleteAccountPage.jsx";
import OwnedDevicesNavigation from "../Components/Layouts/OwnedDevicesLayout/OwnedDevicesLayout.jsx";
import AssignDeviceWindow from "../Components/Windows/AssignDeviceWindow/AssignDeviceWindow.jsx";
import OwnedDevicesPage from "../Pages/OwnedDevicesPage/OwnedDevicesPage.jsx";

export const router = createBrowserRouter([
  {
    path: "",
    errorElement: <ErrorElementPage />,
    element: <RootLayout />,
    children: [
      //PUBLIC ROUTES
      { index: true, element: <HomePage /> },
      { path: "register", element: <RegisterPage />, action: registerAction },
      { path: "register/success", element: <RegisterSuccess /> },
      { path: "unauthorized", element: <Unauthorized /> },
      { path: "spinner", element: <Spinner /> },
      { path: "email/confirm", element: <EmailConfirmed /> },
      {
        path: "devices",
        element: <DevicesRootLayout />,
        children: [
          {
            path: "",
            element: <DevicesPage />,
            loader: devicesLoader,
          },
        ],
      },
      // PROTECTED ROUTES
      {
        element: (
          <RequireAuth allowedRoles={import.meta.env.VITE_EMPLOYEE_ROLE} />
        ),
        children: [{ path: "user", element: <Users /> }],
      },
      {
        element: <RequireAuth allowedRoles={import.meta.env.VITE_USER_ROLE} />,
        children: [
          { path: "profile", element: <ProfilePage /> },
          { path: "profile/personals", element: <PersonalDataPage /> },
          { path: "profile/address", element: <AddressDataPage /> },
          { path: "profile/delete", element: <DeleteAccountPage /> },
          {
            path: "ownedDevices",
            element: <OwnedDevicesNavigation />,
            children: [
              { path: "assigment", element: <AssignDeviceWindow /> },
              { path: "", element: <OwnedDevicesPage /> },
            ],
          },
        ],
      },
    ],
  },
]);
