import React from "react";
import "./App.css";
import { BrowserRouter, Route, Routes } from "react-router";
import Dashboard from "./Dashboard/dashboard";
import Placeholder from "./Placeholder/placeholder";
import AuthGuard from "./lib/guards/AuthGuard";
import UnAuthGuard from "./lib/guards/UnAuthGuard";

function App() {
  return (
    <BrowserRouter>
      <Routes>+
        <Route key="dashboard" path="/" element={<AuthGuard component={<Dashboard />}/>} />
        <Route key="placeholder" path="/auth" element={<UnAuthGuard component={<Placeholder />}/>} />
      </Routes>
    </BrowserRouter>
  );
}

export default App;
