import { useLoaderData } from "react-router-dom";
import { get } from "../../Utils/http";
import DevicesList from "../../Components/DevicesList/DevicesList.jsx";
import classes from "./DevicesPage.module.css"

export default function DevicesPage() {
  const deviceTypes = useLoaderData();

  console.log(deviceTypes)

  return (
    <>
      <DevicesList deviceTypes={deviceTypes}></DevicesList>

    </>
  );
}

export async function loader() {
  return await get(`DeviceType`);
}
