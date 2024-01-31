import { faInfoCircle } from "@fortawesome/free-solid-svg-icons";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";

const NumNote = ({ isInputFocus, currentInput, isInputValid }) => {
  return (
    <p
      id="numnote"
      className={
        isInputFocus && currentInput && !isInputValid
          ? "instructions"
          : "offscreen"
      }
    >
      <FontAwesomeIcon icon={faInfoCircle} />
      Must contain 1-4 numbers.
    </p>
  );
};

export default NumNote;