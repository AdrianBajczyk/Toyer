import Typewriter from "../../Utils/typewriter";

const EmailConfirmed = () => {
  const textArray = ["Success.", "Email Confirmed.", "You may login now."];
  const speed = 50;

  return (
    <>
      <Typewriter textArray={textArray} speed={speed} />
    </>
  );
};

export default EmailConfirmed;
