import { Link } from "react-router-dom";
import { List } from "../../Components/List";

interface DeviceType {
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

//replace with actual fetch data
const dummyDevices: DeviceType[] = [
  {
    id: 1,
    name: "device1",
    description:
      "Lorem ipsum dupa z kota potem przyszedł dziad i zasmrodził cały świat",
    orders: [
      {
        id: 1,
        name: "order1",
        description:
          "Nie masz dzisiaj prawdziwej przyjaźni na świecie. Ostatni znam jej przykład w oszmiańskim powiecie. Tam żył...",
      },
    ],
  },
  {
    id: 2,
    name: "device2",
    description:
      'Abecdało z pieca spadło o ziemię się hukło. Rozsypało się po kątach, strasznie się potłukło. "I" - zgubiło kropeczkę...',
    orders: [
      {
        id: 0,
        name: "order2",
        description:
          "Rada małpa, gdy się śmieli, kiedy mogła udać człeka. Widząc panią raz w kąpieli - wlazła pod stół, cicho czeka...",
      },
    ],
  },
];

export default function DevicesPage() {
  return (
    <>
      <h1>DevicesPage</h1>
      <List
        items={dummyDevices}
        renderItem={(device) => (
          <li key={"deviceType" + device.id}>
            <Link
              id={"deviceTypelink" + device.id.toString()}
              to={`/devices/${device.id}`}
            >{device.name}</Link>
          </li>
        )}
      />
    </>
  );
}
