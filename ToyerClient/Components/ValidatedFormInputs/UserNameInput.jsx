import UserNote from "../UI/InputNotes/UserNote";
import Input from "../UI/Input/Input";
import { useState, useEffect } from "react";

const USER_REGEX = /^[A-z][A-z0-9-_]{3,23}$/;

const UserNameInput = ({ userRef, onValidityChange, ...props }) => {
  const [user, setUser] = useState("");
  const [validUser, setValidUser] = useState(false);
  const [userFocus, setUserFocus] = useState(false);

  useEffect(() => {
    if (USER_REGEX.test(user)) {
      setValidUser(true);
      onValidityChange("UserName", true);
    } else {
      setValidUser(false);
      onValidityChange("UserName", false);
    }
  }, [user]);

  return (
    <>
      <Input
        type="text"
        label="Username:"
        name="UserName"
        id="UserNameInput"
        ref={userRef}
        onChange={(e) => setUser(e.target.value)}
        value={user}
        required
        aria-invalid={validUser ? "false" : "true"}
        aria-describedby="uidnote"
        onFocus={() => setUserFocus(true)}
        onBlur={() => setUserFocus(false)}
        validInput={validUser}
        {...props}
      />
      <UserNote
        isInputFocus={userFocus}
        isInputValid={validUser}
        currentInput={user}
      />
    </>
  );
};

export default UserNameInput;
