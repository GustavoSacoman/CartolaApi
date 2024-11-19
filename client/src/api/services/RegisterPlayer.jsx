import apiClient from "../config";

export const RegisterPlayerService = { 
    getPlayers: async () => {
        try {
            const response = await apiClient.get('/v1/player');
            if (response.data && response.data.data) {
                return response.data;
            } else {
                throw new Error('Estrutura de dados inesperada.');
            }
        } catch (error) {
            console.error('Erro ao buscar todos os jogadores:', error);
            throw error.response?.data || error.message;
        }
    },
    getPlayersName: async (id) => {
        try {
            const response = await apiClient.get(`/v1/player/`);
            if (response.data && response.data.data) {
                const player = response.data?.data;
                console.log(player);
                if (!Array.isArray(player)){
                    throw new Error('Estrutura de dados inesperada.');
                }
                const filterteredPlayers = player.filter((player) => player.namePlayer.toLowerCase().includes(id.toLowerCase()));
                
                if(filterteredPlayers === undefined || filterteredPlayers.length == 0){
                    throw new Error('Jogador não encontrado');
                }
                return filterteredPlayers;
              
            } else {
                throw new Error('Estrutura de dados inesperada.');
            }
        } catch (error) {
            console.error('Erro ao buscar todos os jogadores:', error);
            throw error.response?.data || error.message;
        }
    },
    addPlayer: async (playerData) => {
        try {
            console.log('Enviando dados:', playerData);
            const response = await apiClient.post('/v1/player/create-player', playerData);
            return response.data;
        } catch (error) {
            console.error('Erro ao adicionar jogador:', error);
            throw error.response?.data || error.message;
        }
    },
    updatePlayer: async (id, playerData) => {
        try {
            console.log('Enviando dados:', playerData);
            const response = await apiClient.put(`/v1/player/update-player/${id}`, playerData);
            return response.data;
        } catch (error) {
            console.error('Erro ao editar jogador:', error);
            throw error.response?.data || error.message;
        }
    },

    deletePlayer: async (id) => {
        try {
            console.log("ver id: ", id);
            const response = await apiClient.delete(`/v1/player/delete-player/${id}`);
            return response.data;
        } catch (error) {
            console.error('Erro ao deletar jogador:', error);
            throw error.response?.data || error.message;
        }
    },
};
    export default RegisterPlayerService;
    
