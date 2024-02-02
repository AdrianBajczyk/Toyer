import { useActionData, useNavigate } from "react-router-dom";
import { post } from "../../Utils/http.js";
import LoginForm from "../../Components/LoginForm/LoginForm.jsx";
import { useEffect } from "react";

export default function LoginPage() {
  const actionData = useActionData();
  const navigate = useNavigate();

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
    userCtx.setUser({ email: data.email, token: response.token });
    userCtx.logIn();
    return response;
  };
