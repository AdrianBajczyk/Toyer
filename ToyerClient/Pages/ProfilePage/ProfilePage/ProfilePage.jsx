import { Link } from "react-router-dom";
import classes from "./ProfilePage.module.css";

const ProfilePage = () => {
  return (
    <>
      <div className={classes.subnaviagationContainer}>
        <Link id="subNavHome" to="/" className={classes.navLink}>
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
