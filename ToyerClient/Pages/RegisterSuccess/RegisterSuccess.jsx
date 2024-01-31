import Button from "../../Components/UI/Button"

const RegisterSuccess = () => {
  return (
    <>
    <h1>Success. </h1>
    <h2>Email authorization link has been send.</h2>
    <h3>Use it before first login attempt.</h3>
    <Button element='link' to='/login'>Login</Button>
    </>
  )
}

export default RegisterSuccess