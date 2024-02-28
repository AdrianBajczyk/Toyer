import { faInfoCircle } from "@fortawesome/free-solid-svg-icons";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";

const PropNote = ({ isInputFocus, currentInput, isInputValid }) => {
  return (
    <p
      id="propnote"
      className={
        isInputFocus && currentInput && !isInputValid
          ? "instructions"
          : "offscreen"
      }
    >
      <FontAwesomeIcon icon={faInfoCircle} />
      Must start with an uppercase letter.
      <br />
      From 3 to 10 numbers.
    </p>
  );
};

export default PropNote;
