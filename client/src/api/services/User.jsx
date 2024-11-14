import apiClient from '../config';

export const UserService = {
  getUsers: async () => {
    try {
      const response = await apiClient.get('/v1/User/get-users');
      return response.data;
    } catch (error) {
      throw error.response?.data || error.message;
    }
  },

  addUser: async (userData) => {
    try {
      const response = await apiClient.post('/v1/User/add-user', userData);
      return response.data;
    } catch (error) {
      throw error.response?.data || error.message;
    }
  },

  updateUser: async (userData) => {
    try {
      const response = await apiClient.put('/v1/User/update-user', userData);
      return response.data;
    } catch (error) {
      throw error.response?.data || error.message;
    }
  },

  deleteUser: async (email) => {
    try {
      const response = await apiClient.delete(`/v1/User/delete-user`, {
        params: { email }
      });
      return response.data;
    } catch (error) {
      throw error.response?.data || error.message;
    }
  },

  login: async (credentials) => {
    try {
      const response = await apiClient.post('/v1/User/login', credentials);
      if (response.data?.data?.token) {
        localStorage.setItem('token', response.data.data.token);
      }
      return response.data;
    } catch (error) {
      throw error.response?.data || error.message;
    }
  }
};

export default UserService;
