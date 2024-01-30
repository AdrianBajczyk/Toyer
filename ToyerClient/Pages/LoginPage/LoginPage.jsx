import { redirect } from "react-router-dom";
import { post } from "../../Utils/http.js"
import AuthForm from "../../Components/AuthForm/AuthForm";


export default function LoginPage(props) {

  return <>
  <AuthForm/>
  </>;
}

export async function action({request, params}){
  const formData = await request.formData();
  const data = Object.fromEntries(formData);
  console.log(data)
  const response = await post('https://localhost:7065/api/User/Login', data)
  
  if(response.status === 422){
      return response
  }
  

  console.log(response)
  return redirect('/')
}
