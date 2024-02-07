import classes from "./PersonalDataPage.module.css";
import { Link } from "react-router-dom";
import useAxiosPrivate from "../../../Hooks/useAxiosPrivate";
import useUserContext from "../../../Hooks/useUserContext";
import { useState, useEffect, useRef } from "react";
import List from "../../../Components/UI/List";
import { IonIcon } from "@ionic/react";
import { ellipsisHorizontal, save } from "ionicons/icons";
import InputSelector from "../../../Components/ValidatedFormInputs/InputSelector";

const PersonalDataPage = () => {
  const [profile, setProfile] = useState();
  const axiosPrivate = useAxiosPrivate();
  const userCtx = useUserContext();

  const [activeItemId, setActiveItemId] = useState(null);
  const [isValid, setIsValid] = useState(false);

  const userRef = useRef();

  useEffect(() => {
    let isMounted = true;

    const getProfile = async () => {
      try {
        const response = await axiosPrivate.get(
          `User/extended/${userCtx.user.id}`
        );
        isMounted && setProfile(response.data.userPersonalInfo);
      } catch (err) {
        console.log(err);
      }
    };

    getProfile();

    return () => {
      isMounted = false;
    };
  }, []);

  const handleIconClick = (key) => {
    setActiveItemId(key);
  };

  const handleIconAccept = () => {
    console.log("pierogi");
  };

  const handleValidityChange = (inputName, isValid) => {
    setIsValid(isValid);
  };

  useEffect(() => {
    userRef.current?.focus();
  }, []);

  return (
    <>
      {profile && (
        <>
          <div className={classes.subnaviagationContainer}>
            <Link id="subNavHome" to="/" className={classes.navLink}>
              Home
            </Link>
            →
            <Link id="subNavHome" to="/profile" className={classes.navLink}>
              User
            </Link>
            →<p>Profile</p>
          </div>
          <section className={classes.settingsContainer}>
            <h2 className={classes.settingsHeader}>Account Personal Data</h2>
            <List
              items={Object.entries(profile)}
              className={classes.dataObjectsContainer}
              renderItem={([key, value]) => (
                <li
                  className={classes.profileDataObject}
                  id={`${key}${Math.random()}`}
                >
                  {activeItemId === key ? (
                    <InputSelector
                      name={convertCamelCaseToPascalCase(key)}
                      userRef={userRef}
                      onValidityChange={(inputName, isValid) =>
                        handleValidityChange(inputName, isValid)
                      }
                    />
                  ) : (
                    <>
                      <p className={classes.namePar}>
                        {convertCamelCaseToTitleCase(key)}:
                      </p>
                      <p>{value}</p>
                    </>
                  )}
                  <span
                    className={classes.iconContainer}
                    aria-label="userLogin"
                    onClick={
                      activeItemId === key
                        ? () => handleIconAccept()
                        : () => handleIconClick(key)
                    }
                  >
                    <IonIcon
                      icon={activeItemId === key ? save : ellipsisHorizontal}
                      size="medium"
                    ></IonIcon>
                  </span>
                </li>
              )}
            ></List>
          </section>
        </>
      )}
    </>
  );
};

export default PersonalDataPage;

function convertCamelCaseToTitleCase(camelCaseString) {
  let words = [];
  let currentWord = "";

  for (let i = 0; i < camelCaseString.length; i++) {
    const char = camelCaseString[i];
    if (char === char.toUpperCase()) {
      words.push(currentWord);
      currentWord = char;
    } else {
      currentWord += char;
    }
  }

  if (currentWord) {
    words.push(currentWord);
  }

  words = words.map((word) => word.charAt(0).toUpperCase() + word.slice(1));

  return words.join(" ");
}

function convertCamelCaseToPascalCase(camelCaseString) {
    let word= '';

    for (let i = 0; i < camelCaseString.length; i++) {
        
        if(i===0){
            word += camelCaseString[i].toUpperCase()
        } else {
            word += camelCaseString[i]
        }

    }
    return word
}