import { Outlet } from "react-router";
import MainNavigation from "../../Components/MainNavigation/MainNavigation";
import classes from "./RootLayout.module.css"

function RootLayout() {
  return (
    <>
      <MainNavigation />
      <main className={classes.content}>
      <Outlet />
      </main>
    </>
  );
}

export default RootLayout;
