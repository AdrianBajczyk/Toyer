import { useNavigate } from "react-router";
import Button from "../../Components/Button.tsx"



function DeviceDetials(){

    const navigate = useNavigate()

    return<>
    <p>SomeDetails</p>
    <Button element='button' onClick={() => navigate("..", { relative: "path" })}>Back</Button>
    </>
}

export default DeviceDetials;