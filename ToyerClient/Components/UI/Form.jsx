import React from "react";
import { useRef, useImperativeHandle, forwardRef } from "react";

const Form = forwardRef(function Form(
  { onSave, children, ...otherProps },
  ref
) {
  const form = useRef(null);

  useImperativeHandle(ref, () => {
    return {
      clear() {
        form.current?.reset();
      },
      submit(url) {
        //here
      },
    };
  });

  function handleSubmit(event) {
    event.preventDefault();
    const formData = new FormData(event.currentTarget);
    const data = Object.fromEntries(formData);
    onSave(data);
  }

  return (
    <form onSubmit={handleSubmit} ref={form} {...otherProps}>
      {children}
    </form>
  );
});

export default Form;
