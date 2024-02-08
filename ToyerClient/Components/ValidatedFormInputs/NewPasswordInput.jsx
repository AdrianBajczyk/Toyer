import { useState, useEffect } from "react";
import Input from "../UI/Input/Input";
import PwdNote from "../UI/InputNotes/PwdNote";
import PwdConfirmNote from "../UI/InputNotes/PwdConfirmNote";

const PWD_REGEX = /^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#$%]).{8,24}$/;

const NewPasswordInput = ({userRef, onValidityChange, ...props}) => {

  const [pwd, setPwd] = useState("");
  const [validPwd, setValidPwd] = useState(false);
  const [pwdFocus, setPwdFocus] = useState(false);

  const [confirmPwd, setConfirmPwd] = useState("");
  const [validConfirm, setValidConfirm] = useState(false);
  const [confirmFocus, setConfirmFocus] = useState(false);

  useEffect(() => {
    setValidPwd(PWD_REGEX.test(pwd));
    setValidConfirm(pwd === confirmPwd && confirmPwd);
    if (PWD_REGEX.test(pwd)) {
      setValidPwd(true);
    } else if (pwd === confirmPwd && confirmPwd) {
      setValidConfirm(true);
    } else if (validPwd && validConfirm) {
      onValidityChange("Password", true)
    } else {
      onValidityChange("Password", false)
    }
  }, [pwd, confirmPwd]);

  return (
    <>
      <Input
        type="password"
        label="Password:"
        name="Password"
        id="PasswordInput"
        ref={userRef}
        onChange={(e) => setPwd(e.target.value)}
        value={pwd}
        required
        aria-invalid={validPwd ? "false" : "true"}
        aria-describedby="pwdnote"
        onFocus={() => setPwdFocus(true)}
        onBlur={() => setPwdFocus(false)}
        validInput={validPwd}
        {...props}
      />
      <PwdNote
        isInputFocus={pwdFocus}
        isInputValid={validPwd}
        currentInput={pwd}
      />
      <Input
        type="password"
        label="Confirm password:"
        name="ConfirmPassword"
        id="ConfirmPasswordInput"
        ref={userRef}
        onChange={(e) => setConfirmPwd(e.target.value)}
        value={confirmPwd}
        required
        aria-invalid={validConfirm ? "false" : "true"}
        aria-describedby="confirmnote"
        onFocus={() => setConfirmFocus(true)}
        onBlur={() => setConfirmFocus(false)}
        validInput={validConfirm}
      />
      <PwdConfirmNote isInputFocus={confirmFocus} currentInput={confirmPwd} isInputValid={validConfirm} />
    </>
  );
};

export default NewPasswordInput;
