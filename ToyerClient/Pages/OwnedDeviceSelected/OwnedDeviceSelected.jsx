import { useParams } from "react-router-dom";
import classes from "./OwnedDeviceSelected.module.css";
import { useState, useEffect, useRef } from "react";
import useAxiosPrivate from "../../Hooks/useAxiosPrivate";
import { useErrorBoundary } from "react-error-boundary";
import Spinner from "../../Components/Spinner/Spinner";

const OwnedDeviceSelected = () => {
  const { id } = useParams();

  const [device, setDevice] = useState([]);
  const [isLoading, setIsLoading] = useState(false);

  const { showBoundary } = useErrorBoundary();

  const axiosPrivate = useAxiosPrivate();
  const abortControllerRef = useRef();

  useEffect(() => {
    abortControllerRef.current = new AbortController();
    setIsLoading(true);

    const getDevices = async () => {
      try {
        const response = await axiosPrivate.get(
          `Device/${id}`,
          {},
          {
            headers: {
              "Content-Type": "application/json",
            },
            signal: abortControllerRef.current?.signal,
            withCredentials: true,
          }
        );
        setDevice(response.data);
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

  return isLoading ? (
    <Spinner width={600} height={600} />
  ) : device ? (
    <section className={classes.selectedDevicePageContainer}>
      <div className={classes.deviceInfoContainer}>
        <span>
          <h4>Id:</h4>
          <p>{device.id}</p>
        </span>
        <span>
          <h4>Name:</h4>
          <p>{device.name}</p>
        </span>
        <span>
          <h4>Model:</h4>
          <p>{device.deviceTypeDto?.name}</p>
        </span>
        <span>
          <h4>Manufactured in:</h4>
          <p>{device.dateOfCreation}</p>
        </span>
        <span>
          <h4>Registered in:</h4>
          <p>{device.dateOfLastRegistration}</p>
        </span>
      </div>
      <div className={classes.deviceDescriptionContainer}>
        <span>{device.deviceTypeDto?.description}</span>
      </div>
      <div className={classes.ordersContainer}>
        <h3>Commands:</h3>
        <ul>
          {console.log(device.deviceTypeDto?.orders)}
          {device.deviceTypeDto?.orders.map((order) => (
            <li key={`order-${order.id}`}>
              <span>{order.description}</span>
              <button>{order.name}</button>
            </li>
          ))}
        </ul>
      </div>
    </section>
  ) : (
    <></>
  );
};

export default OwnedDeviceSelected;
