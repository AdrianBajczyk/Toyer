import { NavLink, useNavigate, useNavigation } from "react-router-dom";
import List from "../../UI/List.jsx";
import classes from "./MainNavigation.module.css";
import useUserContext from "../../../Hooks/useUserContext.js";
import { IonIcon } from "@ionic/react";
import { peopleOutline, people } from "ionicons/icons";
import { useState } from "react";
import LoginWindow from "../../Windows/LoginWindow/LoginWindow.jsx";
import ToyerLogo from "../../../src/Asserts/ToyerLogo.jsx";

const links = [
  { id: "l1", to: "devices", name: "Offer" },
  // { id: "l2", to: "devices", name: "Author" },
   { id: "l2", to: "contact", name: "Contact" },
  {
    id: "lu1",
    to: "ownedDevices",
    name: "Your devices",
    requiredRoles: import.meta.env.VITE_USER_ROLE,
  },
  {
    id: "le1",
    to: "user",
    name: "Users",
    requiredRoles: import.meta.env.VITE_EMPLOYEE_ROLE,
  },
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
            nav("/");
          }}
        >
          <ToyerLogo />
        </span>
        <List
          items={links}
          className={classes.list}
          renderItem={(link) =>
            (!link.requiredRoles ||
              userCtx?.user?.roles?.some((role) =>
                link.requiredRoles.includes(role)
              )) && (
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
            )
          }
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
              <LoginWindow onHide={handleHide} />
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
