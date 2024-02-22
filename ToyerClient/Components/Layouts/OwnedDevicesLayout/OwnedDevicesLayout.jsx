import { ErrorBoundary } from "react-error-boundary";
import OwnedDevicesNavigation from "../../Navigations/OwnedDevicesNavigation/OwnedDevicesNavigation.jsx";
import { Outlet } from "react-router-dom";
import { ErrorBoundraiesPage } from "../../../Pages/ErrorPage/ErrorBoundariesPage/ErrorBoundraiesPage.jsx";

function OwnedDevicesLayout() {
  return (
    <>
      <OwnedDevicesNavigation />
      <ErrorBoundary FallbackComponent={ErrorBoundraiesPage}>
        <Outlet />
      </ErrorBoundary>
    </>
  );
}

export default OwnedDevicesLayout;
