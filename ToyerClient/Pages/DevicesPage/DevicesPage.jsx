import { useLoaderData } from "react-router-dom";
import { get } from "../../Utils/http";
import DevicesList from "../../Components/DevicesList/DevicesList.jsx";


export default function DevicesPage() {
  const deviceTypes = useLoaderData();

  return (
    <>
      <DevicesList deviceTypes={deviceTypes}></DevicesList>

    </>
  );
}

export async function loader() {
  return await get(`DeviceType`);
}
