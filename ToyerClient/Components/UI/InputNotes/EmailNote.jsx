import { faInfoCircle } from "@fortawesome/free-solid-svg-icons";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";

const EmailNote = ({ isInputFocus, currentInput, isInputValid }) => {
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
      Must include the "yourEmailName" + "@" symbol.
      <br />
      Followed by a domain name, such as "example.com".
    </p>
  );
};

export default EmailNote;
