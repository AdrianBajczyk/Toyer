import LoginForm from "../../Forms/LoginForm/LoginForm.jsx";
import useLogout from "../../../Hooks/useLogout.js";
import useUserContext from "../../../Hooks/useUserContext.js";
import Button from "../../UI/Button.jsx";

export default function LoginWindow({ onHide }) {
  const logout = useLogout();
  const userCtx = useUserContext();

  const handleLogout = async () => {
    await logout();
    onHide();
  };

  return (
    <>
      <LoginForm onHide={onHide}>
        {userCtx.isLoggedIn ? (
          <>
            <Button onClick={() => onHide()} element="link" to={"/profile"}>
              Profile
            </Button>
            <Button onClick={handleLogout}>Logout</Button>
          </>
        ) : (
          <></>
        )}
      </LoginForm>
    </>
  );
}
