import LoginForm from "../../Components/LoginForm/LoginForm.jsx";
import useLogout from "../../Hooks/useLogout.js";
import useUserContext from "../../Hooks/useUserContext.js";

export default function LoginPage() {

  const logout = useLogout();
  const userCtx = useUserContext();

  const handleLogout = async () =>{
    await logout();
  }

  return (
    <>
      <LoginForm>
      <button disabled={!userCtx.isLoggedIn} onClick={handleLogout}>Logout</button>
      </LoginForm>
    </>
  );
}
