import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import SignUpPage from './pages/auth/signup';
import LoginPage from './pages/auth/login';
import Home from './pages/main/home';

function App() {
  return (
  <Router>
      <Routes>
        <Route path="/" element={<LoginPage/>} />
        <Route path="/sign-up" element={<SignUpPage/>} />
        <Route path="/home" element ={<Home/> } />
      </Routes>
  </Router>
  );
}

export default App;
