import axios from "../src/Api/axios";
import useUserContext from "./useUserContext";

const useLogout = () => {
  const userCtx = useUserContext();

  const logout = async () => {
    try {
      const response = await axios.post(
        "/Token/End",
        {},
        {
          withCredentials: true,
        }
      );
    } catch (err) {
      console.error(err.response);
    } finally {
      userCtx.logOut();
    }
  };

  return logout;
};

export default useLogout;
