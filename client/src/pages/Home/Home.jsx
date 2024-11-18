import React, { useState, useEffect } from 'react';
import { Link } from 'react-router-dom';
import Sidebar from '../../components/Sidebar/Sidebar';
import './Home.css';
import PersonIcon from '@mui/icons-material/Person';
import SportsSoccerIcon from '@mui/icons-material/SportsSoccer';
import EmojiEventsIcon from '@mui/icons-material/EmojiEvents';
import TrendingUpIcon from '@mui/icons-material/TrendingUp';
import MatchService from '../../api/services/MatchService';
import { useAuth } from '../../context/AuthContext';
const Home = () => {
  const [topPlayers, setTopPlayers] = useState([]);
  const [upcomingMatches, setUpcomingMatches] = useState([]);
  const [weeklyHighScores, setWeeklyHighScores] = useState([]);
  const [isLoading, setIsLoading] = useState(true);
  const { user } = useAuth();

  useEffect(() => {
    const fetchData = async () => {
      try {
        const matchesData = await MatchService.getMatches();
        setUpcomingMatches(matchesData.slice(0, 5)); // Get first 5 matches

        // Mock data for now - replace with actual API calls
        setTopPlayers([
          { id: 1, name: "Player One", score: 2500, team: "Team Alpha" },
          { id: 2, name: "Player Two", score: 2300, team: "Team Beta" },
          { id: 3, name: "Player Three", score: 2100, team: "Team Gamma" },
          { id: 4, name: "Player Four", score: 2000, team: "Team Delta" },
          { id: 5, name: "Player Five", score: 1900, team: "Team Epsilon" },
        ]);

        setWeeklyHighScores([
          { id: 1, player: "Player One", score: 35, match: "Alpha vs Beta" },
          { id: 2, player: "Player Two", score: 32, match: "Gamma vs Delta" },
          { id: 3, player: "Player Three", score: 30, match: "Epsilon vs Zeta" },
        ]);

        setIsLoading(false);
      } catch (error) {
        console.error('Error fetching data:', error);
        setIsLoading(false);
      }
    };

    fetchData();
  }, []);

  return (
    <><Sidebar />
    <div className="home-container">
      <div className="home-content">
        <h1 className="welcome-title">{user ? `Welcome to Cartola, ${user.Name}` : 'Welcome to Cartola'}</h1>
        
        <div className="stats-grid">
          <div className="stat-card">
            <div className="stat-icon">
              <PersonIcon />
            </div>
            <div className="stat-info">
              <h3>Active Players</h3>
              <p>2,547</p>
            </div>
          </div>
          
          <div className="stat-card">
            <div className="stat-icon">
              <SportsSoccerIcon />
            </div>
            <div className="stat-info">
              <h3>Total Matches</h3>
              <p>1,234</p>
            </div>
          </div>
          
          <div className="stat-card">
            <div className="stat-icon">
              <EmojiEventsIcon />
            </div>
            <div className="stat-info">
              <h3>Tournaments</h3>
              <p>48</p>
            </div>
          </div>
          
          <div className="stat-card">
            <div className="stat-icon">
              <TrendingUpIcon />
            </div>
            <div className="stat-info">
              <h3>Total Bets</h3>
              <p>15,789</p>
            </div>
          </div>
        </div>

        <div className="dashboard-grid">
          <div className="dashboard-card top-players">
            <h2>Top Players</h2>
            <div className="player-list">
              {topPlayers.map((player, index) => (
                <div key={player.id} className="player-item">
                  <div className="player-rank">{index + 1}</div>
                  <div className="player-info">
                    <h3>{player.name}</h3>
                    <p>{player.team}</p>
                  </div>
                  <div className="player-score">{player.score}</div>
                </div>
              ))}
            </div>
          </div>

          <div className="dashboard-card upcoming-matches">
            <h2>Upcoming Matches</h2>
            <div className="matches-list">
              {upcomingMatches.map((match) => (
                <div key={match.idMatch} className="match-item">
                  <div className="match-teams">
                    <span>Team {match.idTeam1}</span>
                    <span className="vs">VS</span>
                    <span>Team {match.idTeam2}</span>
                  </div>
                  <div className="match-date">
                    {new Date(match.date).toLocaleDateString()}
                  </div>
                </div>
              ))}
            </div>
          </div>

          <div className="dashboard-card weekly-highscores">
            <h2>Weekly High Scores</h2>
            <div className="highscores-list">
              {weeklyHighScores.map((score) => (
                <div key={score.id} className="highscore-item">
                  <div className="highscore-info">
                    <h3>{score.player}</h3>
                    <p>{score.match}</p>
                  </div>
                  <div className="highscore-points">{score.score} pts</div>
                </div>
              ))}
            </div>
          </div>
        </div>
      </div>
    </div>
    </>
  );
};

export default Home;
