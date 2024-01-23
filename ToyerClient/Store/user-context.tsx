import React from "react";
import { type ReactNode, createContext } from "react";

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