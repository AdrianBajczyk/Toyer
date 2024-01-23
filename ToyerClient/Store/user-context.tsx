import { type ReactNode, createContext, useContext, useReducer } from "react";

type User = {
    email: string;
    token: string;
    refreshToken: string;
};

type UserState = {
    isLoggedIn: boolean;
    user: User;
}

const initialState: UserState = {
    user: {email:"", token:"", refreshToken:""}, 
    isLoggedIn: false,}

type UserContextValue = UserState & {
    setUser: (userData: User) => void;
    logIn: () => void;
    logOut: () => void;
}

const UserContext = createContext<UserContextValue | null>(null);

export function useUserContext() {
    const userCtx = useContext(UserContext)

    if (userCtx === null){
        throw new Error("Fatal Error. Unexpected behavior. User context is null.")
    }

    return userCtx;
}

type UserContextProviderProps = {
    children: ReactNode;
};

type EnstablishUserAction = {
    type: 'ENSTABLISH_USER',
    payload: User
};

type LoginUserAction = {
    type: 'LOGIN_USER'
}

type LogoutUserAction = {
    type: 'LOGOUT_USER'
}

type Action = EnstablishUserAction | LoginUserAction | LogoutUserAction

function userReducer(state: UserState, action: Action) : UserState {
if (action.type === 'LOGIN_USER'){
    return {
        ...state,
        isLoggedIn: true
    }
} 
if (action.type === 'LOGOUT_USER'){
    return {
        ...state,
        isLoggedIn: false
    }
}
if (action.type === 'ENSTABLISH_USER'){
    return{
        ...state,
        user: {
            email: action.payload.email,
            token: action.payload.token,
            refreshToken: action.payload.refreshToken,
        }
    }
} else {
    return state;
}
}

export default function UserContextProvider({children}: UserContextProviderProps) {
    const [userState, dispatch] = useReducer(userReducer, initialState)

    const ctx: UserContextValue = {
        user: userState.user,
        isLoggedIn: userState.isLoggedIn,
        setUser(userData) {
            dispatch({type: 'ENSTABLISH_USER', payload: userData})
        },
        logIn() {
            dispatch({type: 'LOGIN_USER'})
        },
        logOut() {
            dispatch({type: 'LOGOUT_USER'})
        },
    };
return <UserContext.Provider value={ctx}>{children}</UserContext.Provider>
}