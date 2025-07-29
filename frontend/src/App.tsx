import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import Dashboard from "./Dashboard/dashboard";
import AuthGuard from "./lib/guards/AuthGuard";
import UnAuthGuard from "./lib/guards/UnAuthGuard";
import SignUpPage from "./pages/auth/signup";
import LoginPage from "./pages/auth/login";
import Home from "./pages/main/home";

import "./App.css";

function App() {
  return (
    <Router>
      <Routes>
        <Route path="/login" element={<LoginPage />} />
        <Route path="/sign-up" element={<SignUpPage />} />
        <Route path="/home" element={<Home />} />
        <Route
          key="dashboard"
          path="/"
          element={<AuthGuard component={<Dashboard />} />}
        />
      </Routes>
    </Router>
  );
}

export default App;
