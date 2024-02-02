import { UserContext } from "../Context/userContext.jsx";
import { useContext } from "react";

export default function useUserContext() {
    const userCtx = useContext(UserContext);
  
    if (userCtx === null) {
      throw new Error("Fatal Error. Unexpected behavior. User context is null.");
    }
  
    return userCtx;
  }