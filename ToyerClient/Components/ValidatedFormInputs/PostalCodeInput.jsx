import { useState, useEffect } from "react";
import PostalNote from "../UI/InputNotes/PostalNote";
import Input from "../UI/Input/Input";

const POSTAL_REGEX = /^(?=(?:[^-]*-?[^-]*){0,1}$)[A-Z0-9-]{5,10}$/;

const PostalCodeInput = ({ userRef, onValidityChange, optional=false,...props }) => {
  const [postal, setPostal] = useState("");
  const [validPostal, setValidPostal] = useState(false);
  const [postalFocus, setPostalFocus] = useState(false);

  useEffect(() => {
    if (POSTAL_REGEX.test(postal)) {
      setValidPostal(true);
      onValidityChange("PostalCode", true);
    } else {
      setValidPostal(false);
      onValidityChange("PostalCode", false);
    }
  }, [postal]);

  return (
    <>
      <Input
        type="text"
        label="Postal Code:"
        name="PostalCode"
        id="PostalCodeInput"
        ref={userRef}
        onChange={(e) => setPostal(e.target.value)}
        value={postal}
        required={!optional}
        aria-invalid={validPostal ? "false" : "true"}
        aria-describedby="postalnote"
        onFocus={() => setPostalFocus(true)}
        onBlur={() => setPostalFocus(false)}
        validInput={validPostal}
        {...props}
      />
      <PostalNote
        isInputFocus={postalFocus}
        isInputValid={validPostal}
        currentInput={postal}
      />
    </>
  );
};

export default PostalCodeInput;
