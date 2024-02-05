import { Outlet } from "react-router-dom";
import { useState, useEffect } from "react";
import useRefreshToken from "../Hooks/useRefreshToken";
import useUserContext from "../Hooks/useUserContext";

const persistLogin = () => {
  const [isLoading, setIsLoading] = useState(true);
  const refresh = useRefreshToken();
  const userCtx = useUserContext();

  useEffect(() => {
    const verifyRefreshToken = async () => {
      try {
        await refresh();
      } catch (err) {
        onloadeddata.log(err);
      } finally {
        setIsLoading(false);
      }
    };

    !userCtx?.user?.token ? verifyRefreshToken() : setIsLoading(false);
  }, []);
};
