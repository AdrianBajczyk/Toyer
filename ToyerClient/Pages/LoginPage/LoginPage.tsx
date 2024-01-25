import { useEffect, useState } from "react";
import { get, post } from "../../Utils/http.ts";

interface LoginPageProps {}

interface LoginPageResponse {
   "statusCode": string,
    "message": string,
    "token": string,
    "refreshToken": string,
}

export default function LoginPage(props: LoginPageProps) {
  const [loginRepsonse, setLoginResponse] = useState<LoginPageResponse>();
  const [isFetching, setIsFetching] = useState<boolean>(false);
  const [error, setError] = useState<string>();

  useEffect(() => {
    async function fetchLogin() {
      setIsFetching(true);
      try{
        const responseData = await get<LoginPageResponse>(
          `https://localhost:7065/api/User`
        );
  
        console.log(responseData)
  
        setLoginResponse(responseData);
      } catch(error) {
        if (error instanceof Error)
        {
          setError(error.message)
        }
      }
      
      setIsFetching(false);
    }

    fetchLogin();
  }, []);

  return <>
  <h1>LoginPage</h1>
  </>;
}
