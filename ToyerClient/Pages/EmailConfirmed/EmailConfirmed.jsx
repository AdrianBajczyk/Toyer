import Typewriter from "../../Utils/typewriter";

const EmailConfirmed = () => {
  const textArray = ["Success.", "Email Confirmed."];
  const speed = 50;

  return (
    <div className={classes.wrapper}>
      <Typewriter textArray={textArray} speed={speed} />
    </div>
  );
};

export default EmailConfirmed;
