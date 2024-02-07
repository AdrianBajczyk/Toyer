import { useState, useEffect } from "react";
import Input from "../UI/Input/Input";
import PhoneNote from "../UI/InputNotes/PhoneNote";

const PHONE_REGEX = /^\(\+\d{2}\) \d{3}-\d{3}-\d{3}$/;

const PhoneInput = ({ userRef, onValidityChange }) => {
  const [phone, setPhone] = useState("");
  const [validPhone, setValidPhone] = useState(false);
  const [phoneFocus, setPhoneFocus] = useState(false);

  useEffect(() => {
  
    if (PHONE_REGEX.test(phone) || !phone) {
      setValidPhone(true);
      onValidityChange("PhoneNumber", true);
    } else {
      setValidPhone(false);
      onValidityChange("PhoneNumber", false);
    }
  }, [phone]);

  return (
    <>
      <Input
        type="tel"
        label="Phone:"
        name="PhoneNumber"
        id="PhoneInput"
        ref={userRef}
        onChange={(e) => setPhone(e.target.value)}
        value={phone}
        aria-invalid={validPhone ? "false" : "true"}
        aria-describedby="phonenote"
        onFocus={() => setPhoneFocus(true)}
        onBlur={() => setPhoneFocus(false)}
        validInput={validPhone}
      />
      <PhoneNote
        isInputFocus={phoneFocus}
        isInputValid={validPhone}
        currentInput={phone}
      />
    </>
  );
};

export default PhoneInput;
