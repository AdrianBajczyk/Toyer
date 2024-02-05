import { NavLink } from "react-router-dom";
import List from "../UI/List.jsx";
import classes from "./MainNavigation.module.css";

const links = [
  { id: "l1", to: "/", name: "Home" },
  { id: "l2", to: "/devices", name: "Devices" },
  { id: "l3", to: "/user", name: "User" },
  { id: "l4", to: "/login", name: "Login" },
];

function MainNavigation() {
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
      </nav>
    </header>
  );
}

export default MainNavigation;
