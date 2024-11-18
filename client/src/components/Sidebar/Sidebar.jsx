import React, { useState } from 'react';
import './Sidebar.css';
import HomeIcon from '@mui/icons-material/Home';
import GroupIcon from '@mui/icons-material/Group';
import EmojiEventsIcon from '@mui/icons-material/EmojiEvents';
import SportsSoccerIcon from '@mui/icons-material/SportsSoccer';
import PersonIcon from '@mui/icons-material/Person';
import SportsKabaddiIcon from '@mui/icons-material/SportsKabaddi';
import ExitToAppIcon from '@mui/icons-material/ExitToApp';
import { useAuth } from '../../context/AuthContext';

const Sidebar = () => {
  const [isDarkMode, setIsDarkMode] = useState(true);
  const { isAuthenticated, logout } = useAuth();

  const handleAuthClick = () => {
    if (isAuthenticated) {
      logout();
      window.location.href = '/login';
    } else {
      window.location.href = '/login';
    }
  };

  return (
    <div className={`sidebar ${!isDarkMode ? 'light-mode' : ''}`}>
      {/* Logo Section */}
      <div className="logo-section">
        <h1>LOGO</h1>
      </div>

      {}
      <div className="nav-links">
        <a href="/" className="nav-link">
          <HomeIcon />
          <span>Home</span>
        </a>
        <a href="/teams" className="nav-link">
          <GroupIcon />
          <span>Teams</span>
        </a>
        <a href="/tournaments" className="nav-link">
          <EmojiEventsIcon />
          <span>Tournaments</span>
        </a>
        <a href="/matches" className="nav-link">
          <SportsSoccerIcon />
          <span>Matches</span>
        </a>
        <a href="/players" className="nav-link">
          <SportsKabaddiIcon />
          <span>Players</span>
        </a>
        <a href="/profile" className="nav-link">
          <PersonIcon />
          <span>Profile</span>
        </a>
      </div>

      {/* Bottom Section */}
      <div className="bottom-section">
        <button className={isAuthenticated ? 'logout-button' : 'login-button'} onClick={handleAuthClick}>
          <ExitToAppIcon />
          <span>{isAuthenticated ? 'Logout' : 'Sign In'}</span>
        </button>
      </div>
    </div>
  );
};

export default Sidebar;

