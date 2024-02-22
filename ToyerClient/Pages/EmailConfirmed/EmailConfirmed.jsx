import Typewriter from "../../Utils/typewriter";
import classes from "./EmailConfirmed.module.css";

const EmailConfirmed = () => {
  const textArray = ["Success.", "Email Confirmed.", "You may login now."];
  const speed = 50;

  return (
    <>
      <div className={classes.textContainer}>
        <Typewriter textArray={textArray} speed={speed} />
      </div>
    </>
  );
};

export default EmailConfirmed;
