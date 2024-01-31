import { faInfoCircle } from "@fortawesome/free-solid-svg-icons";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";

const PostalNote = ({ isInputFocus, currentInput, isInputValid }) => {
  return (
    <p
      id="postalnote"
      className={
        isInputFocus && currentInput && !isInputValid
          ? "instructions"
          : "offscreen"
      }
    >
      <FontAwesomeIcon icon={faInfoCircle} />
      Only numbers and one optional hyphen allowed.
      <br />
       Ca 3 to 23 lowercase letters.
    </p>
  );
};

export default PostalNote;