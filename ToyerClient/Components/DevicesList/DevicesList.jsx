import React, { useState } from "react";
import classes from "./DevicesList.module.css";
import DeviceDetials from "../../Pages/DeviceDetails/DeviceDetails";

const renderDelay = 700

function DevicesList({ deviceTypes }) {
  const [expandedCardId, setExpandedCardId] = useState(null);
  const [renderChildComponent, setRenderChildComponent] = useState(false);
  const [hoverAllowed, setHoverAllowed] = useState(true);

  const handleContainerHover = () => {
    if (hoverAllowed) {
      setHoverAllowed(false);
      setTimeout(() => {
        setHoverAllowed(true);
      }, renderDelay);
    }
  };

  const handleContainerClick = (deviceId) => {
    if (expandedCardId === deviceId) {
      setExpandedCardId(null);
      setRenderChildComponent(false); 
    } else if (hoverAllowed) {
      setExpandedCardId(deviceId);
      setTimeout(() => {
        setRenderChildComponent(true); 
      }, renderDelay);
    }
  };

  return (
    <>
      <section className={classes.cardsBox}>
        {deviceTypes.map((device, index) => {
          const isExpanded = expandedCardId === device.id;
          return (
            <div
              key={`deviceType-${device.id}`}
              className={`${classes.cardContainer} ${
                isExpanded ? classes.cardContainerExpanded : ""
              }`}
              onClick={() => handleContainerClick(device.id)}
              onMouseEnter={handleContainerHover}
              style={{
                order: isExpanded ? -1 : index,
                display: expandedCardId && !isExpanded ? "none" : null,
              }}
            >
              <div
                className={`${classes.card} ${
                  isExpanded ? classes.cardFlipped : ""
                }`}
              >
                <span
                  className={classes.cardFront}
                  style={{ backgroundImage: `url(${device.imageUrl})` }}
                />
                <span className={classes.cardBack}>
                  {!isExpanded ? <h3>{device.name}</h3> : renderChildComponent ? <DeviceDetials id={device.id} /> : <></>}
                </span>
              </div>
            </div>
          );
        })}
      </section>
    </>
  );
}

export default DevicesList;
