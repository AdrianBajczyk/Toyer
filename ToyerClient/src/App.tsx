import { BrowserRouter, Route, Routes } from "react-router-dom";
import LoginPage from "../Pages/LoginPage/LoginPage.tsx";
import HomePage from "../Pages/HomePage/HomePage.tsx";
import DevicePage from "../Pages/DevicePage/DevicePage.tsx";

function App() {
  return (
    <div className="App">
      <BrowserRouter>


        <Routes>
          <Route path="/" element={<HomePage />} />
          <Route path="/login" element={<LoginPage />} />
          <Route path="/device/:id" element={<DevicePage />} />
        </Routes>

        
      </BrowserRouter>
    </div>
  );
}
export default App;
