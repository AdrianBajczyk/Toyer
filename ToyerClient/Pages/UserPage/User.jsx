import UsersList from "../../Components/UsersList/UsersList";
import useAxiosPrivate from "../../Hooks/useAxiosPrivate";
import { useState, useEffect } from "react";

const User = () => {
  const [users, setUsers] = useState();
  const axiosPrivate = useAxiosPrivate();


  useEffect(() => {
    let isMounted = true;

    const getUsers = async () => {
      try {
        const response = await axiosPrivate.get("/User");
        isMounted && setUsers(response.data);
      } catch (err) {
        console.log(err);
      }
    };

    getUsers();

    return () => {
      isMounted = false;
    };
  }, []);

  return users && <UsersList users={users} />;
};

export default User;
