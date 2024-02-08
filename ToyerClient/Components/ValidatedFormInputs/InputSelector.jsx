import EmailInput from "./EmailInput";
import NewPasswordInput from "./NewPasswordInput";
import NumberInput from "./NumberInput";
import PhoneInput from "./PhoneInput";
import ProperNameInput from "./ProperNameInput";
import UserNameInput from "./UserNameInput";
import DateInput from "./DateInput";
import PostalCodeInput from "./PostalCodeInput";

const InputSelector = ({ name, ...props }) => {
  switch (name) {
    case "Email":
      return <EmailInput {...props} />;
    case "NewPassword":
      return <NewPasswordInput {...props} />;
    case "PhoneNumber":
      return <PhoneInput {...props} />;
    case "BirthDate":
      return <DateInput {...props} />;
    case "StreetNumber" :
      return <NumberInput name={name} {...props} />;
    case "UnitNumber":
      return <NumberInput name={name} {...props} />;
    case "UserName":
      return <UserNameInput {...props} />;
    case "PostalCode":
      return <PostalCodeInput {...props} />;
    default:
      return <ProperNameInput name={name} {...props} />;
  }
};

export default InputSelector;
