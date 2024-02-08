import classes from "./AddressDataPage.module.css";
import { Link } from "react-router-dom";
import useAxiosPrivate from "../../../Hooks/useAxiosPrivate";
import useUserContext from "../../../Hooks/useUserContext";
import { useState, useEffect, useRef } from "react";
import List from "../../../Components/UI/List";
import { IonIcon } from "@ionic/react";
import { ellipsisHorizontal, checkmark, close } from "ionicons/icons";
import InputSelector from "../../../Components/ValidatedFormInputs/InputSelector";

const AddressDataPage = () => {
  const [profile, setProfile] = useState();
  const axiosPrivate = useAxiosPrivate();
  const userCtx = useUserContext();

  const [activeItemId, setActiveItemId] = useState(null);
  const [invalidItemId, setInvalidItemId] = useState(null);
  const [profileUpdated, setProfileUpdated] = useState(false);

  const userRef = useRef();

  useEffect(() => {
    let isMounted = true;

    const getProfile = async () => {
      try {
        const response = await axiosPrivate.get(
          `User/extended/${userCtx.user.id}`
        );
        isMounted && setProfile(response.data.userAddress);
      } catch (err) {
        console.log(err);
      }
    };

    getProfile();

    return () => {
      isMounted = false;
    };
  }, [profileUpdated]);

  const handleIconClick = (key) => {
    if (activeItemId === key) {
      setActiveItemId(null);
      setInvalidItemId(null);
    } else {
      setActiveItemId(key);
    }
  };

  const handleIconAccept = (key, value) => {
    const requestObject = {
      name: null,
      surname: null,
      birthDate: null,
      phoneNumber: null,
      email: null,
    };

    requestObject[key] = value;

    const updateProfile = async () => {
      try {
        const response = await axiosPrivate.put(
          `/User/Address/${userCtx.user.id}`,
          JSON.stringify(requestObject),
          {
            headers: { "Content-Type": "application/json" },
            withCredentials: true,
          }
        );
        setProfileUpdated((prev) => !prev);
      } catch (err) {
        console.log(err);
      }
    };

    updateProfile();
    setActiveItemId(null);
  };

  const handleValidityChange = (inputName, isValid) => {
    !isValid ? setInvalidItemId(inputName) : setInvalidItemId(null);
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
            →<p>Adrress</p>
          </div>
          <section className={classes.settingsContainer}>
            <h2 className={classes.settingsHeader}>Address Data</h2>
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
                      checkIcon={false}
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
                    className={
                      invalidItemId === convertCamelCaseToPascalCase(key)
                        ? classes.iconContainerNotAllowed
                        : activeItemId === key
                        ? classes.iconContainerAllowed
                        : classes.iconContainer
                    }
                    onClick={
                      activeItemId === key &&
                      !(invalidItemId === convertCamelCaseToPascalCase(key))
                        ? () => handleIconAccept(key, userRef.current.value)
                        : () => handleIconClick(key)
                    }
                  >
                    <IonIcon
                      icon={
                        invalidItemId === convertCamelCaseToPascalCase(key)
                          ? close
                          : activeItemId === key
                          ? checkmark
                          : ellipsisHorizontal
                      }
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
}

export default AddressDataPage

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
  let word = "";

  for (let i = 0; i < camelCaseString.length; i++) {
    if (i === 0) {
      word += camelCaseString[i].toUpperCase();
    } else {
      word += camelCaseString[i];
    }
  }
  return word;
}
