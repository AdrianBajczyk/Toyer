import { useRouteError, isRouteErrorResponse } from "react-router";
import MainNavigation from "../../../Components/Navigations/MainNavigation/MainNavigation.jsx";
import Typewriter from "../../../Utils/typewriter.jsx";
import classes from "./ErrorElementPage.module.css";

const speed = 70;

function ErrorElementPage() {
  const error = useRouteError();
  console.log(error)

  if (isRouteErrorResponse(error)) {
    const textArray = [
      error.status.toString(),
      error.statusText,
      error.data.toString(),
    ];

    
    return (
      <>
        <MainNavigation />
        <main>
          <div className={classes.errorTextContainer}>
            <Typewriter textArray={textArray} speed={speed} />
          </div>
        </main>
      </>
    );
  }

  return (
    <>
      <MainNavigation />
      <main>
        <div className={classes.errorTextContainer}>
          <Typewriter textArray={["Unexpected error, please contact support."]} speed={speed} />
        </div>
      </main>
    </>
  );
}

export default ErrorElementPage;
