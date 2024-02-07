import { isFuture, isMatch as isDateMatch } from "date-fns";
import { isEarlierThan100YearsAgo } from "../../Utils/date.js";
import DateNote from "../UI/InputNotes/DateNote.jsx";
import { useState, useEffect } from "react";
import Input from "../UI/Input/Input.jsx";

const DateInput = ({ userRef, onValidityChange }) => {
  const [birthDate, setBirthDate] = useState("");
  const [validBirthDate, setValidBirthDate] = useState(false);
  const [birthDateFocus, setBirthDateFocus] = useState(false);

  useEffect(() => {
    if (
      isDateMatch(birthDate, "yyyy-MM-dd") &&
      !isFuture(birthDate) &&
      !isEarlierThan100YearsAgo(birthDate)
    ) {
      setValidBirthDate(true);
      onValidityChange("BirthDate", true);
    } else {
      setValidBirthDate(false);
      onValidityChange("BirthDate", false);
    }
  }, [birthDate]);

  return (
    <>
      <Input
        type="date"
        label="Birth date:"
        name="BirthDate"
        id="BirthDateInput"
        ref={userRef}
        onChange={(e) => setBirthDate(e.target.value)}
        value={birthDate}
        required
        aria-invalid={validBirthDate ? "false" : "true"}
        aria-describedby="datenote"
        onFocus={() => setBirthDateFocus(true)}
        onBlur={() => setBirthDateFocus(false)}
        validInput={validBirthDate}
      />
      <DateNote
        isInputFocus={birthDateFocus}
        isInputValid={validBirthDate}
        currentInput={birthDate}
      />
    </>
  );
};

export default DateInput;
