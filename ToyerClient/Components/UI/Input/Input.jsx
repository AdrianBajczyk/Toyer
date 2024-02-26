import { forwardRef } from "react";
import { faCheck, faTimes } from "@fortawesome/free-solid-svg-icons";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";

const Input = forwardRef(function Input(
  { id, validInput, inputState, label, checkIcon = true, textarea = false, ...props },
  ref
) {
  const InputComponent = textarea ? 'textarea' : 'input'; 

  const inputClass = textarea ? 'textarea-style' : 'input-style';

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
      <InputComponent id={id} name={id} ref={ref} autoComplete="off" className={inputClass} {...props} />
    </>
  );
});

export default Input;