import { useLoaderData } from "react-router-dom";
import { get } from "../../Utils/http";
import DevicesList from "../../Components/DevicesList/DevicesList.jsx"



export default function DevicesPage() {
  const deviceTypes = useLoaderData();

  return (
    <>
      <h1>DevicesPage</h1>
      <DevicesList deviceTypes={deviceTypes}></DevicesList>
    </>
  );
}

export async function loader() {

  return await get(
      `https://localhost:7065/api/DeviceType/34`
    );

}
