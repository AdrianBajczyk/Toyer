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
  const [isSendingOrder, setIsSendingOrder] = useState(false);

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

  const handleSendOrder = async (orderId) => {
    const sendOrder = async () => {
      setIsSendingOrder(true);
      try {
        const response = await axiosPrivate.post(
          `Device/${device.id}/command/${orderId}`,
          {},
          {
            headers: {
              "Content-Type": "application/json",
            },
            withCredentials: true,
          }
        );
        console.log(response);
      } catch (error) {
        showBoundary(error);
      } finally {
        setIsSendingOrder(false);
      }
    };

    sendOrder();
  };

  return isLoading ? (
    <Spinner width={600} height={600} />
  ) : device ? (
    <section className={classes.selectedDevicePageContainer}>
      <div className={classes.deviceInfoContainer}>
      <h3>Info:</h3>
        <table>
          <tbody>
            <tr>
              <th>Id:</th>
              <td>{device.id}</td>
            </tr>
            <tr>
              <th>Name:</th>
              <td>{device.name}</td>
            </tr>
            <tr>
              <th>Model:</th>
              <td>{device.deviceTypeDto?.name}</td>
            </tr>
            <tr>
              <th>Manufactured in:</th>
              <td>{device.dateOfCreation}</td>
            </tr>
            <tr>
              <th>Registered in:</th>
              <td>{device.dateOfLastRegistration}</td>
            </tr>
          </tbody>
        </table>
      </div>
      <div className={classes.deviceDescriptionContainer}>
        <span>{device.deviceTypeDto?.description}</span>
      </div>
      <div className={classes.ordersContainer}>
        <h3>Commands:</h3>
        <table>
          <thead>
            <tr>
              <th>Description</th>
              <th>Action</th>
            </tr>
          </thead>
          <tbody>
            {device.deviceTypeDto?.orders.map((order) => (
              <tr key={`order-${order.id}`}>
                <td>{order.description}</td>
                <td>
                  <button onClick={() => handleSendOrder(order.id)}>
                    {order.name}
                  </button>
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>
    </section>
  ) : (
    <></>
  );
};

export default OwnedDeviceSelected;
