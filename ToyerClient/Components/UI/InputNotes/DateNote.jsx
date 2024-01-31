import { faInfoCircle } from "@fortawesome/free-solid-svg-icons";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";

const DateNote = ({ isInputFocus, currentInput, isInputValid }) => {
  return (
    <p
      id="datenote"
      className={
        isInputFocus && currentInput && !isInputValid
          ? "instructions"
          : "offscreen"
      }
    >
      <FontAwesomeIcon icon={faInfoCircle} />
      Can't represent future.
      <br />
      Can't be earlier than 100 years ago.
    </p>
  );
};

export default DateNote;