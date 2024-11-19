import React from 'react';
import { BrowserRouter as Router, Routes, Route, Navigate } from 'react-router-dom';
import Login from './pages/Login/Login';
import Home from './pages/Home/Home';
import Profile from './pages/Profile/Profile';
import PasswordReset from './pages/PasswordReset/PasswordReset';
import RegisterMatch from './pages/RegisterMatch/RegisterMatch';
import ListMatch from './pages/ListMatch/ListMatch';
import EditMatch from './pages/EditMatch/EditMatch';
import RegisterPlayer from './pages/Player/RegisterPlayerPage';
import Tournament from './pages/Tournament/Tournament';
import { AuthProvider } from './context/AuthContext';

function App() {
  return (
    <AuthProvider>
      <Router>
        <Routes>
          <Route 
            path="/" 
            element={<Home />}
          />
          <Route path="/login" element={<Login />} />
          <Route path="/profile" element={<Profile />} />
          <Route path="/matches" element={<RegisterMatch />} />
          <Route path="/password-reset" element={<PasswordReset />} />
          <Route path="/register-match" element={<RegisterMatch />} />
          <Route path="/list-match" element={<ListMatch />} />
          <Route path="/edit-match/:idMatch" element={<EditMatch />} />
          <Route path="/players" element={<RegisterPlayer />} />
          <Route path="/tournament" element={<Tournament />} />
        </Routes>
      </Router>
    </AuthProvider>
  );
}

export default App;
