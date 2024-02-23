
import OwnedDevicesNavigation from "../../Navigations/OwnedDevicesNavigation/OwnedDevicesNavigation.jsx";
import { Outlet } from "react-router-dom";

function OwnedDevicesLayout() {
  return (
    <>
      <OwnedDevicesNavigation />
        <Outlet />
    </>
  );
}

export default OwnedDevicesLayout;
