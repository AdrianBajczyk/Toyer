import Typewriter from "../../Utils/typewriter";
import classes from "./EmailSent.module.css";

const EmailSent = () => {
  const speed = 70;
  const textArray = ["Success.", "Your email has been sent."];

  return (
    <div className={classes.emailSendContainer}>
      <Typewriter textArray={textArray} speed={speed} />
    </div>
  );
};

export default EmailSent;
