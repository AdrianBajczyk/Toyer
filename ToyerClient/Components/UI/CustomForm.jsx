import React from "react";
import { Form } from "react-router-dom";
import { useRef, useImperativeHandle, forwardRef } from "react";

const CustomForm = function CustomForm({children, ...otherProps }) {
  
  return (
    <Form {...otherProps}>
      {children}
    </Form>
  );
};

export default CustomForm;
