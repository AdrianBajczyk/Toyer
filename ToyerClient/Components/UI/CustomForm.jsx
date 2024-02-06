import React from "react";
import { Form } from "react-router-dom";

const CustomForm = function CustomForm({children, ...otherProps }) {
  
  return (
    <Form {...otherProps}>
      {children}
    </Form>
  );
};

export default CustomForm;
