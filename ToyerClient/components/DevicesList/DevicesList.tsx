import { type DeviceType } from "../../Pages/DevicesPage/DevicesPage.tsx";
import List from "../List.tsx";
import { Link } from "react-router-dom";

type DevicesListProps = {
  deviceTypes: DeviceType[];
};

function DevicesList({ deviceTypes }: DevicesListProps) {
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
