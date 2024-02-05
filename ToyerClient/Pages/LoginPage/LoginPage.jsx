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


