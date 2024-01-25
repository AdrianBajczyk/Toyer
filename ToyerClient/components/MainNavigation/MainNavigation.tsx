import {  NavLink } from "react-router-dom";
import { List } from "../List.tsx";
import classes from "./MainNavigation.module.css";

const links = [
  { id: "l1", to: "/", name: "Home" },
  { id: "l2", to: "/login", name: "Login" },
];

function MainNavigation() {
  return (
    <header className={classes.header}>
      <nav>
        <List
          items={links}
          className={classes.list}
          renderItem={(link) => (
            <li>
              <NavLink id={link.id} to={link.to} className={({isActive})=>isActive? classes.active : undefined}end>
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
