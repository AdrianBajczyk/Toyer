import useUserContext from "./useUserContext.js";
import axios from "../src/Api/axios.js";

const useRefreshToken = () => {
  const userCtx = useUserContext();
  const refresh = async () => {
    const response = await axios.post(
      "/Token",
      { Token: userCtx.user.token },
      {
        headers: { "Content-Type": "application/json" },
        withCredentials: true,
      }
    );
    userCtx.setUser({ token: response.data.token });

    return response.data.token;
  };

  return refresh;
};

export default useRefreshToken;
