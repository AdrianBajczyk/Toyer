import { faInfoCircle } from "@fortawesome/free-solid-svg-icons";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";

const PwdNote = ({ isInputFocus, currentInput, isInputValid }) => {
  return (
    <p
      id="uidnote"
      className={
        isInputFocus && currentInput && !isInputValid
          ? "instructions"
          : "offscreen"
      }
    >
      <FontAwesomeIcon icon={faInfoCircle} />
      8 to 24 characters.
      <br />
      Must include uppercase and lowercase letters, a number and a special
      character.
      <br />
      Required at least one special character: <span aria-label="exclamation mark">
        !
      </span>{" "}
      <span aria-label="at symbol">@</span> <span aria-label="hashtag">#</span>{" "}
      <span aria-label="dollar sign">$</span>{" "}
      <span aria-label="percent">%</span>
    </p>
  );
};

export default PwdNote;
