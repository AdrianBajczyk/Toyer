import CustomForm from "../UI/CustomForm.jsx";
import Input from "../UI/Input/Input.jsx";
import Button from "../UI/Button.jsx";
import { useState, useEffect, useRef } from "react";
import { isFuture, isMatch as isDateMatch } from "date-fns";
import { useActionData, useNavigation } from "react-router-dom";
import UserNote from "../UI/InputNotes/UserNote.jsx";
import PwdNote from "../UI/InputNotes/PwdNote.jsx";
import PwdConfirmNote from "../UI/InputNotes/PwdConfirmNote.jsx";
import PropNote from "../UI/InputNotes/PropNote.jsx";
import { isEarlierThan100YearsAgo } from "../../Utils/date.js";
import EmailNote from "../UI/InputNotes/EmailNote.jsx";
import NumNote from "../UI/InputNotes/NumNote.jsx";
import PostalNote from "../UI/InputNotes/PostalNote.jsx";
import PhoneNote from "../UI/InputNotes/PhoneNote.jsx";
import DateNote from "../UI/InputNotes/DateNote.jsx";

const USER_REGEX = /^[A-z][A-z0-9-_]{3,23}$/;
const PWD_REGEX = /^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#$%]).{8,24}$/;
const PROP_REGEX = /^[A-Z][a-z]{2,23}$/;
const EMAIL_REGEX = /^[\w\-\.]+@([\w-]+\.)+[a-z]{2,3}$/;
const DIGIT_REGEX = /^\d{1,4}$/;
const PHONE_REGEX = /^\(\+\d{2}\) \d{3}-\d{3}-\d{3}$/;
const POSTAL_REGEX = /^(?=(?:[^-]*-?[^-]*){0,1}$)[A-Z0-9-]{5,10}$/;

