import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import Dashboard from "./pages/Dashboard/dashboard";
import AuthGuard from "./lib/guards/AuthGuard";
import SignUpPage from "./pages/auth/signup";
import LoginPage from "./pages/auth/login";

import "./App.css";

function App() {
  return (
    <Router>
      <Routes>
        <Route path="/login" element={<LoginPage />} />
        <Route path="/sign-up" element={<SignUpPage />} />
        <Route
          key="dashboard"
          path="/dashboard"
          element={/*<AuthGuard component={*/<Dashboard/>} ///>/}
        />
        <Route
          key="Profile"
          path="/profile/:id"
          element={<AuthGuard component={<Dashboard />} />}
        />
      </Routes>
    </Router>
  );
}

export default App;
