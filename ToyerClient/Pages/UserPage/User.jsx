import { useLoaderData } from "react-router-dom";
import UsersList from "../../Components/UsersList/UsersList";
import {get} from "../../Utils/http.js"


const User = () => {

    const users = useLoaderData();
    console.log(users)

  return (
    <UsersList users={users}/>
  )
}

export default User

export async function loader() {
    return await get(`User`);
  
  }