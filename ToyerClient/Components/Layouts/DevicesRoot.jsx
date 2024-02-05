import DevicesNavigation from "../DevicesNavigation/DevicesNavigation.jsx";
import { Outlet } from "react-router-dom";

function DevicesRootLayout() {
  return (
    <>
      <DevicesNavigation />
      <Outlet />
    </>
  );
}

export default DevicesRootLayout;
