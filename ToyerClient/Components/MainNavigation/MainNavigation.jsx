import { NavLink } from "react-router-dom";
import List from "../UI/List.jsx";
import classes from "./MainNavigation.module.css";
import useUserContext from "../../Hooks/useUserContext.js";
import { IonIcon } from "@ionic/react";
import { peopleOutline } from "ionicons/icons";
import { useState } from "react";
import LoginPage from "../../Pages/LoginPage/LoginPage.jsx";

const links = [
  { id: "l1", to: "/", name: "Home" },
  { id: "l2", to: "/devices", name: "Devices" },
  { id: "l3", to: "/user", name: "User" },
  { id: "l5", to: "/spinner", name: "Spiner" },
];

function MainNavigation() {
  const userCtx = useUserContext();

  const [userIconActive, setUserIconActive] = useState(false);

  const handleIconClick = () => {
    setUserIconActive(true);
  };

  const handleHide = () => {
    setUserIconActive(false);
  };

  return (
    <header className={classes.header}>
      <nav>
        <List
          items={links}
          className={classes.list}
          renderItem={(link) => (
            <li key={link.id}>
              <NavLink
                id={link.id}
                to={link.to}
                className={({ isActive }) =>
                  isActive ? classes.active : undefined
                }
                end
              >
                {link.name}
              </NavLink>
            </li>
          )}
        />
        <div>
          <span
            className={classes.iconContainer}
            aria-label="userLogin"
            onClick={handleIconClick}
          >
            <IonIcon icon={peopleOutline} size="large"></IonIcon>
          </span>
          {userIconActive ? (
            <>
              <div
                className={classes.loginBackground}
                onClick={handleHide}
              ><span
              className={classes.iconContainer}
              aria-label="userLogin"
              onClick={handleIconClick}
            >
              <IonIcon icon={peopleOutline} size="large"></IonIcon>
            </span></div>
            <LoginPage onHide={handleHide}/>
            </>
          ) : (
            <></>
          )}
        </div>
      </nav>
    </header>
  );
}

export default MainNavigation;
