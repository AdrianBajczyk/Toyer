
import List from "../UI/List.jsx";
import { Link } from "react-router-dom";



function DevicesList({ deviceTypes }) {
  return (
    <>
      <List
        items={deviceTypes}
        renderItem={(device) => (
          <li key={"deviceType" + device.id}>
            <Link
              id={"deviceTypelink" + device.id.toString()}
              to={`/devices/${device.id}`}
            >
              {device.name}
            </Link>
          </li>
        )}
      />
    </>
  );
}

export default DevicesList;
