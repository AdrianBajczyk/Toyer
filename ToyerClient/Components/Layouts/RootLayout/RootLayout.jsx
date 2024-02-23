import { Outlet, useNavigation } from "react-router-dom";
import MainNavigation from "../../Navigations/MainNavigation/MainNavigation";
import Spinner from "../../Spinner/Spinner";
import { ErrorBoundary } from "react-error-boundary";
import { ErrorBoundariesPage } from "../../../Pages/ErrorPage/ErrorBoundariesPage/ErrorBoundariesPage.jsx";

function RootLayout() {
  const navigation = useNavigation();

  const isLoading = navigation.state === "loading" ? true : false;

  return (
    <>
      <MainNavigation />
      <main>
        <ErrorBoundary FallbackComponent={ErrorBoundariesPage}>
          {isLoading ? <Spinner height={"600"} width={"600"} /> : <Outlet />}
        </ErrorBoundary>
      </main>
    </>
  );
}

export default RootLayout;
