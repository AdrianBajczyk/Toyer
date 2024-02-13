import { redirect } from "react-router-dom";
import RegisterForm from "../../Components/RegisterForm/RegisterForm";
import { post } from "../../Utils/http.js";

export default function RegisterPage() {
  return <RegisterForm />;
}

export async function action({ request }) {
  const formData = await request.formData();
  const data = Object.fromEntries(formData);

  if (data.UnitNumber === "") {
    data.UnitNumber = 0;
  }

  const redirectUrl = window.location.origin + '/email/confirm';

  const response = await post("User", data,  redirectUrl );

  if (response.status === 422) {
    return response;
  }

  return redirect("/register/success");
}
