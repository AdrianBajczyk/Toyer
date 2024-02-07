import CustomForm from "../UI/CustomForm.jsx";
import Button from "../UI/Button.jsx";
import { useState, useEffect, useRef } from "react";
import { useActionData, useNavigation } from "react-router-dom";
import UserNameInput from "../ValidatedFormInputs/UserNameInput.jsx";
import NewPasswordInput from "../ValidatedFormInputs/NewPasswordInput.jsx";
import ProperNameInput from "../ValidatedFormInputs/ProperNameInput.jsx";
import EmailInput from "../ValidatedFormInputs/EmailInput.jsx";
import DateInput from "../ValidatedFormInputs/DateInput.jsx";
import NumberInput from "../ValidatedFormInputs/NumberInput.jsx";
import PostalCodeInput from "../ValidatedFormInputs/PostalCodeInput.jsx";
import PhoneInput from "../ValidatedFormInputs/PhoneInput.jsx";
import classes from "./RegisterForm.module.css"

export function RegisterForm() {
  const navigation = useNavigation();
  const actionData = useActionData();

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
          <UserNameInput
            userRef={userRef}
            onValidityChange={(inputName, isValid) =>
              handleValidityChange(inputName, isValid)
            }
          />
          <NewPasswordInput
            userRef={userRef}
            onValidityChange={(inputName, isValid) =>
              handleValidityChange(inputName, isValid)
            }
          />
          <ProperNameInput
            userRef={userRef}
            name="Name"
            onValidityChange={(inputName, isValid) =>
              handleValidityChange(inputName, isValid)
            }
          />
          <ProperNameInput
            userRef={userRef}
            name="Surname"
            onValidityChange={(inputName, isValid) =>
              handleValidityChange(inputName, isValid)
            }
          />
          <EmailInput
            userRef={userRef}
            onValidityChange={(inputName, isValid) =>
              handleValidityChange(inputName, isValid)
            }
          />
          <DateInput
            userRef={userRef}
            onValidityChange={(inputName, isValid) =>
              handleValidityChange(inputName, isValid)
            }
          />
          <ProperNameInput
            userRef={userRef}
            name="Street"
            onValidityChange={(inputName, isValid) =>
              handleValidityChange(inputName, isValid)
            }
          />
          <NumberInput
            userRef={userRef}
            name="StreetNumber"
            onValidityChange={(inputName, isValid) =>
              handleValidityChange(inputName, isValid)
            }
          />
          <NumberInput
            useRef={userRef}
            name="UnitNumber"
            optional={true}
            onValidityChange={(inputName, isValid) =>
              handleValidityChange(inputName, isValid)
            }
          />
          <ProperNameInput
            userRef={userRef}
            name="City"
            onValidityChange={(inputName, isValid) =>
              handleValidityChange(inputName, isValid)
            }
          />
          <ProperNameInput
            userRef={userRef}
            name="State"
            optional={true}
            onValidityChange={(inputName, isValid) =>
              handleValidityChange(inputName, isValid)
            }
          />
          <PostalCodeInput
            userRef={userRef}
            onValidityChange={(inputName, isValid) =>
              handleValidityChange(inputName, isValid)
            }
          />
          <ProperNameInput
            userRef={userRef}
            name="Country"
            onValidityChange={(inputName, isValid) =>
              handleValidityChange(inputName, isValid)
            }
          />
          <PhoneInput
            userRef={userRef}
            onValidityChange={(inputName, isValid) =>
              handleValidityChange(inputName, isValid)
            }
          />
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
