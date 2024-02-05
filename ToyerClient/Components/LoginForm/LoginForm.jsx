import CustomForm from "../UI/CustomForm.jsx";
import Input from "../UI/Input/Input.jsx";
import Button from "../UI/Button.jsx";
import { useState, useEffect, useRef } from "react";
import axios from "../../src/Api/axios.js";
import useUserContext from "../../Hooks/useUserContext.js";
import { Link, useNavigate, useLocation } from "react-router-dom";

const EMAIL_REGEX = /^[\w\-\.]+@([\w-]+\.)+[a-z]{2,3}$/;

export default function LoginForm() {
  const navigate = useNavigate();
  const location = useLocation();
  const from = location.state?.from?.pathname || "/";

  const userCtx = useUserContext();
  const errRef = useRef();

  const [isSubmitting, setIsSubmitting] = useState(false);
  const [errMsg, setErrMsg] = useState("");

  const [email, setEmail] = useState("");
  const [validEmail, setValidEmail] = useState(false);

  const [password, setPassword] = useState("");
  const [emptyPwd, setNotEmptyPwd] = useState(true);

  useEffect(() => {
    setValidEmail(EMAIL_REGEX.test(email));
  }, [email]);

  useEffect(() => {
    setNotEmptyPwd(password.length > 0);
  }, [password]);

  const handleSubmit = async (e) => {
    e.preventDefault();
    setIsSubmitting(true);
    try {
      const response = await axios.post(
        "User/Login",
        JSON.stringify({ email, password }),
        {
          headers: { "Content-Type": "application/json" },
          withCredentials: true,
        }
      );
      console.log(JSON.stringify(response?.data));
      userCtx.setUser({
        email: email,
        id: response.data.id,
        token: response.data.token,
      });
      setEmail("");
      setPassword("");
      navigate(from, { replace: true });
    } catch (err) {
      console.log(err);
      userCtx.logOut();
      if (!err?.response) {
        setErrMsg("No Server Response");
      } else if (err.response?.status === 422) {
        setErrMsg("Missing Username or Password");
      } else if (err.response?.status === 401) {
        setErrMsg("Unauthorized");
      } else {
        setErrMsg("Login Failed");
      }
    } finally {
      errRef.current.focus();
      setIsSubmitting(false);
    }
  };

  const isSubmittBlocked = !validEmail || !emptyPwd;

  return (
    <section>
      <p
        ref={errRef}
        className={errMsg ? "errmsg" : "offscreen"}
        aria-live="assertive"
      >
        {errMsg}
      </p>
      <CustomForm onSubmit={handleSubmit}>
        <Input
          type="email"
          label="Email"
          name="email"
          id="EmailInput"
          onChange={(e) => setEmail(e.target.value)}
          value={email}
          required
          validInput={validEmail}
        />
        <Input
          type="password"
          label="Password"
          name="password"
          id="PasswordInput"
          onChange={(e) => setPassword(e.target.value)}
          value={password}
          required
          validInput={emptyPwd}
        />
        <Button element="button" disabled={isSubmittBlocked || isSubmitting}>
          {isSubmitting ? "Submitting..." : "Login"}
        </Button>
      </CustomForm>
      <Button element="link" to={"/register"} disabled={isSubmitting}>
        Create new user
      </Button>
    </section>
  );
}
