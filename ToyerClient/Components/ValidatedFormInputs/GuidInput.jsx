import { useState, useEffect } from "react";
import Input from "../UI/Input/Input";
import GuidNote from "../UI/InputNotes/GuidNote";

const GUID_REGEX = /^[0-9a-fA-F]{8}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{12}$/

const GuidInput = ({ userRef, onValidityChange, ...props  }) => {
    const [guid, setGuid] = useState("");
    const [validGuid, setValidGuid] = useState(false);
    const [guidFocus, setGuidFocus] = useState(false);
  
    useEffect(() => {
      if (GUID_REGEX.test(guid)) {
        setValidGuid(true);
        onValidityChange("Guid", true);
      } else {
        setValidGuid(false);
        onValidityChange("Guid", false);
      }
    }, [guid]);



  return (
    <>
    <Input
      type="text"
      label='Device id'
      name='DeviceId'
      id='GuidInput'
      ref={userRef}
      onChange={(e) => setGuid(e.target.value)}
      value={guid}
      required={true}
      aria-invalid={validGuid ? "false" : "true"}
      aria-describedby="numnote"
      onFocus={() => setGuidFocus(true)}
      onBlur={() => setGuidFocus(false)}
      validInput={validGuid}
      {...props}
    />
    <GuidNote
      isInputFocus={guidFocus}
      isInputValid={validGuid}
      currentInput={guid}
    />
  </>
  )
}

export default GuidInput