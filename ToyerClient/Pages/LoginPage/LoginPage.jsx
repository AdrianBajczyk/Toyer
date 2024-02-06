import LoginForm from "../../Components/LoginForm/LoginForm.jsx";
import useLogout from "../../Hooks/useLogout.js";
import useUserContext from "../../Hooks/useUserContext.js";
import Button from "../../Components/UI/Button.jsx";

export default function LoginPage({onHide}) {

  const logout = useLogout();
  const userCtx = useUserContext();

  const handleLogout = async () =>{
    await logout();
    onHide();
  }

  return (
    <>
      <LoginForm onHide={onHide}>
        {userCtx.isLoggedIn ? <Button onClick={handleLogout}>Logout</Button> : <></>}
      </LoginForm>
    </>
  );
}
