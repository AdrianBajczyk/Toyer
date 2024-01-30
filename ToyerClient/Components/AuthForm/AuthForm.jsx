import CustomForm from "../UI/CustomForm.jsx";
import Input from "../UI/Input/Input.jsx";
import Button from "../UI/Button.jsx"

export default function AuthForm() {
  return (
    <CustomForm>
      <Input type="email" label="Email" name="Email" id="EmailInput" />
      <Input
        type="password"
        label="Password"
        name="Password"
        id="PasswordInput"
      />
      <Button element='link' to={'/register'}>Register</Button>
      <Button element='button' >Login</Button>
    </CustomForm>
  );
}
