import { useState } from 'react';
import './LoginDarkMode.css';
import './LoginWhiteMode.css';
import UserService from '../../api/services/User';
import { useNavigate } from 'react-router-dom';
import { ToastContainer, toast } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';
import { useAuth } from '../../context/AuthContext';

const Login = () => {
  const navigate = useNavigate();
  const { setIsAuthenticated, setUser } = useAuth();
  const [isSignUpMode, setIsSignUpMode] = useState(false);
  const [isDarkMode, setIsDarkMode] = useState(true);
  const [formData, setFormData] = useState({
    email: '',
    password: '',
    name: ''
  });

  const handleInputChange = (e) => {
    setFormData({
      ...formData,
      [e.target.name]: e.target.value
    });
  };

  const toggleMode = () => {
    setIsSignUpMode(!isSignUpMode);
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

  const handleSignIn = async (e) => {
    e.preventDefault();
    try {
      const response = await UserService.login({
        email: formData.email,
        password: formData.password
      });
      if (response.status === 'success') {
        setIsAuthenticated(true);
        setUser(response.data);
        navigate('/home');
      }
    } catch (error) {
      showError(error.message || 'Login failed');
    }
  };

  const handleSignUp = async (e) => {
    e.preventDefault();
    try {
      const response = await UserService.addUser({
        email: formData.email,
        password: formData.password,
        name: formData.name
      });
      
      if (response.status === 'success') {
        await handleSignIn(e);
      }
    } catch (error) {
      showError(error.message || 'Registration failed');
    }
  };

  return (
    <div className={`login-container ${!isDarkMode ? 'light-mode' : ''}`}>
      <ToastContainer />
      <div className={`login-box ${isSignUpMode ? 'sign-up-mode' : ''} ${!isDarkMode ? 'light-mode' : ''}`}>
        <button 
          className="theme-toggle"
          onClick={toggleTheme}
          style={{ 
            background: isDarkMode ? '#f5f0ff' : '#520255',
            border: `2px solid ${isDarkMode ? '#520255' : '#f5f0ff'}`
          }}  
        >
          {isDarkMode ? '☀️' : '🌙'}
        </button>

        <div className="forms-container">
          <div className="signin-signup">
            <form className="sign-in-form form" onSubmit={handleSignIn}>
              <h2>Sign In</h2>
              <input 
                type="email" 
                name="email"
                placeholder="Email" 
                value={formData.email}
                onChange={handleInputChange}
                required 
              />
              <input 
                type="password"
                name="password" 
                placeholder="Password"
                value={formData.password}
                onChange={handleInputChange}
                required 
              />
              <button type="submit">Sign In</button>
            </form>

            <form className="sign-up-form form" onSubmit={handleSignUp}>
              <h2>Create Account</h2>
              <input 
                type="text"
                name="name" 
                placeholder="Name"
                value={formData.name}
                onChange={handleInputChange}
                required 
              />
              <input 
                type="email"
                name="email" 
                placeholder="Email"
                value={formData.email}
                onChange={handleInputChange}
                required 
              />
              <input 
                type="password"
                name="password" 
                placeholder="Password"
                value={formData.password}
                onChange={handleInputChange}
                required 
              />
              <button type="submit">Sign Up</button>
            </form>
          </div>
        </div>

        <div className="panels-container">
          <div className="panel left-panel">
            <h3>New Here?</h3>
            <p>Sign up and discover great opportunities!</p>
            <button onClick={toggleMode}>Sign Up</button>
          </div>
          <div className="panel right-panel">
            <h3>One of Us?</h3>
            <p>Sign in and continue your journey with us!</p>
            <button onClick={toggleMode}>Sign In</button>
          </div>
        </div>
      </div>
    </div>
  );
};

export default Login;
