import React from "react";
import classes from "./OwnedDevicesNavigation.module.css";
import { NavLink } from "react-router-dom";

const OwnedDevicesNavigation = () => {
  return (
    <menu className={classes.menuContainer}>
      <li>
        <NavLink
          id='addLinkId'
          to='assigment'
          className={({ isActive }) => (isActive ? classes.active : undefined)}
          end
        >
          ADD
        </NavLink>
      </li>
    </menu>
  );
};

export default OwnedDevicesNavigation;
