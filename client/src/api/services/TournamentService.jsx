import apiClient from '../config'; 

export const TournamentService = {

  getTournaments: async () => {
    try {
      const response = await apiClient.get('/v1/Tournament');
      return response.data;
    } catch (error) {
      throw error.response?.data || error.message;
    }
  },

 
  addTournament: async (tournamentData) => {
    try {
        const response = await apiClient.post('/v1/Tournament/create-tournament', tournamentData);
        console.log("Tournament created response:", response.data); 
        return response.data;
    } catch (error) {
        console.error("Error creating tournament:", error);
        throw error; 
    }
},


  
  updateTournament: async (tournamentData) => {
    try {
      const response = await apiClient.put(`/v1/Tournament/update-tournament?tournamentId=${tournamentData.id}&newTournamentName=${tournamentData.name}`);
      return response.data;
    } catch (error) {
      console.error("Error during update:", error);
      throw error.response?.data || error.message;
    }
  },

 
  deleteTournament: async (id) => {
    try {
      console.log(`Sending DELETE request for tournament with id: ${id}`);
      const response = await apiClient.delete(`/v1/Tournament/delete-tournament`, {
        params: { tournamentId: id }  // Passando o ID como parâmetro de consulta
      });
      return response.data;
    } catch (error) {
      console.error("Error during DELETE request:", error);
      throw error.response?.data || error.message;
    }
  }
};

export default TournamentService;
