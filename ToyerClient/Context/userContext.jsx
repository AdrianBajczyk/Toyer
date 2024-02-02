import { createContext, useContext, useReducer } from "react";

const initialState = {
  user: { email: "", token: "", refreshToken: "" },
  isLoggedIn: false,
};

export const UserContext = createContext(null);

function userReducer(state, action) {
  //switch case (lepsza wydajność - malutka, ale zawsze :D)(cukier składniowy)
  if (action.type === "LOGIN_USER") {
    return {
      ...state,
      isLoggedIn: true,
    };
  }
  if (action.type === "LOGOUT_USER") {
    return {
      ...state,
      isLoggedIn: false,
    };
  }
  if (action.type === "ENSTABLISH_USER") {
    return {
      ...state,
      user: {
        email: action.payload.email,
        id: action.payload.id,
        token: action.payload.token,
      },
    };
  }
  return state;
}

export default function UserContextProvider({ children }) {
  const [userState, dispatch] = useReducer(userReducer, initialState);

  const ctx = {
    user: userState.user,
    isLoggedIn: userState.isLoggedIn,
    setUser(userData) {
      dispatch({ type: "ENSTABLISH_USER", payload: userData });
    },
    logIn() {
      dispatch({ type: "LOGIN_USER" });
    },
    logOut() {
      dispatch({ type: "LOGOUT_USER" });
    },
  };
  return <UserContext.Provider value={ctx}>{children}</UserContext.Provider>;
}
