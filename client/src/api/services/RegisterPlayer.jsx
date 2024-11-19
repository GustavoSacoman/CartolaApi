import apiClient from "../config";

export const RegisterPlayerService = { 
    getPlayers: async () => {
        try {
            const response = await apiClient.get('/v1/player');
            if (response.data && response.data.data) {
                return response.data;
            } else {
                throw new Error('Unexpected data structure');
            }
        } catch (error) {
            console.error('Error while retrieving all players', error);
            throw error.response?.data || error.message;
        }
    },
    getPlayersName: async (id) => {
        try {
            const response = await apiClient.get(`/v1/player/`);
            if (response.data && response.data.data) {
                const player = response.data?.data;
                if (!Array.isArray(player)){
                    throw new Error('Unexpected data structure');
                }
                const filterteredPlayers = player.filter((player) => player.namePlayer.toLowerCase().includes(id.toLowerCase()));
                
                if(filterteredPlayers === undefined || filterteredPlayers.length == 0){
                    throw new Error('Player not found');
                }
                return filterteredPlayers;
              
            } else {
                throw new Error('Unexpected data structure');
            }
        } catch (error) {
            console.error('Error while retrieving all players', error);
            throw error.response?.data || error.message;
        }
    },
    addPlayer: async (playerData) => {
        try {
            const response = await apiClient.post('/v1/player/create-player', playerData);
            return response.data;
        } catch (error) {
            console.error('Error while adding player', error);
            throw error.response?.data || error.message;
        }
    },
    updatePlayer: async (id, playerData) => {
        try {
            const response = await apiClient.put(`/v1/player/update-player/${id}`, playerData);
            return response.data;
        } catch (error) {
            console.error('Error while editing player', error);
            throw error.response?.data || error.message;
        }
    },

    deletePlayer: async (id) => {
        try {
            const response = await apiClient.delete(`/v1/player/delete-player/${id}`);
            return response.data;
        } catch (error) {
            console.error('Error while deleting player', error);
            throw error.response?.data || error.message;
        }
    },
};
    export default RegisterPlayerService;
    
