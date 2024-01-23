import { BrowserRouter, Route, Routes } from "react-router-dom";
import LoginPage from "../Pages/LoginPage/LoginPage.tsx";
import HomePage from "../Pages/HomePage/HomePage.tsx";
import DevicePage from "../Pages/DevicePage/DevicePage.tsx";
import UserContextProvider from "../Store/user-context.tsx"

function App() {
  return (
    <UserContextProvider>
      <div className="App">
      <BrowserRouter>


        <Routes>
          <Route path="/" element={<HomePage />} />
          <Route path="/login" element={<LoginPage />} />
          <Route path="/device/:id" element={<DevicePage />} />
        </Routes>


      </BrowserRouter>
    </div>
    </UserContextProvider>
    
  );
}
export default App;
