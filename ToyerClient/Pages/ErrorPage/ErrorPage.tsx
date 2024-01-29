import { useRouteError, isRouteErrorResponse } from "react-router";
import MainNavigation from "../../Components/MainNavigation/MainNavigation.tsx";

function ErrorPage() {
  const error = useRouteError() as Response;

  if (isRouteErrorResponse(error)) {
    return (
      <>
        <MainNavigation />
        <main>
          <h1>{error.status}</h1>
          <h2>{error.statusText}</h2> 
          <h3>{error.data}</h3>
        </main>
      </>
    );
  }

  return <></>;
}

export default ErrorPage;
