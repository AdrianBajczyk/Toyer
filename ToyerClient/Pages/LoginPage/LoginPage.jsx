import {  useActionData, useNavigate } from "react-router-dom";
import { post } from "../../Utils/http.js"
import AuthForm from "../../Components/AuthForm/AuthForm";
import { useUserContext } from "../../Store/user-context.jsx";
import { useEffect } from "react";


export default function LoginPage() {

  const actionData = useActionData();
  const userCtx = useUserContext();
  const goTo = useNavigate();

  if (actionData && actionData.state === "success"){
    userCtx.logIn();
    console.log(actionData)
    Object.keys(actionData).forEach(key => {
      delete actionData[key];
    });
  }

  return <>
  <AuthForm/>
  </>;
}

export async function action({request, params}){
  const formData = await request.formData();
  const data = Object.fromEntries(formData);
  console.log(data)
  const response = await post('https://localhost:7065/api/User/Login', data)
  
  if(response.status === 422 || response.status === 401){
    return response
  }
  
  if(response.status === 200)
  return { state: "success", response: {response} }
  
}
