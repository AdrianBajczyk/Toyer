import classes from "./AssignDeviceForm.module.css";
import CustomForm from "../../UI/CustomForm";
import { useRef, useState, useEffect } from "react";
import Input from "../../UI/Input/Input";
import Button from "../../UI/Button";
import { useNavigate, useLocation } from "react-router-dom";
import useAxiosPrivate from "../../../Hooks/useAxiosPrivate";
import useUserContext from "../../../Hooks/useUserContext";
import GuidNote from "../../UI/InputNotes/GuidNote";

const GUID_REGEX =
  /^[0-9a-fA-F]{8}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{12}$/;

const AssignDeviceForm = () => {
  const userRef = useRef();
  const navigate = useNavigate();
  const axiosPrivate = useAxiosPrivate();
  const userCtx = useUserContext();

  const location = useLocation();
  const from = location.state?.from?.pathname || "/";

  const [isSubmitting, setIsSubmitting] = useState(false);
  const errRef = useRef();

  const [guid, setGuid] = useState("");
  const [validGuid, setValidGuid] = useState(false);
  const [guidFocus, setGuidFocus] = useState(false);

  const [errMsg, setErrMsg] = useState("");

  useEffect(() => {
    if (GUID_REGEX.test(guid)) {
      setValidGuid(true);
    } else {
      setValidGuid(false);
    }
  }, [guid]);

  const handleSubmit = async (e) => {
    e.preventDefault();
    setIsSubmitting(true);
    try {
      const response = await axiosPrivate.post(
        `DeviceAssign/${guid}/user/${userCtx.user.id}`,
        {},
        {
          headers: {
            "Content-Type": "application/json",
          },
          withCredentials: true,
        }
      );

      navigate(from, { replace: true });
    } catch (error) {
      console.log(error.response.data);
      if (!error?.response) {
        setErrMsg("No Server Response");
      } else {
        setErrMsg(error.response.data.error);
      }
    } finally {
      errRef.current.focus();
      setIsSubmitting(false);
    }
  };

  return (
    <>
      <section className={classes.assigmentContainer}>
        <p
          ref={errRef}
          className={errMsg ? "errmsg" : "offscreen"}
          aria-live="assertive"
        >
          {errMsg}
        </p>
        <CustomForm onSubmit={handleSubmit}>
          <Input
            type="text"
            label="Device id"
            name="DeviceId"
            id="GuidInput"
            ref={userRef}
            onChange={(e) => setGuid(e.target.value)}
            value={guid}
            required
            aria-invalid={validGuid ? "false" : "true"}
            aria-describedby="guidnote"
            onFocus={() => setGuidFocus(true)}
            onBlur={() => setGuidFocus(false)}
            validInput={validGuid}
          />
          <GuidNote
            isInputFocus={guidFocus}
            isInputValid={validGuid}
            currentInput={guid}
          />

          <Button element="button" disabled={isSubmitting || !validGuid}>
            {isSubmitting ? "Submitting" : "Register"}
          </Button>
        </CustomForm>
      </section>
    </>
  );
};

export default AssignDeviceForm;
