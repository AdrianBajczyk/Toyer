import MainNavigation from "../../Components/MainNavigation/MainNavigation.tsx"

type ErrorPageProps = {
    message:string;
}

function ErrorPage({message} : ErrorPageProps){
return <>
<MainNavigation/>
<main>
    <h2>Error</h2>
    <p>{message}</p>
</main>
</>
}

export default ErrorPage;