import { useState, useEffect } from "react";
import NumNote from "../UI/InputNotes/NumNote";
import Input from "../UI/Input/Input";

const DIGIT_REGEX = /^\d{1,4}$/;

const NumberInput = ({ userRef, name, onValidityChange, optional = false, ...props }) => {
  const [num, setNum] = useState("");
  const [validNum, setValidNum] = useState(false);
  const [numFocus, setNumFocus] = useState(false);

  useEffect(() => {
    if (DIGIT_REGEX.test(num)) {
      setValidNum(true);
      onValidityChange(name, true);
    } else if (optional && !num) {
      setValidNum(true);
      onValidityChange(name, true);
    } else {
      setValidNum(false);
      onValidityChange(name, false);
    }
  }, [num]);

  const labelName = addSpaceBeforeUppercase(name);

  return (
    <>
      <Input
        type="text"
        label={`${labelName}:`}
        name={name}
        id={`${name}Input`}
        ref={userRef}
        onChange={(e) => setNum(e.target.value)}
        value={num}
        required
        aria-invalid={validNum ? "false" : "true"}
        aria-describedby="numnote"
        onFocus={() => setNumFocus(true)}
        onBlur={() => setNumFocus(false)}
        validInput={validNum}
        {...props}
      />
      <NumNote
        isInputFocus={numFocus}
        isInputValid={validNum}
        currentInput={num}
      />
    </>
  );
};

export default NumberInput;

function addSpaceBeforeUppercase(str) {
  let result = "";
  for (let i = 0; i < str.length; i++) {
    if (
      i > 0 &&
      str[i] === str[i].toUpperCase() &&
      str[i - 1] !== str[i - 1].toUpperCase()
    ) {
      result += " ";
    }
    result += str[i];
  }
  return result;
}
