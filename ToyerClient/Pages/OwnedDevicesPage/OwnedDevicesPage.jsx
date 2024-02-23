import classes from "./OwnedDevicesPage.module.css";
import { useState, useEffect, useRef } from "react";
import useUserContext from "../../Hooks/useUserContext";
import Spinner from "../../Components/Spinner/Spinner";
import useAxiosPrivate from "../../Hooks/useAxiosPrivate";
import { useErrorBoundary } from "react-error-boundary";
import Typewriter from "../../Utils/typewriter";
import { NavLink } from "react-router-dom";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faSquareCheck, faSquare } from "@fortawesome/free-solid-svg-icons";

const textArray = ["You have no assigned devices yet", "If you have one ADD"];

const OwnedDevicesPage = () => {
  const [devices, setDevices] = useState([]);
  const [isLoading, setIsLoading] = useState(false);
  const [iconSelection, setIconSelection] = useState({});

  const { showBoundary } = useErrorBoundary();

  const userCtx = useUserContext();
  const axiosPrivate = useAxiosPrivate();
  const abortControllerRef = useRef();

  useEffect(() => {
    abortControllerRef.current = new AbortController();
    setIsLoading(true);

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
        console.log(response.data);
      } catch (error) {
        showBoundary(error);
      } finally {
        setIsLoading(false);
      }
    };

    getDevices();

    return () => {
      abortControllerRef.current.abort();
    };
  }, []);

  // Function to handle icon selection for a specific device
  const handleIconSelection = (deviceId, icon) => {
    setIconSelection((prevSelections) => ({
      ...prevSelections,
      [deviceId]: icon,
    }));
  };

  return isLoading ? (
    <Spinner height={"600"} width={"600"} />
  ) : devices.length ? (
    <div className={classes.devicesContainer}>
      <table>
        <thead>
          <tr>
            <th>Device name</th>
            <th>Id</th>
            <th>Select</th>
          </tr>
        </thead>
        <tbody>
          {devices.map((device) => {
            return (
              <tr key={`user-${device.id}`}>
                <td>{device.name}</td>
                <td>{device.id}</td>
                <td>
                  <NavLink
                    id={`selectDevice-${device.id}`}
                    to={device.id}
                    onMouseEnter={() =>
                      handleIconSelection(device.id, faSquareCheck)
                    }
                    onMouseLeave={() =>
                      handleIconSelection(device.id, faSquare)
                    }
                  >
                    <FontAwesomeIcon
                      icon={iconSelection[device.id] || faSquare}
                    />
                  </NavLink>
                </td>
              </tr>
            );
          })}
        </tbody>
      </table>
    </div>
  ) : (
    <div className={classes.devicesContainer}>
      <Typewriter textArray={textArray} speed={60} />
    </div>
  );
};

export default OwnedDevicesPage;
