import { axiosPrivate } from "../src/Api/axios";
import { useEffect } from "react";
import useRefreshToken from "./useRefreshToken";
import useUserContext from "./useUserContext";

const useAxiosPrivate = () => {
  const refresh = useRefreshToken();
  const userCtx = useUserContext();

  useEffect(() => {
    const requestIntercept = axiosPrivate.interceptors.request.use(
      (config) => {
        console.log("inreqest");
        if (!config.headers["Authorization"]) {
          console.log("firstRequest");
          console.log("user" + userCtx.user.token);
          config.headers["Authorization"] = `Bearer ${userCtx.user.token}`;
        }
        return config;
      },
      (error) => {
        console.log(error);
        Promise.reject(error);
      }
    );

    const responseIntercept = axiosPrivate.interceptors.response.use(
      (response) => {
        console.log("inResponse");
        return response;
      },
      async (error) => {
        const prevRequest = error?.config;
        if (error?.response?.status === 401 && !prevRequest?.sent) {
          console.log("inRetry");
          prevRequest.sent = true;
          const newAccessToken = await refresh();
          prevRequest.headers["Authorization"] = `Bearer ${newAccessToken}`;
          console.log(prevRequest);
          return axiosPrivate(prevRequest);
        }
        console.log(error);
        return Promise.reject(error);
      }
    );

    return () => {
      axiosPrivate.interceptors.request.eject(requestIntercept);
      axiosPrivate.interceptors.response.eject(responseIntercept);
    };
  }, [refresh, userCtx]);

  return axiosPrivate;
};

export default useAxiosPrivate;
