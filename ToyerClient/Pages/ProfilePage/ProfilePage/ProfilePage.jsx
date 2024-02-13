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
        <h2 className={classes.settingsHeader}>Settings</h2>
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
              <h3 className={classes.descriptionHeader}>Addresses</h3>
              <p className={classes.description}>
                Manage Your private and shipping addressess
              </p>
            </div>
            <Link to="address">CHANGE</Link>
          </div>
        </div>
      </section>
      <section className={classes.settingsContainer}>
        <h2 className={classes.settingsHeader}>Operations</h2>
        <div className={classes.settingsOptions}>
          <div className={classes.settingsUnit}>
            <div className={classes.settingsOptionsDescription}>
              <h3 className={classes.descriptionHeader}>Delete</h3>
              <p className={classes.description}>
                Unassigns all devices from current account and deletes it.
              </p>
            </div>
            <Link className={classes.warningLink} to="delete">
              PROCEED
            </Link>
          </div>
        </div>
      </section>
    </>
  );
};

export default ProfilePage;
