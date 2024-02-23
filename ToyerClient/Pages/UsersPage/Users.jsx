import Spinner from "../../Components/Spinner/Spinner";
import useAxiosPrivate from "../../Hooks/useAxiosPrivate";
import { useState, useEffect, useRef } from "react";
import { useErrorBoundary } from "react-error-boundary";
import classes from './Users.module.css';

const Users = () => {
  const [users, setUsers] = useState();
  const axiosPrivate = useAxiosPrivate();
  const [isLoading, setIsLoding] = useState(false);

  const abortControllerRef = useRef();

  const { showBoundary } = useErrorBoundary();

  useEffect(() => {
    abortControllerRef.current = new AbortController();
    setIsLoding(true);

    const getUsers = async () => {
      try {
        const response = await axiosPrivate.get(
          "/User",
          {},
          {
            headers: {
              "Content-Type": "application/json",
            },
            signal: abortControllerRef.current?.signal,
            withCredentials: true,
          }
        );
        setUsers(response.data);
      } catch (error) {
        showBoundary(error);
      } finally {
        setIsLoding(false);
      }
    };

    getUsers();

    return () => {
      abortControllerRef.current?.abort();
    };
  }, []);

  return isLoading ? (
    <Spinner width={600} height={600} />
  ) : users ? (
    <div className={classes.tableContainer}>
      <table>
        <tr>
          <th>UserName</th>
          <th>Id</th>
        </tr>
        {users.map((user) => {
          return (
            <tr key={`user-${user.id}`}>
              <td>{user.userName}</td>
              <td>{user.id}</td>
            </tr>
          );
        })}
      </table>
    </div>
  ) : <></>;
};

export default Users;
