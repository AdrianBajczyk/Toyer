import { useRouteError, isRouteErrorResponse } from "react-router";
import MainNavigation from "../../Components/MainNavigation/MainNavigation.jsx";
import Typewriter from "../../Utils/typewriter.jsx";

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
          <Typewriter textArray={textArray} speed={speed} />
        </main>
      </>
    );
  }

  return <></>;
}

export default ErrorPage;
