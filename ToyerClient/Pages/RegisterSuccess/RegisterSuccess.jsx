import Typewriter from "../../Utils/typewriter";
import classes from './RegisterSuccess.module.css'

const RegisterSuccess = () => {

  const textArray = ["Success.", "Email authorization link has been send.", "Use it before first login attempt."];
  const speed = 50;

  return (
    <div className={classes.textContainer}>
    <Typewriter textArray={textArray} speed={speed}/>
    </div>
  )
}

export default RegisterSuccess