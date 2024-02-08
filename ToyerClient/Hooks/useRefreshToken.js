import useUserContext from "./useUserContext.js";
import axios from "../src/Api/axios.js";

const useRefreshToken = () => {
  const userCtx = useUserContext();
  const refresh = async () => {
    console.log("useRefresh")
    const response = await axios.post(
      "/Token",
      { Token: userCtx.user.token },
      {
        headers: { "Content-Type": "application/json" },
        withCredentials: true,
      }
    );
    const updatedUser = {
      ...userCtx.user,
      token: response.data.token,
    };
    userCtx.setUser(updatedUser);

    return response.data.token;
  };

  return refresh;
};

export default useRefreshToken;
