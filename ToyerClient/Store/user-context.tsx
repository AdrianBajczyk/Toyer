import { createContext } from "react";

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