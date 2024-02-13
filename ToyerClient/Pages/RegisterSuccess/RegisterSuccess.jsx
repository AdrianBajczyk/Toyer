import Typewriter from "../../Utils/typewriter"

const RegisterSuccess = () => {

  const textArray = ["Success.", "Email authorization link has been send.", "Use it before first login attempt."];
  const speed = 50;

  return (
    <>
    <Typewriter textArray={textArray} speed={speed}/>
    </>
  )
}

export default RegisterSuccess