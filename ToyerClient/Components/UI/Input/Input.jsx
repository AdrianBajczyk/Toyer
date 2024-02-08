import { forwardRef } from "react";
import { faCheck, faTimes } from "@fortawesome/free-solid-svg-icons";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";

const Input = forwardRef(function Input(
  { id, validInput, inputState, label, checkIcon = true, ...props },
  ref
) {
  return (
    <>
      <label htmlFor={id}>
        {label}
        {checkIcon && (
          <>
            <FontAwesomeIcon
              icon={faCheck}
              className={validInput ? "valid" : "hide"}
            />
            <FontAwesomeIcon
              icon={faTimes}
              className={validInput || !props.value ? "hide" : "invalid"}
            />
          </>
        )}
      </label>
      <input id={id} name={id} ref={ref} autoComplete="off" {...props} />
    </>
  );
});

export default Input;
