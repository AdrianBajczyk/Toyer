import { faInfoCircle } from "@fortawesome/free-solid-svg-icons";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";

const PwdConfirmNote = ({ isInputFocus, isInputValid }) => {
    return (
        <p id="confirmnote" className={isInputFocus && !isInputValid ? "instructions" : "offscreen"}>
        <FontAwesomeIcon icon={faInfoCircle} />
        Must match the first password input field.
    </p>
    );
  };

export default PwdConfirmNote