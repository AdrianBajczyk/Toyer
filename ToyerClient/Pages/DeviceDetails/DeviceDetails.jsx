import { useNavigate, useLoaderData } from "react-router-dom";
import Button from "../../Components/UI/Button.jsx"
import { get } from "../../Utils/http.js"



  export default function DeviceDetials(){

    const navigate = useNavigate();
    const deviceType = useLoaderData();

    return<>
    <p>{deviceType.id}</p>
    <p>{deviceType.name}</p>
    <p>{deviceType.description}</p>
    <Button element='button' onClick={() => navigate("..", { relative: "path" })}>Back</Button>
    </>
}

export const loader = async ({params}) => {
    const typedParams = params 
    return get(
        `https://localhost:7065/api/DeviceType/${typedParams.id}`
      );
}

