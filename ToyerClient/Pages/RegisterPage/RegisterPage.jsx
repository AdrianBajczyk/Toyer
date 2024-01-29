import { redirect } from "react-router-dom";
import RegisterForm from "../../Components/RegisterForm/RegisterForm";
import {post} from "../../Utils/http.js"

export default function RegisterPage(){
    return <RegisterForm/>
    
}

export async function action({request, params}){

const formData = await request.formData();
const data = Object.fromEntries(formData);

await post('https://localhost:7065/api/User', data)

return redirect('/')
}