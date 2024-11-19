import { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import UserService from '../../api/services/User';
import { ToastContainer, toast } from 'react-toastify';
import Sidebar from '../../components/Sidebar/Sidebar';
import 'react-toastify/dist/ReactToastify.css';
import './PasswordReset.css';

const PasswordReset = () => {
  const navigate = useNavigate();
  const [step, setStep] = useState(1);
  const [isDarkMode, setIsDarkMode] = useState(true);
  const [formData, setFormData] = useState({
    email: '',
    phone: '',
    newPassword: '',
    confirmPassword: ''
  });

  const handleInputChange = (e) => {
    setFormData({
      ...formData,
      [e.target.name]: e.target.value
    });
  };

  const toggleTheme = () => {
    setIsDarkMode(!isDarkMode);
  };

  const showError = (message) => {
    toast.error(message, {
      position: "top-right",
      autoClose: 3000,
      hideProgressBar: false,
      closeOnClick: true,
      pauseOnHover: false,
      draggable: true,
      progress: undefined,
      theme: isDarkMode ? "dark" : "light",
    });
  };

  const handleVerification = async (e) => {
    e.preventDefault();
    try {
      const response = await UserService.verifyPhone({
        email: formData.email,
        phone: formData.phone
      });
      
      if (response.status === 'success') {
        setStep(2);
      }
    } catch (error) {
      showError('Invalid email or phone number');
    }
  };

  const handlePasswordReset = async (e) => {
    e.preventDefault();
    
    if (formData.newPassword !== formData.confirmPassword) {
      showError('Passwords do not match');
      return;
    }

    try {
      const response = await UserService.resetPassword({
        email: formData.email,
        phone: formData.phone,
        newPassword: formData.newPassword
      });

      if (response.status === 'success') {
        toast.success('Password reset successful!');
        navigate('/login');
      }
    } catch (error) {
      showError('Failed to reset password');
    }
  };

  return (
    <>
      <Sidebar />
      <div className={`reset-container ${!isDarkMode ? 'light-mode' : ''}`}>
        <ToastContainer />
      <div className={`reset-box ${!isDarkMode ? 'light-mode' : ''}`}>
        <button
          className="theme-toggle"
          onClick={toggleTheme}
          style={{ 
            background: isDarkMode ? '#f5f0ff' : '#520255',
            border: `2px solid ${isDarkMode ? '#520255' : '#f5f0ff'}`,
            transition: 'all 0.7s ease-in-out',
            transform: isDarkMode ? 'rotate(360deg)' : 'rotate(0deg)'
          }}
        >
          {isDarkMode ? '☀️' : '🌙'}
        </button>

        {step === 1 ? (
          <form className="reset-form" onSubmit={handleVerification}>
            <div className='reset-form-container'>
              <h2>Reset Password</h2>
              <input
              className="reset-input"
              type="email"
              name="email"
              placeholder="Email"
              value={formData.email}
              onChange={handleInputChange}
              required
            />
            <input
              className="reset-input"
              type="tel"
              name="phone"
              placeholder="Phone Number"
              value={formData.phone}
              onChange={handleInputChange}
              required
              />
            </div>
            <div className='reset-form-buttons'>
              <button className="reset-button" type="submit">Verify</button>
              <button 
              className="reset-button" 
              type="button"
              onClick={() => navigate('/login')}
            >
                Back to Login
              </button>
            </div>
          </form>
        ) : (
          <form className="reset-form" onSubmit={handlePasswordReset}>
            <h2>New Password</h2>
            <input
              className="reset-input"
              type="password"
              name="newPassword"
              placeholder="New Password"
              value={formData.newPassword}
              onChange={handleInputChange}
              required
            />
            <input
              className="reset-input"
              type="password"
              name="confirmPassword"
              placeholder="Confirm Password"
              value={formData.confirmPassword}
              onChange={handleInputChange}
              required
            />
            <button className="reset-button" type="submit">Reset Password</button>
          </form>
        )}
      </div>
      </div>
    </>
  );
};

export default PasswordReset;
