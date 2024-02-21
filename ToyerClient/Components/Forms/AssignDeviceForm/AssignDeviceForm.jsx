import classes from './AssignDeviceForm.module.css'
import CustomForm from "../../UI/CustomForm";
import { useRef, useState, useEffect } from "react";
import InputSelector from "../../ValidatedFormInputs/InputSelector";
import Button from "../../UI/Button";
import { useNavigation, useActionData } from "react-router-dom";


const AssignDeviceForm = () => {

  const actionData = useActionData();
  const navigation = useNavigation();
  const userRef = useRef();
  
    const isSubmitting = navigation.state === "submitting";
  
    const [isFormValid, setIsFormValid] = useState(false);
  
    useEffect(() => {
      userRef.current?.focus();
    }, []);
  
    const handleValidityChange = (inputName, isValid) => {
      setIsFormValid(isValid);
    };

    return (
        <>
        <section className={classes.assigmentContainer}>
        <CustomForm method="post">
            <InputSelector
              userRef={userRef}
              name="Guid"
              optional={false}
              onValidityChange={(inputName, isValid) =>
                handleValidityChange(inputName, isValid)
              }
            />
            <Button element="button" disabled={isSubmitting || !isFormValid}>
                {isSubmitting ? "Submitting" : "Register"}
              </Button>
          </CustomForm>
        </section>
        </>
          
      );
}

export default AssignDeviceForm