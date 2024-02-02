import CustomForm from "../UI/CustomForm.jsx";
import Input from "../UI/Input/Input.jsx";
import Button from "../UI/Button.jsx";
import { useActionData, useNavigation } from "react-router-dom";
import { useState, useEffect, useRef } from "react";

const EMAIL_REGEX = /^[\w\-\.]+@([\w-]+\.)+[a-z]{2,3}$/;

export default function LoginForm() {
  const actionData = useActionData();
  const navigation = useNavigation();

  const [email, setEmail] = useState("");
  const [validEmail, setValidEmail] = useState(false);

  const [pwd, setPwd] = useState("");
  const [emptyPwd, setNotEmptyPwd] = useState(true);


  useEffect(() => {
    setValidEmail(EMAIL_REGEX.test(email));
  }, [email]);

  useEffect(() => {
    setNotEmptyPwd(pwd.length > 0);
  }, [pwd]);



  const isSubmitting = navigation.state === "submitting"
  const isSubmittBlocked = !validEmail || !emptyPwd;

  return (
    <section>
      <CustomForm method="post">
        <Input
          type="email"
          label="Email"
          name="email"
          id="EmailInput"
          onChange={(e) => setEmail(e.target.value)}
          value={email}
          required
          validInput={validEmail}
        />
        <Input
          type="password"
          label="Password"
          name="password"
          id="PasswordInput"
          onChange={(e) => setPwd(e.target.value)}
          value={pwd}
          required
          validInput={emptyPwd}
        />
        <Button element="button" disabled={isSubmittBlocked || isSubmitting}>
          {isSubmitting ? "Submitting..." : "Login"}
        </Button>
      </CustomForm>
      <Button element="link" to={"/register"} disabled={isSubmitting}>
        Create new user
      </Button>
      {actionData &&
        actionData.error &&
        typeof actionData.error === "object" && (
          <ul>
            {Object.entries(actionData.error).map(([key, value]) => (
              <li key={key}>
                <h3>{key}</h3>
                <p>{value}</p>
              </li>
            ))}
          </ul>
        )}
      {actionData &&
        actionData.error &&
        typeof actionData.error === "string" && <p>{actionData.error}</p>}
    </section>
  );
}
