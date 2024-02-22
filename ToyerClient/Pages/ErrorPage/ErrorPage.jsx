import { useRouteError, isRouteErrorResponse } from "react-router";
import MainNavigation from "../../Components/Navigations/MainNavigation/MainNavigation.jsx";
import Typewriter from "../../Utils/typewriter.jsx";
import classes from './ErrorPage.module.css'

function ErrorPage() {
  const error = useRouteError();

  if (isRouteErrorResponse(error)) {
    const textArray = [
      error.status.toString(),
      error.statusText,
      error.data.toString(),
    ];

    const speed = 70;

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

  return <></>;
}

export default ErrorPage;
