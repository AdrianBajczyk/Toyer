import { type ReactNode, createContext, useContext } from "react";

type User = {
    email: string;
    token: string;
    refreshToken: string;
};

type UserState = {
    isLoggedIn: boolean;
    user: User;
}

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

export default function UserContextProvider({children}: UserContextProviderProps) {
    const ctx: UserContextValue = {
        user: {email:"", token:"", refreshToken:""},
        isLoggedIn: false,
        setUser(userData) {
            //add logic later
        },
        logIn() {
            //add logic later
        },
        logOut() {
            //add logic later
        },
    };
return <UserContext.Provider value={ctx}>{children}</UserContext.Provider>
}