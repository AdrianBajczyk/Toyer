import { useLocation, Navigate, Outlet } from "react-router-dom";
import useUserContext from "../Hooks/useUserContext";

const RequireAuth = ({ allowedRoles }) => {
  const location = useLocation();
  const userCtx = useUserContext();

  return userCtx?.user?.roles?.find((role) => allowedRoles?.includes(role)) ? (
    <Outlet />
  ) : (
    <Navigate to="/unauthorized" state={{ from: location }} replace />
  );
};

export default RequireAuth;
