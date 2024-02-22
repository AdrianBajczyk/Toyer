import React, { useState, useEffect,} from "react";

const Typewriter = ({ textArray, speed, onEnd=()=>{} }) => {
  const [currentText, setCurrentText] = useState("");
  const [currentIndex, setCurrentIndex] = useState(0);
  const [renderedTexts, setRenderedTexts] = useState([]);

  useEffect(() => {
    if (currentIndex < textArray.length) {
      const timer = setTimeout(() => {
        setCurrentText(
          textArray[currentIndex].substring(0, currentText.length + 1)
        );
      }, speed);

      if (currentText === textArray[currentIndex]) {
        setTimeout(() => {
          setRenderedTexts((prev) => [...prev, textArray[currentIndex]]);
          setCurrentIndex((prev) => prev + 1);
          setCurrentText("");
        }, 200);
      }

      return () => clearTimeout(timer);
    }

    if (renderedTexts.length === textArray.length) {
      onEnd(true)
    }
  }, [currentText, currentIndex, speed, textArray, renderedTexts]);

  return (
    <>
      {renderedTexts.map((text, index) => (
        <div key={index} style={{ textAlign: "center" }}>
          <h1>{text}</h1>
          <br />
        </div>
      ))}
      {currentIndex < textArray.length && (
        <h1 style={{ textAlign: "center" }}>{currentText}</h1>
      )}
    </>
  );
};

export default Typewriter;
