import { useNavigate } from "react-router-dom";
import Button from "../../Components/UI/Button.jsx";
import { get } from "../../Utils/http.js";
import { useEffect, useRef, useState } from "react";

export default function DeviceDetials({ id }) {
  const navigate = useNavigate();

  const abortControllerRef = useRef(null);

  const [deviceDetails, setDeviceDetails] = useState();
  const [isLoading, setIsLoding] = useState();

  useEffect(() => {
    let isMounted = true;
    // abortControllerRef.current?.abort();
    abortControllerRef.current = new AbortController();
    abortControllerRef.current?.abort();
    console.log(abortControllerRef.current.signal)

    const getUsers = async () => {
      setIsLoding(true);

      try {
        const response = await get(`/DeviceType/${id}`, {
          signal: abortControllerRef.current?.signal,
        });
        isMounted && setDeviceDetails(response);
        console.log(response);
      } catch (err) {
        console.log(err);
      } finally {
        setIsLoding(false);
      }
    };

    getUsers();

    return () => {
      isMounted = false;
    };
  }, []);

  useEffect(() => {}, []);

  return (
    <>
      {isLoading ? (
        <>Loading...</>
      ) : deviceDetails ? (
        <div>
          <p>{deviceDetails.orders[0].description}</p>
          <p>DUPA</p>
          <p>DUPA</p>
          <Button
            element="button"
            onClick={() => navigate("..", { relative: "path" })}
          >
            Back
          </Button>
        </div>
      ) : <></>}
    </>
  );
}
