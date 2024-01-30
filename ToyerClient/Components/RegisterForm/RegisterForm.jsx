import CustomForm from "../UI/CustomForm.jsx";
import Input from "../UI/Input/Input.jsx";
import Button from "../UI/Button.jsx";
import { useActionData, useNavigation } from "react-router-dom";

export function RegisterForm() {
  const navigation = useNavigation();
  const actionData = useActionData();

  const isSubmitting = navigation.state === "submitting";

  return (
    <>
      <CustomForm method="post">
        <Input
          type="text"
          label="User name"
          name="UserName"
          id="UserNameInput"
        />
        <Input
          type="password"
          label="Password"
          name="Password"
          id="PasswordInput"
        />
        <Input
          type="password"
          label="Confirm password"
          name="ConfirmPassword"
          id="ConfirmPasswordInput"
        />
        <Input type="text" label="Name" name="Name" id="NameInput" />
        <Input type="text" label="Surname" name="Surname" id="SurnameInput" />
        <Input type="email" label="Email" name="Email" id="EmailInput" />
        <Input
          type="date"
          label="Birth date"
          name="BirthDate"
          id="BirthDateInput"
        />
        <Input type="text" label="Street" name="Street" id="StreetInput" />
        <Input
          type="number"
          label="Street number"
          name="StreetNumber"
          id="StreetNumberInput"
        />
        <Input
          type="number"
          label="Unit number"
          name="UnitNumber"
          id="UnitNumberInput"
        />
        <Input type="text" label="City" name="City" id="CityInput" />
        <Input type="text" label="State" name="State" id="StateInput" />
        <Input
          type="text"
          label="Postal code"
          name="PostalCode"
          id="PostalCodeInput"
        />
        <Input type="text" label="Country" name="Country" id="CountryInput" />
        <Input type="text" label="Phone" name="PhoneNumber" id="PhoneInput" />
        <Button element="button" disabled={isSubmitting}>
          {isSubmitting ? "Submitting" : "Register"}
        </Button>
      </CustomForm>
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
    </>
  );
}

export default RegisterForm;
