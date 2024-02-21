import { faInfoCircle } from "@fortawesome/free-solid-svg-icons";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";

const GuidNote = ({ isInputFocus, currentInput, isInputValid }) => {
    return (
        <p
          id="guidnote"
          className={
            isInputFocus && currentInput && !isInputValid
              ? "instructions"
              : "offscreen"
          }
        >
          <FontAwesomeIcon icon={faInfoCircle} />
          Enter your unique device id.
          <br />
          ########-####-####-############
          <br/>
          Each "#" represents a hexadecimal digit (0-9, A-F).
        </p>
      );
}

export default GuidNote