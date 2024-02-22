import { useLoaderData, useNavigation } from "react-router-dom";
import { get } from "../../Utils/http";
import DevicesList from "../../Components/DevicesList/DevicesList.jsx";
import classes from './DevicesPage.module.css'

export default function DevicesPage() {
  const deviceTypes = useLoaderData();

  return (
    <div className={classes.cardsContainer}>
      <DevicesList deviceTypes={deviceTypes}></DevicesList>
    </div>

  );
}

export async function loader() {
  return await get(`DeviceType`);
}
