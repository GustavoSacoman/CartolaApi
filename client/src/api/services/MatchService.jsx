import apiClient from '../config';

const MatchService = {
  getMatches: async () => {
    try {
      const response = await apiClient.get('/v1/match');
      if (response.data && response.data.data) {
        return response.data.data;  // Retorna os dados se existirem
      } else {
        throw new Error('Estrutura de dados inesperada.');
      }
    } catch (error) {
      console.error('Erro ao buscar todas as partidas:', error);
      throw error.response?.data || error.message;
    }
  },

  getMatchById: async (id) => {
    try {
      const response = await apiClient.get(`/v1/match/${id}`);
      if (response.data && response.data.data) {
        return response.data.data;  // Retorna os dados da partida
      } else {
        throw new Error('Estrutura de dados inesperada.');
      }
    } catch (error) {
      console.error(`Erro ao buscar partida com ID ${id}:`, error);
      throw error.response?.data || error.message;
    }
  },

  addMatch: async (matchData) => {
    try {
      const response = await apiClient.post('/v1/match', matchData);
      return response.data;  // Retorna a resposta completa após a criação
    } catch (error) {
      console.error('Erro ao adicionar nova partida:', error);
      throw error.response?.data || error.message;
    }
  },

  updateMatch: async (id, matchData) => {
    try {
      const response = await apiClient.put(`/v1/match/${id}`, matchData);
      return response.data;  // Retorna a resposta completa após a atualização
    } catch (error) {
      console.error(`Erro ao atualizar partida com ID ${id}:`, error);
      throw error.response?.data || error.message;
    }
  },

  deleteMatch: async (idMatch) => {
    try {
      const response = await apiClient.delete(`/v1/match/${idMatch}`);
      return response.data;  // Retorna a resposta completa após a exclusão
    } catch (error) {
      console.error(`Erro ao excluir partida com ID ${idMatch}:`, error);
      throw error.response?.data || error.message;
    }
  },
};

export default MatchService;

