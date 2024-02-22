import CustomForm from "../../UI/CustomForm.jsx";
import Button from "../../UI/Button.jsx";
import { useState, useEffect, useRef } from "react";
import { useActionData, useNavigation } from "react-router-dom";
import classes from "./RegisterForm.module.css";
import InputSelector from "../../ValidatedFormInputs/InputSelector.jsx";

const inputs = [
  { name: "UserName", optional: false },
  { name: "NewPassword", optional: false },
  { name: "Name", optional: false },
  { name: "Surname", optional: false },
  { name: "Email", optional: false },
  { name: "BirthDate", optional: false },
  { name: "Street", optional: false },
  { name: "StreetNumber", optional: false },
  { name: "UnitNumber", optional: true },
  { name: "City", optional: false },
  { name: "State", optional: true },
  { name: "PostalCode", optional: false },
  { name: "Country", optional: false },
  { name: "PhoneNumber", optional: true },
];

export function RegisterForm() {
  const actionData = useActionData();
  const navigation = useNavigation();

  const isSubmitting = navigation.state === "submitting";
  const userRef = useRef();

  const [formValidity, setFormValidity] = useState({
    UserName: false,
    Password: false,
    Name: false,
    Surname: false,
    Email: false,
    BirthDate: false,
    Street: false,
    StreetNumber: false,
    UnitNumber: true,
    City: false,
    State: true,
    PostalCode: false,
    Country: false,
    PhoneNumber: true,
  });

  const handleValidityChange = (inputName, isValid) => {
    setFormValidity((prevValidity) => ({
      ...prevValidity,
      [inputName]: isValid,
    }));
  };

  useEffect(() => {
    userRef.current?.focus();
  }, []);

  const isFormValid = Object.values(formValidity).every(
    (value) => value === true
  );

  return (
    <>
      <section className={classes.registerContainer}>
        <h1>Register</h1>
        <CustomForm method="post">
          {inputs.map((input, index) => (
            <InputSelector
              key={input.name+index}
              userRef={userRef}
              name={input.name}
              optional={input.optional}
              onValidityChange={(inputName, isValid) =>
                handleValidityChange(inputName, isValid)
              }
            />
          ))}
          <Button element="button" disabled={isSubmitting || !isFormValid}>
            {isSubmitting ? "Submitting" : "Register"}
          </Button>
        </CustomForm>
        {actionData &&
          actionData.error &&
          typeof actionData.error === "object" && (
            <ul
              className={actionData.error ? "errmsg" : "offscreen"}
              aria-live="assertive"
            >
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
          typeof actionData.error === "string" && (
            <p
              className={actionData.error ? "errmsg" : "offscreen"}
              aria-live="assertive"
            >
              {actionData.error}
            </p>
          )}
      </section>
    </>
  );
}

export default RegisterForm;
