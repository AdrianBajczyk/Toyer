import { RouterProvider, createBrowserRouter } from "react-router-dom";
import LoginPage from "../Pages/LoginPage/LoginPage.tsx";
import HomePage from "../Pages/HomePage/HomePage.tsx";
import DevicePage from "../Pages/DevicePage/DevicePage.tsx";
import UserContextProvider from "../Store/user-context.tsx";
import MainNavigation from "../Components/MainNavigation/MainNavigation.tsx";
import RootLayout from "../Pages/RootLayout/RootLayout.tsx";
import ErrorPage from "../Pages/ErrorPage/ErrorPage.tsx";

const router = createBrowserRouter([
  {
    path: "/",
    errorElement: <ErrorPage message="Page not found"/>,
    element: <RootLayout />,
    children: [
      { path: "/", element: <HomePage /> },
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
