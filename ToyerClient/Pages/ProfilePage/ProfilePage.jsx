import useAxiosPrivate from "../../Hooks/useAxiosPrivate";
import useUserContext from "../../Hooks/useUserContext";
import { useState, useEffect } from "react";

const ProfilePage = () => {

    const [profile, setProfile] = useState();
    const axiosPrivate = useAxiosPrivate();
    const userCtx = useUserContext();

  useEffect(() => {
    let isMounted = true;

    const getProfile = async () => {
      try {
        const response = await axiosPrivate.get(`User/extended/${userCtx.user.id}`);
        isMounted && setProfile(response.data);
      } catch (err) {
        console.log(err);
      }
    };

    getProfile();

    return () => {
      isMounted = false;
    };
  }, []);

  return <>{JSON.stringify(profile)}</>;
}

export default ProfilePage