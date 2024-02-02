import { post } from "../../Utils/http.js";
import LoginForm from "../../Components/LoginForm/LoginForm.jsx";
import { redirect } from "react-router-dom";

export default function LoginPage() {
  return (
    <>
      <LoginForm />
    </>
  );
}

export const action =
  (userCtx) =>
  async ({ request }) => {
    const formData = await request.formData();
    const data = Object.fromEntries(formData);
    const response = await post("https://localhost:7065/api/User/Login", data);
    if (response.status !== 200) {
      return response;
    }
    userCtx.setUser({ email: data.email, id: response.id ,token: response.token });
    userCtx.logIn();
    
    console.log(userCtx)
    return response;
  };
