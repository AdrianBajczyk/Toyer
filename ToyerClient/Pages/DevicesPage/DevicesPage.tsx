import { useLoaderData } from "react-router-dom";
import { get } from "../../Utils/http";
import DevicesList from "../../Components/DevicesList/DevicesList.tsx"

export interface DeviceType {
  id: number;
  name: string;
  description: string;
  orders: OrderType[];
}

interface OrderType {
  id: number;
  name: string;
  description: string;
}

export default function DevicesPage() {
  const deviceTypes = useLoaderData() as DeviceType[];

  return (
    <>
      <h1>DevicesPage</h1>
      <DevicesList deviceTypes={deviceTypes}></DevicesList>
    </>
  );
}

export async function loader() {
  try {
    const responseData = await get<DeviceType[]>(
      `https://localhost:7065/api/DeviceType`
    );

    return responseData;
  } catch (error) {
    if (error instanceof Error) {
    }
  }
}
