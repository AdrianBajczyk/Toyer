import classes from "./OwnedDevicesPage.module.css";
import { useState, useEffect, useRef } from "react";
import useUserContext from "../../Hooks/useUserContext";
import Spinner from "../../Components/Spinner/Spinner";
import useAxiosPrivate from "../../Hooks/useAxiosPrivate";
import { useErrorBoundary } from "react-error-boundary";

const OwnedDevicesPage = () => {
  const [devices, setDevices] = useState([]);
  const [isLoading, setIsLoding] = useState(false);

  const { showBoundary } = useErrorBoundary();

  const userCtx = useUserContext();
  const axiosPrivate = useAxiosPrivate();
  const abortControllerRef = useRef();

  useEffect(() => {
    abortControllerRef.current = new AbortController();
    setIsLoding(true);

    const getDevices = async () => {
      try {
        const response = await axiosPrivate.get(
          `DeviceAssign/${userCtx.user.id}`,
          {},
          {
            headers: {
              "Content-Type": "application/json",
            },
            signal: abortControllerRef.current?.signal,
            withCredentials: true,
          }
        );
        setDevices(response.data);
        // console.log(response.data);
      } catch (error) {
        showBoundary(error);
      } finally {
        setIsLoding(false);
      }
    };

    getDevices();

    return () => {
      abortControllerRef.current.abort();
    };
  }, []);

  return isLoading ? (
    <Spinner height={"600"} width={"600"} />
  ) : (
    <div className={classes.devicesPageContainer}>OwnedDevicesPage</div>
  );
};

export default OwnedDevicesPage;
