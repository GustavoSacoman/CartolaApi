import { useState } from 'react';
import './LoginDarkMode.css';
import './LoginWhiteMode.css';

const Login = () => {
  const [isSignUpMode, setIsSignUpMode] = useState(false);
  const [isDarkMode, setIsDarkMode] = useState(true);

  const toggleMode = () => {
    setIsSignUpMode(!isSignUpMode);
  };

  const toggleTheme = () => {
    setIsDarkMode(!isDarkMode);
  };

  const handleSignIn = (e) => {
    e.preventDefault();
    // Add sign in logic here
  };

  const handleSignUp = (e) => {
    e.preventDefault();
    // Add sign up logic here
  };

  return (
    <div className={`login-container ${!isDarkMode ? 'light-mode' : ''}`}>
      <div className={`login-box ${isSignUpMode ? 'sign-up-mode' : ''} ${!isDarkMode ? 'light-mode' : ''}`}>
        <button 
          className="theme-toggle"
          onClick={toggleTheme}
          style={{
            position: 'absolute',
            top: '20px',
            right: '20px',
            zIndex: 10,
            width: '40px',
            height: '40px',
            borderRadius: '50%',
            padding: 0,
            fontSize: '20px',
            background: isDarkMode ? '#f5f0ff' : '#520255',
            border: `2px solid ${isDarkMode ? '#520255' : '#f5f0ff'}`,
            cursor: 'pointer',
            display: 'flex',
            alignItems: 'center',
            justifyContent: 'center',
            boxShadow: '0 2px 5px rgba(0,0,0,0.2)'
          }}
        >
          {isDarkMode ? '☀️' : '🌙'}
        </button>

        <div className="forms-container">
          <div className="signin-signup">
            <form className="sign-in-form form" onSubmit={handleSignIn}>
              <h2>Sign In</h2>
              <input type="email" placeholder="Email" required />
              <input type="password" placeholder="Password" required />
              <button type="submit">Sign In</button>
            </form>

            <form className="sign-up-form form" onSubmit={handleSignUp}>
              <h2>Create Account</h2>
              <input type="text" placeholder="Name" required />
              <input type="email" placeholder="Email" required />
              <input type="password" placeholder="Password" required />
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
