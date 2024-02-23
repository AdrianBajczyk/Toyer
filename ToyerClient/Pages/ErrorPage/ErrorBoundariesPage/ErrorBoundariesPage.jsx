import classes from "./ErrorBoundariesPage.module.css";
import Typewriter from "../../../Utils/typewriter";
import { useState } from "react";

const speed = 70;

export const ErrorBoundariesPage = (props) => {
  const { error, resetErrorBoundary } = props;
  const [isTypingComplete, setIsTypingComplete] = useState();

  const handleTypingEnd = (isComplete) => {
    setIsTypingComplete(isComplete);
  };

  if (!error.respnse) {
    const textArray = [
      "500",
      "Connection error.",
      "No response. Server is offline. Try again later.",
    ];

    return (
        <div className={classes.errorContainer}>
        <Typewriter
          textArray={textArray}
          speed={speed}
          onEnd={handleTypingEnd}
        />
        {isTypingComplete ? (
          <button onClick={resetErrorBoundary}>reload</button>
        ) : (
          <></>
        )}
      </div>
    );
  }

  const textArray = [
    `${error.response.data.error}`,
    `${error.response.data.status}`,
    `${error.response.data.message}`,
  ];

  return (
    <div className={classes.errorContainer}>
      <Typewriter textArray={textArray} speed={speed} onEnd={handleTypingEnd} />
      {isTypingComplete ? (
        <button onClick={resetErrorBoundary}>reload</button>
      ) : (
        <></>
      )}
    </div>
  );
};
