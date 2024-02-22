import { useState, useEffect } from "react";
import Input from "../UI/Input/Input";
import PropNote from "../UI/InputNotes/PropNote";

const PROP_REGEX = /^[A-Z][a-z]{2,23}$/;

const ProperNameInput = ({
  userRef,
  name,
  optional = false,
  onValidityChange,
  ...props
}) => {
  const [input, setInput] = useState("");
  const [validInput, setValidInput] = useState(false);
  const [inputFocus, setInputFocus] = useState(false);

  useEffect(() => {
    if (PROP_REGEX.test(input)) {
      setValidInput(true);
      onValidityChange(name, true);
    } else if (optional && !input) {
      setValidInput(true);
      onValidityChange(name, true);
    } else {
      setValidInput(false);
      onValidityChange(name, false);
    }
  }, [input]);

  return (
    <>
      <Input
        type="text"
        label={`${name}:`}
        name={name}
        id={`${name}Input`}
        ref={userRef}
        onChange={(e) => setInput(e.target.value)}
        value={input}
        required={!optional}
        aria-invalid={validInput ? "false" : "true"}
        aria-describedby="propnote"
        onFocus={() => setInputFocus(true)}
        onBlur={() => setInputFocus(false)}
        validInput={validInput}
        {...props}
      />
      <PropNote
        isInputFocus={inputFocus}
        isInputValid={validInput}
        currentInput={input}
      />
    </>
  );
};

export default ProperNameInput;
