import React, { useState, useEffect } from 'react';
import { Navigate } from 'react-router-dom';
import './Profile.css';
import Sidebar from '../../components/Sidebar/Sidebar';
import { useAuth } from '../../context/AuthContext';
import PersonIcon from '@mui/icons-material/Person';
import EditIcon from '@mui/icons-material/Edit';
import SaveIcon from '@mui/icons-material/Save';
import CloseIcon from '@mui/icons-material/Close';
import { ToastContainer, toast } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';
import UserService from '../../api/services/User';

const Profile = () => {
  const { user, setUser, isAuthenticated } = useAuth();
  const [isEditing, setIsEditing] = useState(false);
  const [isLoading, setIsLoading] = useState(true);
  const [formData, setFormData] = useState({
    name: user?.Name || '',
    email: user?.Email || '',
    phone: user?.Phone || ''
  });

  useEffect(() => {
    const fetchUserData = async () => {
      try {
        if (user?.email) {
          setFormData({
            name: user.Name || '',
            email: user.Email || '',
            phone: user.Phone || ''
          });
        }
      } catch (error) {
        toast.error('Failed to load user data');
      } finally {
        setIsLoading(false);
      }
    };

    fetchUserData();
  }, [user]);

  if (!isAuthenticated) {
    return <Navigate to="/login" replace />;
  }

  const handleInputChange = (e) => {
    setFormData({
      ...formData,
      [e.target.name]: e.target.value
    });
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    try {
      const response = await UserService.updateUser({
        email: formData.email,
        name: formData.name,
        phone: formData.phone,
        password: user.password
      });

      if (response.status === 'success') {
        setUser({
          ...user,
          Name: formData.name,
          Email: formData.email,
          Phone: formData.phone
        });
        setIsEditing(false);
        toast.success('Profile updated successfully!');
      }
    } catch (error) {
      toast.error(error.message || 'Failed to update profile');
    }
  };

  const handleDiscard = () => {
    setFormData({
      name: user?.Name || '',
      email: user?.Email || '',
      phone: user?.Phone || ''
    });
    setIsEditing(false);
    toast.info('Changes discarded');
  };

  if (isLoading) {
    return (
      <div className="profile-container">
        <Sidebar />
        <div className="profile-content">
          <div className="profile-card loading">
            <div className="loading-animation">
              <div className="loading-spinner" />
              <span className="loading-text">Loading profile...</span>
            </div>
            
            {/* Skeleton Loading UI */}
            <div className="profile-header">
              <div className="profile-avatar skeleton" />
            </div>

            <div className="profile-form">
              <div className="form-group skeleton">
                <input disabled />
              </div>
              <div className="form-group skeleton">
                <input disabled />
              </div>
              <div className="form-group skeleton">
                <input disabled />
              </div>
            </div>

            <div className="profile-stats">
              <div className="stat-item skeleton" />
              <div className="stat-item skeleton" />
              <div className="stat-item skeleton" />
            </div>
          </div>
        </div>
      </div>
    );
  }

  return (
    <div className="profile-container">
      <Sidebar />
      <ToastContainer />
      <div className="profile-content">
        <div className="profile-card content-loaded">
          <div className="profile-header">
            <div className="profile-info">
              <div className="profile-avatar">
                <PersonIcon style={{ fontSize: 60 }} />
              </div>
              <h2 className="welcome-message">Welcome! {user?.Name || 'User'}</h2>
            </div>
            <div className="header-actions">
              {isEditing && (
                <button 
                  className="edit-button discard-button"
                  onClick={handleDiscard}
                  title="Discard changes"
                >
                  <CloseIcon />
                </button>
              )}
              <button 
                className="edit-button"
                onClick={() => setIsEditing(!isEditing)}
                title={isEditing ? "Save" : "Edit"}
              >
                {isEditing ? <SaveIcon /> : <EditIcon />}
              </button>
            </div>
          </div>

          <form onSubmit={handleSubmit} className={`profile-form ${isEditing ? 'editing' : ''}`}>
            <div className="form-group">
              <label>Name</label>
              <input
                type="text"
                name="name"
                value={formData.name}
                onChange={handleInputChange}
                disabled={!isEditing}
              />
            </div>

            <div className="form-group">
              <label>Email</label>
              <input
                type="email"
                name="email"
                value={formData.email}
                onChange={handleInputChange}
                disabled={!isEditing}
              />
            </div>

            <div className="form-group">
              <label>Phone</label>
              <input
                type="tel"
                name="phone"
                value={formData.phone}
                onChange={handleInputChange}
                disabled={!isEditing}
              />
            </div>

            <div className="form-actions">
              {isEditing && (
                <>
                  <button type="button" className="discard-button-large" onClick={handleDiscard}>
                    Discard Changes
                  </button>
                  <button type="submit" className="save-button">
                    Save Changes
                  </button>
                </>
              )}
            </div>
          </form>

          <div className="profile-stats">
            <div className="stat-item">
              <h3>Teams</h3>
              <p>5</p>
            </div>
            <div className="stat-item">
              <h3>Tournaments</h3>
              <p>3</p>
            </div>
            <div className="stat-item">
              <h3>Matches</h3>
              <p>24</p>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
};

export default Profile;
