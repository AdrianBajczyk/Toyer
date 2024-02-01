import {  useActionData, useNavigate } from "react-router-dom";
import { post } from "../../Utils/http.js"
import LoginForm from "../../Components/LoginForm/LoginForm.jsx";
import { useUserContext } from "../../Store/user-context.jsx";
import { useEffect } from "react";


export default function LoginPage() {

  const actionData = useActionData();
  const userCtx = useUserContext();
  const goTo = useNavigate();

  if (actionData && actionData.state === "success"){
    userCtx.logIn();
    actionData.state = '';
  }

  return <>
  <LoginForm/>
  </>;
}

export async function action({request, params}){
  const formData = await request.formData();
  const data = Object.fromEntries(formData);
  const response = await post('https://localhost:7065/api/User/Login', data)
  
  if(response.status === 422 || response.status === 401){
    return response
  }
  
  if(response.status === 200)
  return { state: "success", response: {response} }
  
}
