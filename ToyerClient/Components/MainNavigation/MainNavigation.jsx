import { NavLink, useNavigate } from "react-router-dom";
import List from "../UI/List.jsx";
import classes from "./MainNavigation.module.css";
import useUserContext from "../../Hooks/useUserContext.js";
import { IonIcon } from "@ionic/react";
import { peopleOutline, people } from "ionicons/icons";
import { useState } from "react";
import LoginPage from "../../Pages/LoginPage/LoginPage.jsx";
import ToyerLogo from "../../src/Asserts/ToyerLogo.jsx";

const links = [
  { id: "l2", to: "/devices", name: "Devices" },
  { id: "l3", to: "/user", name: "User", requiredRoles: import.meta.env.VITE_EMPLOYEE_ROLE },
  { id: "l5", to: "/spinner", name: "Spiner" },
  { id: "l6", to: "/", name: "Contact" },
];

function MainNavigation() {
  const userCtx = useUserContext();
  const nav = useNavigate();
  const [userIconActive, setUserIconActive] = useState(false);

  const handleIconClick = () => {
    setUserIconActive(true);
  };

  const handleHide = () => {
    setUserIconActive(false);
  };

  const [icon, setIcon] = useState(peopleOutline);
  const handleMouseEnter = () => {
    setIcon(people);
  };

  const handleMouseLeave = () => {
    setIcon(peopleOutline);
  };

  return (
    <header className={classes.header}>
      <nav>
        <span
          className={classes.logo}
          onClick={() => {
            nav("");
          }}
        >
          <ToyerLogo />
        </span>
        <List
          items={links}
          className={classes.list}
          renderItem={(link) => (
            (!link.requiredRoles || userCtx?.user?.roles?.some(role => link.requiredRoles.includes(role))) && (
              <li key={link.id}>
                <NavLink
                  id={link.id}
                  to={link.to}
                  className={({ isActive }) => (isActive ? classes.active : undefined)}
                  end
                >
                  {link.name}
                </NavLink>
              </li>
            )
          )}
        />
        <div>
          <IonIcon
            className={classes.iconContainer}
            aria-label="userLogin"
            onClick={handleIconClick}
            onMouseEnter={handleMouseEnter}
            onMouseLeave={handleMouseLeave}
            icon={icon}
            size="large"
          ></IonIcon>

          {userIconActive ? (
            <>
              <div
                className={classes.loginBackground}
                onClick={handleHide}
              ></div>
              <LoginPage onHide={handleHide} />
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
