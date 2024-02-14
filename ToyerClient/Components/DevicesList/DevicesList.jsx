import List from "../UI/List.jsx";
import { Link } from "react-router-dom";
import classes from "./DevicesList.module.css";
import { useState } from "react";

function DevicesList({ deviceTypes }) {
  return (
    <>
       {console.log(deviceTypes)}
        <section className={classes.cardsBox}>
        {deviceTypes.map((device) => (
            <div className={classes.cardContainer}>
              <div className={classes.card}>
                <span className={classes.cardFront} style={{ backgroundImage: `url(${device.imageUrl})` }}/>
                <span className={classes.cardBack}>
                  <h3>{device.name}</h3>
                  <p>{device.description}</p>
                </span>
              </div>
            </div>
            ))}
        </section>
          
        
    </>
  );
}

export default DevicesList;
