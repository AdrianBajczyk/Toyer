import { RouterProvider } from "react-router-dom";
import { router } from './RouterConstructor.jsx'
import ParticlesBackground from "../Components/ParticlesBackground.jsx";

function App() {


  return (
    <>
      <ParticlesBackground/>
      <RouterProvider router={router} />
    </>
  );
}

export default App;
