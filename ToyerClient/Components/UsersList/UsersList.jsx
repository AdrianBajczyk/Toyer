
import List from "../UI/List.jsx";



function UsersList({ users }) {
  return (
    <>
      <List
        items={users}
        renderItem={(user) => (
          <li key={"userKey"+ Math.random() + user.userName}>
            {user.userName}
          </li>
        )}
      />
    </>
  );
}

export default UsersList;