export function RegisterForm() {
  const navigation = useNavigation();
  const actionData = useActionData();

  const isSubmitting = navigation.state === "submitting";

  const userRef = useRef();
  const errRef = useRef();

  const [user, setUser] = useState("");
  const [validUser, setValidUser] = useState(false);
  const [userFocus, setUserFocus] = useState(false);

  const [pwd, setPwd] = useState("");
  const [validPwd, setValidPwd] = useState(false);
  const [pwdFocus, setPwdFocus] = useState(false);

  const [confirmPwd, setConfirmPwd] = useState("");
  const [validConfirm, setValidConfirm] = useState(false);
  const [confirmFocus, setConfirmFocus] = useState(false);

  const [name, setName] = useState("");
  const [validName, setValidName] = useState(false);
  const [nameFocus, setNameFocus] = useState(false);

  const [surname, setSurname] = useState("");
  const [validSurname, setValidSurname] = useState(false);
  const [surnameFocus, setSurnameFocus] = useState(false);

  const [email, setEmail] = useState("");
  const [validEmail, setValidEmail] = useState(false);
  const [emailFocus, setEmailFocus] = useState(false);

  const [birthDate, setBirthDate] = useState("");
  const [validBirthDate, setValidBirthDate] = useState(false);
  const [birthDateFocus, setBirthDateFocus] = useState(false);

  const [street, setStreet] = useState("");
  const [validStreet, setValidStreet] = useState(false);
  const [streetFocus, setStreetFocus] = useState(false);

  const [streetNum, setStreetNum] = useState("");
  const [validStreetNum, setValidStreetNum] = useState(false);
  const [streetNumFocus, setStreetNumFocus] = useState(false);

  const [unitNum, setUnitNum] = useState("");
  const [validUnitNum, setValidUnitNum] = useState(false);
  const [unitNumFocus, setUnitNumFocus] = useState(false);

  const [city, setCity] = useState("");
  const [validCity, setValidCity] = useState(false);
  const [cityFocus, setCityFocus] = useState(false);

  const [state, setState] = useState("");
  const [validState, setValidState] = useState(false);
  const [stateFocus, setStateFocus] = useState(false);

  const [postal, setPostal] = useState("");
  const [validPostal, setValidPostal] = useState(false);
  const [postalFocus, setPostalFocus] = useState(false);

  const [country, setCountry] = useState("");
  const [validCountry, setValidCountry] = useState(false);
  const [countryFocus, setCountryFocus] = useState(false);

  const [phone, setPhone] = useState("");
  const [validPhone, setValidPhone] = useState(false);
  const [phoneFocus, setPhoneFocus] = useState(false);

  const isSubmitBlocked =
    !validUser ||
    !validPwd ||
    !validConfirm ||
    !validName ||
    !validSurname ||
    !validEmail ||
    !validBirthDate ||
    !validStreet ||
    !validStreetNum ||
    !validCity ||
    !validPostal ||
    !validCountry;

  const [errMsg, setErrMsg] = useState("");

  useEffect(() => {
    userRef.current?.focus();
  }, []);

  useEffect(() => {
    setValidUser(USER_REGEX.test(user));
  }, [user]);

  useEffect(() => {
    setValidPwd(PWD_REGEX.test(pwd));
    setValidConfirm(pwd === confirmPwd && confirmPwd);
  }, [pwd, confirmPwd]);

  useEffect(() => {
    setValidName(PROP_REGEX.test(name));
  }, [name]);

  useEffect(() => {
    setValidSurname(PROP_REGEX.test(surname));
  }, [surname]);

  useEffect(() => {
    setValidEmail(EMAIL_REGEX.test(email));
  }, [email]);

  useEffect(() => {
    setValidEmail(EMAIL_REGEX.test(email));
  }, [email]);

  useEffect(() => {
    setValidBirthDate(
      isDateMatch(birthDate, "yyyy-MM-dd") &&
        !isFuture(birthDate) &&
        !isEarlierThan100YearsAgo(birthDate)
    );
  }, [birthDate]);

  useEffect(() => {
    setValidStreet(PROP_REGEX.test(street));
  }, [street]);

  useEffect(() => {
    setValidStreetNum(DIGIT_REGEX.test(streetNum));
  }, [streetNum]);

  useEffect(() => {
    setValidUnitNum(DIGIT_REGEX.test(unitNum) || !unitNum);
  }, [unitNum]);

  useEffect(() => {
    setValidCity(PROP_REGEX.test(city));
  }, [city]);

  useEffect(() => {
    setValidState(PROP_REGEX.test(state) || !state);
  }, [state]);

  useEffect(() => {
    setValidPostal(POSTAL_REGEX.test(postal));
  }, [postal]);

  useEffect(() => {
    setValidCountry(PROP_REGEX.test(country));
  }, [country]);

  useEffect(() => {
    setValidPhone(PHONE_REGEX.test(phone) || !phone);
  }, [phone]);

  useEffect(() => {
    setErrMsg("");
  }, [
    user,
    pwd,
    confirmPwd,
    name,
    surname,
    email,
    birthDate,
    street,
    streetNum,
    unitNum,
    city,
    state,
    postal,
    phone,
  ]);

  return (
    <>
      <section>
        <h1>Register</h1>
        <CustomForm className="FormClass" method="post">
          <Input
            type="text"
            label="Username:"
            name="UserName"
            id="UserNameInput"
            ref={userRef}
            onChange={(e) => setUser(e.target.value)}
            value={user}
            required
            aria-invalid={validUser ? "false" : "true"}
            aria-describedby="uidnote"
            onFocus={() => setUserFocus(true)}
            onBlur={() => setUserFocus(false)}
            validInput={validUser}
          />
          <UserNote
            isInputFocus={userFocus}
            isInputValid={validUser}
            currentInput={user}
          />
          <Input
            type="password"
            label="Password:"
            name="Password"
            id="PasswordInput"
            ref={userRef}
            onChange={(e) => setPwd(e.target.value)}
            value={pwd}
            required
            aria-invalid={validPwd ? "false" : "true"}
            aria-describedby="pwdnote"
            onFocus={() => setPwdFocus(true)}
            onBlur={() => setPwdFocus(false)}
            validInput={validPwd}
          />
          <PwdNote
            isInputFocus={pwdFocus}
            isInputValid={validPwd}
            currentInput={pwd}
          />
          <Input
            type="password"
            label="Confirm password:"
            name="ConfirmPassword"
            id="ConfirmPasswordInput"
            ref={userRef}
            onChange={(e) => setConfirmPwd(e.target.value)}
            value={confirmPwd}
            required
            aria-invalid={validConfirm ? "false" : "true"}
            aria-describedby="confirmnote"
            onFocus={() => setConfirmFocus(true)}
            onBlur={() => setConfirmFocus(false)}
            validInput={validConfirm}
          />
          <PwdConfirmNote
            isInputFocus={confirmFocus}
            isInputValid={validConfirm}
          />
          <Input
            type="text"
            label="Name"
            name="Name"
            id="NameInput"
            ref={userRef}
            onChange={(e) => setName(e.target.value)}
            value={name}
            required
            aria-invalid={validName ? "false" : "true"}
            aria-describedby="propnote"
            onFocus={() => setNameFocus(true)}
            onBlur={() => setNameFocus(false)}
            validInput={validName}
          />
          <PropNote
            isInputFocus={nameFocus}
            isInputValid={validName}
            currentInput={name}
          />
          <Input
            type="text"
            label="Surname"
            name="Surname"
            id="SurnameInput"
            ref={userRef}
            onChange={(e) => setSurname(e.target.value)}
            value={surname}
            required
            aria-invalid={validSurname ? "false" : "true"}
            aria-describedby="propnote"
            onFocus={() => setSurnameFocus(true)}
            onBlur={() => setSurnameFocus(false)}
            validInput={validSurname}
          />
          <PropNote
            isInputFocus={surnameFocus}
            isInputValid={validSurname}
            currentInput={surname}
          />
          <Input
            type="email"
            label="Email"
            name="Email"
            id="EmailInput"
            ref={userRef}
            onChange={(e) => setEmail(e.target.value)}
            value={email}
            required
            aria-invalid={validEmail ? "false" : "true"}
            aria-describedby="emailnote"
            onFocus={() => setEmailFocus(true)}
            onBlur={() => setEmailFocus(false)}
            validInput={validEmail}
          />
          <EmailNote
            isInputFocus={emailFocus}
            isInputValid={validEmail}
            currentInput={email}
          />
          <Input
            type="date"
            label="Birth date:"
            name="BirthDate"
            id="BirthDateInput"
            ref={userRef}
            onChange={(e) => setBirthDate(e.target.value)}
            value={birthDate}
            required
            aria-invalid={validBirthDate ? "false" : "true"}
            aria-describedby="datenote"
            onFocus={() => setBirthDateFocus(true)}
            onBlur={() => setBirthDateFocus(false)}
            validInput={validBirthDate}
          />
          <DateNote
            isInputFocus={birthDateFocus}
            isInputValid={validBirthDate}
            currentInput={birthDate}
          />
          <Input
            type="text"
            label="Street"
            name="Street"
            id="StreetInput"
            ref={userRef}
            onChange={(e) => setStreet(e.target.value)}
            value={street}
            required
            aria-invalid={validStreet ? "false" : "true"}
            aria-describedby="propnote"
            onFocus={() => setStreetFocus(true)}
            onBlur={() => setStreetFocus(false)}
            validInput={validStreet}
          />
          <PropNote
            isInputFocus={streetFocus}
            isInputValid={validStreet}
            currentInput={street}
          />
          <Input
            type="text"
            label="Street number"
            name="StreetNumber"
            id="StreetNumberInput"
            ref={userRef}
            onChange={(e) => setStreetNum(e.target.value)}
            value={streetNum}
            required
            aria-invalid={validStreetNum ? "false" : "true"}
            aria-describedby="numnote"
            onFocus={() => setStreetNumFocus(true)}
            onBlur={() => setStreetNumFocus(false)}
            validInput={validStreetNum}
          />
          <NumNote
            isInputFocus={streetNumFocus}
            isInputValid={validStreetNum}
            currentInput={streetNum}
          />
          <Input
            type="text"
            label="Unit number:"
            name="UnitNumber"
            id="UnitNumberInput"
            ref={userRef}
            onChange={(e) => setUnitNum(e.target.value)}
            value={unitNum}
            aria-invalid={validUnitNum ? "false" : "true"}
            aria-describedby="numnote"
            onFocus={() => setUnitNumFocus(true)}
            onBlur={() => setUnitNumFocus(false)}
            validInput={validUnitNum}
          />
          <NumNote
            isInputFocus={unitNumFocus}
            isInputValid={validUnitNum}
            currentInput={unitNum}
          />
          <Input
            type="text"
            label="City"
            name="City"
            id="CityInput"
            ref={userRef}
            onChange={(e) => setCity(e.target.value)}
            value={city}
            required
            aria-invalid={validCity ? "false" : "true"}
            aria-describedby="propnote"
            onFocus={() => setCityFocus(true)}
            onBlur={() => setCityFocus(false)}
            validInput={validCity}
          />
          <PropNote
            isInputFocus={cityFocus}
            isInputValid={validCity}
            currentInput={city}
          />
          <Input
            type="text"
            label="State"
            name="State"
            id="StateInput"
            ref={userRef}
            onChange={(e) => setState(e.target.value)}
            value={state}
            aria-invalid={validState ? "false" : "true"}
            aria-describedby="propnote"
            onFocus={() => setStateFocus(true)}
            onBlur={() => setStateFocus(false)}
            validInput={validState}
          />
          <PropNote
            isInputFocus={stateFocus}
            isInputValid={validState}
            currentInput={state}
          />
          <Input
            type="text"
            label="Postal code:"
            name="PostalCode"
            id="PostalCodeInput"
            ref={userRef}
            onChange={(e) => setPostal(e.target.value)}
            value={postal}
            required
            aria-invalid={validPostal ? "false" : "true"}
            aria-describedby="postalnote"
            onFocus={() => setPostalFocus(true)}
            onBlur={() => setPostalFocus(false)}
            validInput={validPostal}
          />
          <PostalNote
            isInputFocus={postalFocus}
            isInputValid={validPostal}
            currentInput={postal}
          />
          <Input
            type="text"
            label="Country:"
            name="Country"
            id="CountryInput"
            ref={userRef}
            onChange={(e) => setCountry(e.target.value)}
            value={country}
            required
            aria-invalid={validCountry ? "false" : "true"}
            aria-describedby="propnote"
            onFocus={() => setCountryFocus(true)}
            onBlur={() => setCountryFocus(false)}
            validInput={validCountry}
          />
          <PropNote
            isInputFocus={countryFocus}
            isInputValid={validCountry}
            currentInput={country}
          />
          <Input
            type="tel"
            label="Phone:"
            name="PhoneNumber"
            id="PhoneInput"
            ref={userRef}
            onChange={(e) => setPhone(e.target.value)}
            value={phone}
            aria-invalid={validPhone ? "false" : "true"}
            aria-describedby="phonenote"
            onFocus={() => setPhoneFocus(true)}
            onBlur={() => setPhoneFocus(false)}
            validInput={validPhone}
          />
          <PhoneNote
            isInputFocus={phoneFocus}
            isInputValid={validPhone}
            currentInput={phone}
          />
          <Button element="button" disabled={isSubmitting || isSubmitBlocked}>
            {isSubmitting ? "Submitting" : "Register"}
          </Button>
        </CustomForm>
        {actionData &&
          actionData.error &&
          typeof actionData.error === "object" && (
            <ul
              className={actionData.error ? "errmsg" : "offscreen"}
              aria-live="assertive"
            >
              {Object.entries(actionData.error).map(([key, value]) => (
                <li key={key}>
                  <h3>{key}</h3>
                  <p>{value}</p>
                </li>
              ))}
            </ul>
          )}
        {actionData &&
          actionData.error &&
          typeof actionData.error === "string" && (
            <p
              className={actionData.error ? "errmsg" : "offscreen"}
              aria-live="assertive"
            >
              {actionData.error}
            </p>
          )}
      </section>
    </>
  );
}

export default RegisterForm;
