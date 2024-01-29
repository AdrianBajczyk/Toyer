import { type LoaderFunction, useNavigate, useLoaderData } from "react-router-dom";
import Button from "../../Components/UI/Button.tsx"
import { DeviceType } from "../DevicesPage/DevicesPage.tsx";
import {get} from "../../Utils/http.ts"

type LoaderParams = {
    id: string
  };

  export default function DeviceDetials(){

    const navigate = useNavigate()
    const deviceType = useLoaderData() as DeviceType;

    return<>
    <p>{deviceType.id}</p>
    <p>{deviceType.name}</p>
    <p>{deviceType.description}</p>
    <Button element='button' onClick={() => navigate("..", { relative: "path" })}>Back</Button>
    </>
}

export const loader : LoaderFunction = async ({params}) => {
    const typedParams = params as LoaderParams
    return await get<DeviceType>(
        `https://localhost:7065/api/DeviceType/${typedParams.id}`
      );
}

