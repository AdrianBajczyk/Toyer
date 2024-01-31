import CustomForm from "../UI/CustomForm.jsx";
import Input from "../UI/Input/Input.jsx";
import Button from "../UI/Button.jsx";
import { useActionData, useNavigation } from "react-router-dom";

export default function AuthForm() {
  const actionData = useActionData();
  const navigation = useNavigation();

  const isSubmitting = navigation.state === "submitting";

  return (
    <section>
      <CustomForm method="post">
        <Input type="email" label="Email" name="Email" id="EmailInput" />
        <Input
          type="password"
          label="Password"
          name="Password"
          id="PasswordInput"
        />
        <Button element="button" disabled={isSubmitting}>
          {isSubmitting ? "Submitting..." : "Login"}
        </Button>
      </CustomForm>
      <Button element="link" to={"/register"} disabled={isSubmitting}>
          Create new user
        </Button>
      {actionData && actionData.errors && (
        <ul>
          {Object.entries(actionData.errors).map(([key, value]) => (
            <li key={key}>
              <h3>{key}</h3>
              <p>{value}</p>
            </li>
          ))}
        </ul>
      )}
    </section>
  );
}
