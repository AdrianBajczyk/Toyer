import { useState, useEffect } from "react";
import Input from "../UI/Input/Input";
import EmailNote from "../UI/InputNotes/EmailNote";

const EMAIL_REGEX = /^[\w\-\.]+@([\w-]+\.)+[a-z]{2,3}$/;

const EmailInput = ({ userRef, onValidityChange, optional=false, ...props  }) => {
  const [email, setEmail] = useState("");
  const [validEmail, setValidEmail] = useState(false);
  const [emailFocus, setEmailFocus] = useState(false);

  useEffect(() => {
    if (EMAIL_REGEX.test(email)) {
      setValidEmail(true);
      onValidityChange("Email", true);
    } else {
      setValidEmail(false);
      onValidityChange("Email", false);
    }
  }, [email]);

  return (
    <>
      <Input
        type="email"
        label="Email:"
        name="Email"
        id="EmailInput"
        ref={userRef}
        onChange={(e) => setEmail(e.target.value)}
        value={email}
        required={!optional}
        aria-invalid={validEmail ? "false" : "true"}
        aria-describedby="emailnote"
        onFocus={() => setEmailFocus(true)}
        onBlur={() => setEmailFocus(false)}
        validInput={validEmail}
        {...props}
      />
      <EmailNote
        isInputFocus={emailFocus}
        isInputValid={validEmail}
        currentInput={email}
      />
    </>
  );
};

export default EmailInput;
