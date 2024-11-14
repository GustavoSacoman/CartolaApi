import { useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import './Profile.css';
import { useAuth } from '../../context/AuthContext';
import Sidebar from '../../components/Sidebar/Sidebar';
import defaultProfileIcon from '../../assets/Profile/default-profile-icon.svg';
import { Chart as ChartJS, ArcElement, Tooltip, Legend, CategoryScale, LinearScale, PointElement, LineElement } from 'chart.js';
import { Pie, Line } from 'react-chartjs-2';

// Register ChartJS components
ChartJS.register(
  ArcElement,
  Tooltip,
  Legend,
  CategoryScale,
  LinearScale,
  PointElement,
  LineElement
);

const Profile = () => {
  const { currentUser, isAuthenticated } = useAuth();
  const navigate = useNavigate();
  const [chartType, setChartType] = useState('pie');

  // Add authentication redirect

  // Mock data for charts (replace with real data from your backend)
  const betsData = {
    wins: 25,
    losses: 15,
    totalBets: 40,
    creditBalance: 1500,
  };

  // Add chart data
  const pieChartData = {
    labels: ['Wins', 'Losses'],
    datasets: [{
      data: [betsData.wins, betsData.losses],
      backgroundColor: ['#4caf50', '#f44336'],
      borderColor: ['#45a049', '#e53935'],
      borderWidth: 1,
    }],
  };

  const pieOptions = {
    plugins: {
      legend: {
        position: 'bottom',
      },
    },
    responsive: true,
    maintainAspectRatio: false,
  };

  // Add mock data for line chart
  const lineChartData = {
    labels: ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun'],
    datasets: [
      {
        label: 'Wins',
        data: [4, 6, 8, 3, 5, 7],
        borderColor: '#4caf50',
        backgroundColor: 'rgba(76, 175, 80, 0.5)',
        tension: 0.3,
      },
      {
        label: 'Losses',
        data: [2, 4, 3, 5, 2, 3],
        borderColor: '#f44336',
        backgroundColor: 'rgba(244, 67, 54, 0.5)',
        tension: 0.3,
      },
    ],
  };

  const lineOptions = {
    responsive: true,
    maintainAspectRatio: false,
    plugins: {
      legend: {
        position: 'bottom',
      },
    },
    scales: {
      y: {
        beginAtZero: true,
      },
    },
  };

  // Add this function to generate particles
  const renderParticles = () => {
    const particles = [];
    for (let i = 0; i < 20; i++) {
      const style = {
        left: `${Math.random() * 100}%`,
        top: `${Math.random() * 100}%`,
        animationDelay: `${Math.random() * 5}s`
      };
      particles.push(<div key={i} className="particle" style={style} />);
    }
    return particles;
  };

  return (
    <div className="profile-container">
      <div className="animated-background">
        {renderParticles()}
      </div>
      <Sidebar />
      <div className="profile-content">
        <div className="profile-header">
          <div className="profile-avatar">
            <img src={currentUser?.photoURL || defaultProfileIcon} alt="Profile" />
          </div>
          <h1>{currentUser?.displayName || 'User'}</h1>
        </div>

        <div className="profile-grid">
          <section className="profile-card">
            <h2>Personal Information</h2>
            <div className="card-content">
              <div className="info-item">
                <span>Email</span>
                <p>{currentUser?.email}</p>
              </div>
              <div className="info-item">
                <span>Member Since</span>
                <p>{currentUser?.metadata?.creationTime?.split('T')[0]}</p>
              </div>
              <div className="info-item">
                <span>Credit Balance</span>
                <p>${betsData.creditBalance}</p>
              </div>
            </div>
          </section>

          <section className="profile-card">
            <h2>Betting Statistics</h2>
            <div className="card-content">
              <div className="chart-controls">
                <button 
                  className={`chart-toggle-btn ${chartType === 'pie' ? 'active' : ''}`}
                  onClick={() => setChartType('pie')}
                >
                  Pie Chart
                </button>
                <button 
                  className={`chart-toggle-btn ${chartType === 'line' ? 'active' : ''}`}
                  onClick={() => setChartType('line')}
                >
                  Line Chart
                </button>
              </div>
              <div className="stats-grid">
                <div className="stat-item">
                  <span>Total Bets</span>
                  <h3>{betsData.totalBets}</h3>
                </div>
                <div className="stat-item">
                  <span>Wins</span>
                  <h3 className="wins">{betsData.wins}</h3>
                </div>
                <div className="stat-item">
                  <span>Losses</span>
                  <h3 className="losses">{betsData.losses}</h3>
                </div>
              </div>
              <div className="chart-container">
                {chartType === 'pie' ? (
                  <Pie data={pieChartData} options={pieOptions} />
                ) : (
                  <Line data={lineChartData} options={lineOptions} />
                )}
              </div>
            </div>
          </section>

          <section className="profile-card">
            <h2>Security Settings</h2>
            <div className="card-content">
              <button className="profile-button">Change Password</button>
              <button className="profile-button">Enable 2FA</button>
              <button className="profile-button">Manage Devices</button>
            </div>
          </section>
        </div>
      </div>
    </div>
  );
};

export default Profile;
