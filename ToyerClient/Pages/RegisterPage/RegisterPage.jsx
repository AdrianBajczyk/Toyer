import { redirect } from "react-router-dom";
import RegisterForm from "../../Components/Forms/RegisterForm/RegisterForm.jsx";
import { post } from "../../Utils/http.js";
import classes from './RegisterPage.module.css'

export default function RegisterPage() {
  return (
    <div className={classes.registerForm}>
      <RegisterForm />
    </div>
  );
}

export async function action({ request }) {
  const formData = await request.formData();
  const data = Object.fromEntries(formData);

  if (data.UnitNumber === "") {
    data.UnitNumber = 0;
  }

  const redirectUrl = window.location.origin + "/email/confirm";

  const response = await post("User", data, redirectUrl);

  if (response.status === 422) {
    return response;
  }

  return redirect("/register/success");
}
