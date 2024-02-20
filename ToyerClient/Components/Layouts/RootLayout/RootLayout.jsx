import { Outlet, useNavigation } from "react-router-dom";
import MainNavigation from "../../MainNavigation/MainNavigation";
import Spinner from "../../Spinner/Spinner";

function RootLayout() {

  const navigation = useNavigation();

  const isLoading = navigation.state === "loading" ? true : false;

  return (
    <>
      <MainNavigation />
      <main>
        {isLoading ? <Spinner height={'600'} width={'600'}/> : <Outlet />}
        
      </main>
    </>
  );
}

export default RootLayout;
