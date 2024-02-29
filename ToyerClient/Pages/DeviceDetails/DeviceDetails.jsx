import { useNavigate } from "react-router-dom";
import { get } from "../../Utils/http.js";
import { useEffect, useRef, useState } from "react";
import Spinner from "../../Components/Spinner/Spinner.jsx";
import classes from "./DeviceDetails.module.css";

export default function DeviceDetials({ id }) {
  const abortControllerRef = useRef(null);

  const [deviceDetails, setDeviceDetails] = useState();
  const [isLoading, setIsLoding] = useState(null);
  const [err, setErr] = useState(null);

  useEffect(() => {
    abortControllerRef.current = new AbortController();
    setIsLoding(true);

    const getUsers = async () => {
      try {
        const response = await get(
          `/DeviceType/${id}`,
          abortControllerRef.current?.signal
        );
        setDeviceDetails(response);
      } catch (error) {
        console.log("device details error:");
        console.log(error);
        setErr(error);
      } finally {
        setIsLoding(false);
      }
    };

    getUsers();

    return () => {
      abortControllerRef.current.abort();
    };
  }, []);


  return (
    <>
      {err ? (
        <div>{err.statusText} Try again later...</div>
      ) : isLoading ? (
        <Spinner height={"400"} width={"400"} />
      ) : (
        deviceDetails && (
          <div className={classes.blurredText}>
            <h2>{deviceDetails.name}</h2>
            <p>{deviceDetails.description}</p>
            <h3>Available commands:</h3>
            <table>
              <thead>
                <tr>
                  <th>Name</th>
                  <th>Description</th>
                </tr>
              </thead>
              <tbody>
                {deviceDetails.orders.map((order) => (
                  <tr key={`order-${order.id}`}>
                    <td>{order.name}</td>
                    <td>{order.description}</td>
                  </tr>
                ))}
              </tbody>
            </table>
          </div>
        )
      )}
    </>
  );
}
