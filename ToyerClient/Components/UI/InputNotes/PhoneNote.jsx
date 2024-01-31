import { faInfoCircle } from "@fortawesome/free-solid-svg-icons";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";

const PhoneNote = ({ isInputFocus, currentInput, isInputValid }) => {
  return (
    <p
      id="phonenote"
      className={
        isInputFocus && currentInput && !isInputValid
          ? "instructions"
          : "offscreen"
      }
    >
      <FontAwesomeIcon icon={faInfoCircle} />
      Only "(+12) 123-123-123" format allowed. 
    </p>
  );
};

export default PhoneNote;