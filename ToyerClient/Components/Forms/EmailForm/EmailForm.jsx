import Button from "../../UI/Button";
import CustomForm from "../../UI/CustomForm";
import Input from "../../UI/Input/Input";
import EmailNote from "../../UI/InputNotes/EmailNote";
import classes from "./EmailForm.module.css";
import { useState, useEffect } from "react";
import { useActionData, useNavigation } from "react-router-dom";
import MessageNote from "../../UI/InputNotes/MessageNote";

const EMAIL_REGEX = /^[\w\-\.]+@([\w-]+\.)+[a-z]{2,3}$/;

const EmailForm = () => {
  const actionData = useActionData();
  const navigation = useNavigation();

  const isSubmitting = navigation.state === "submitting";

  const [email, setEmail] = useState("");
  const [validEmail, setValidEmail] = useState(false);
  const [emailFocus, setEmailFocus] = useState(false);

  const [message, setMessage] = useState("");
  const [validMessage, setValidMessage] = useState(false);
  const [messageFocus, setMessageFocus] = useState(false);

  useEffect(() => {
    setValidEmail(EMAIL_REGEX.test(email));
  }, [email]);

  useEffect(() => {
    setValidMessage(message.length > 20 && message.length < 500);
  }, [message]);

  const isSubmittBlocked = !validEmail || !validMessage;

  return (
    <>
      <section className={classes.emailFormContainer}>
        {actionData && actionData.error && typeof actionData.error === "string" && (
          <p
            className={actionData.error ? "errmsg" : "offscreen"}
            aria-live="assertive"
          >
            {actionData.error.Message}
          </p>
        )}
        <CustomForm method="post">
          <Input
            type="email"
            label="Email"
            name="contactEmail"
            id="EmailInput"
            onChange={(e) => setEmail(e.target.value)}
            value={email}
            required
            aria-invalid={validEmail ? "false" : "true"}
            aria-describedby="emailNote"
            onFocus={() => setEmailFocus(true)}
            onBlur={() => setEmailFocus(false)}
            validInput={validEmail}
          />
          <EmailNote
            isInputFocus={emailFocus}
            isInputValid={validEmail}
            currentInput={email}
          />
          <Input
            type="text"
            label="Message"
            name="message"
            id="MessageInput"
            onChange={(e) => setMessage(e.target.value)}
            value={message}
            required
            aria-invalid={validMessage ? "false" : "true"}
            aria-describedby="messageNote"
            onFocus={() => setMessageFocus(true)}
            onBlur={() => setMessageFocus(false)}
            validInput={validMessage}
            textarea={true}
          />
          <MessageNote
            isInputFocus={messageFocus}
            isInputValid={validMessage}
            currentInput={message}
          />

          <Button element="button" disabled={isSubmitting || isSubmittBlocked}>
            {isSubmitting ? "Sending..." : "Send"}
          </Button>
        </CustomForm>
      </section>
    </>
  );
};

export default EmailForm;
