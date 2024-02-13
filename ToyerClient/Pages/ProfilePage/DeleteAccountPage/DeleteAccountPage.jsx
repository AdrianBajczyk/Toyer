import Typewriter from "../../../Utils/typewriter";
import { useState } from "react";
import Button from "../../../Components/UI/Button";
import { Link } from "react-router-dom";
import classes from "./DeleteAccountPage.module.css";
import { useNavigate } from "react-router-dom";
import useAxiosPrivate from "../../../Hooks/useAxiosPrivate";
import useUserContext from "../../../Hooks/useUserContext";

const textArray = [
  "Warning.",
  "You are about to delete your account.",
  "Any stored data will be lost.",
];
const speed = 50;

const DeleteAccountPage = () => {
  const userCtx = useUserContext();
  const axiosPrivate = useAxiosPrivate();
  const nav = useNavigate();
  const [renderButton, setRenderButton] = useState(false);

  const handleEnd = (isEnd) => {
    isEnd ? setRenderButton(true) : null;
  };

  const handleDelete = () => {
    const deleteUser = async () => {
      try {
        await axiosPrivate.delete(`User/${userCtx.user.id}`, {
          headers: { "Content-Type": "application/json" },
          withCredentials: true,
        });
        
      } catch (err) {
        console.log(err);
      } finally {
        nav("/");
        userCtx.logOut();
      }

      
    };

    deleteUser();
  };

  return (
    <>
      <div className={classes.subnaviagationContainer}>
        <Link id="subNavHome" to="/" className={classes.navLink}>
          Home
        </Link>
        →
        <Link id="subNavHome" to="/profile" className={classes.navLink}>
          User
        </Link>
        →<p>Delete</p>
      </div>
      <div className={classes.messageContainer}>
        <Typewriter textArray={textArray} speed={speed} onEnd={handleEnd} />
      </div>
      {renderButton ? (
        <div>
          <Button onClick={handleDelete}>Delete</Button>
          <Button onClick={() => nav("/profile")}>Back</Button>
        </div>
      ) : (
        <></>
      )}
    </>
  );
};

export default DeleteAccountPage;
