import EmailForm from "../../Components/Forms/EmailForm/EmailForm";
import classes from "./EmailPage.module.css";
import { post } from "../../Utils/http";
import { redirect } from "react-router-dom";

const EmailPage = () => {
  return (
    <div className={classes.EmailPageContainer}>
      <EmailForm />
    </div>
  );
};

export default EmailPage;

export async function action({ request }) {
  const formData = await request.formData();
  const data = Object.fromEntries(formData);

  const response = await post("MessageService", data);

  if (response.status === 422) {
    return response;
  }

  return redirect("/email/success");
}
