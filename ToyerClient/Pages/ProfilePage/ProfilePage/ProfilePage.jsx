import { Link, NavLink } from "react-router-dom";
import useAxiosPrivate from "../../../Hooks/useAxiosPrivate";
import useUserContext from "../../../Hooks/useUserContext";
import { useState, useEffect } from "react";
import classes from "./ProfilePage.module.css";

const ProfilePage = () => {
  const [profile, setProfile] = useState();
  const axiosPrivate = useAxiosPrivate();
  const userCtx = useUserContext();

  useEffect(() => {
    let isMounted = true;

    const getProfile = async () => {
      try {
        const response = await axiosPrivate.get(
          `User/extended/${userCtx.user.id}`
        );
        isMounted && setProfile(response.data);
      } catch (err) {
        console.log(err);
      }
    };

    getProfile();

    return () => {
      isMounted = false;
    };
  }, []);

  return (
    <>
      <div className={classes.subnaviagationContainer}>
        <Link id="subNavHome" to="/" className={classes.navLink} end>
          Home
        </Link>
        →<p>User</p>
      </div>
      <section className={classes.settingsContainer}>
        <h2 className={classes.settingsHeader}>Account Settings</h2>
        <div className={classes.settingsOptions}>
          <div className={classes.settingsUnit}>
            <div className={classes.settingsOptionsDescription}>
              <h3 className={classes.descriptionHeader}>Your Data</h3>
              <p className={classes.description}>
                Information about You and Your Account
              </p>
            </div>
            <Link to="personals">CHANGE</Link>
          </div>
          <div className={classes.settingsUnit}>
            <div className={classes.settingsOptionsDescription}>
              <h3 className={classes.descriptionHeader}>Shipping Addresses</h3>
              <p className={classes.description}>Manage Your Addresses</p>
            </div>
            <Link to="address">CHANGE</Link>
          </div>
        </div>
      </section>
    </>
  );
};

export default ProfilePage;
