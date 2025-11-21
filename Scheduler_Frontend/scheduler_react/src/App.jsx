import React from "react";
import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import HomePage from "./pages/HomePage";
import SchedulePage from "./pages/SchedulePage";
import AboutPage from "./pages/AboutPage";
import ContactPage from "./pages/ContactPage";
import RadioNavbar from "./components/RadioNavbar";
import { ThemeProvider } from "./context/ThemeContext"; 
import DJPage from "./pages/DjPage";

function App() {
  return (
    
    <ThemeProvider>
      
      <Router>
       
        <RadioNavbar />
        <Routes>
         
          <Route path="/" element={<HomePage />} />
          <Route path="/dj" element={<DJPage />} />
          <Route path="/schedule" element={<SchedulePage />} />
           <Route path="/about" element={<AboutPage />} />
          <Route path="/contact" element={<ContactPage />} />
        </Routes>
      </Router>
    </ThemeProvider>
  );
}

export default App;