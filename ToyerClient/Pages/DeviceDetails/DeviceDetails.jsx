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

    console.log(abortControllerRef.current?.signal);

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

  useEffect(() => {}, []);

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
            <h3>Awailable commands:</h3>
            <table>
              <tr>
                <th>Name</th>
                <th>Description</th>
              </tr>
                {deviceDetails.orders.map((order) => {
                  return <tr key={`order-${order.id}`}><td>{order.name}</td><td>{order.description}</td></tr>;
                })}
            </table>

          </div>
        )
      )}
    </>
  );
}
