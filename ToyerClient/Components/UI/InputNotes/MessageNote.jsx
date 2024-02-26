import { faInfoCircle } from "@fortawesome/free-solid-svg-icons";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";

const MessageNote = ({ isInputFocus, currentInput, isInputValid }) => {
  return (
    <p
    id="messageNote"
    className={
      isInputFocus && currentInput && !isInputValid
        ? "instructions"
        : "offscreen"
    }
  >
    <FontAwesomeIcon icon={faInfoCircle} />
    The message must be 
    <br />
    between 20 and 500 characters in length.
  </p>
  )
}

export default MessageNote