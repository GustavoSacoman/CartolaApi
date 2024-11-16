import React, { useState } from 'react';
import { useNavigate, useParams } from 'react-router-dom';
import MatchService from '../../api/services/MatchService';
import './Matches.css';

const EditMatches = () => {
  const { id } = useParams(); // Usar o hook useParams para pegar o ID da URL
  const navigate = useNavigate();

  const [formData, setFormData] = useState({
    team1: '',
    team2: '',
    date: '',
    tournament: '',
    result: '',
  });

  const [loading, setLoading] = useState(false); // Estado de carregamento
  const [message, setMessage] = useState(''); // Mensagem de feedback
  const [messageType, setMessageType] = useState(''); // Tipo de mensagem (erro ou sucesso)

  // Função para atualizar os dados do formulário
  const handleChange = (e) => {
    setFormData({ ...formData, [e.target.name]: e.target.value });
  };

  // Função para submeter o formulário de edição
  const handleSubmit = async (e) => {
    e.preventDefault();
    const updatedMatch = {
      date: formData.date,
      result: formData.result,
      idTeam1: parseInt(formData.team1, 10),
      idTeam2: parseInt(formData.team2, 10),
      idTournament: parseInt(formData.tournament, 10),
    };

    // Verifica se o ID da partida está presente
    if (!id) {
      setMessage('ID da partida não encontrado.');
      setMessageType('error');
      return;
    }

    setLoading(true); // Começa o carregamento durante a atualização

    try {
      // Chama o serviço para atualizar a partida
      await MatchService.updateMatch(id, updatedMatch); // Passar o id obtido da URL
      setMessage('Partida atualizada com sucesso!');
      setMessageType('success');
      navigate('/matches');
    } catch (error) {
      console.error('Erro ao atualizar a partida:', error);
      setMessage('Erro ao atualizar a partida');
      setMessageType('error');
    } finally {
      setLoading(false); // Finaliza o carregamento
    }
  };

  // Função para voltar para a lista de partidas
  const handleBack = () => {
    navigate('/matches');
  };

  return (
    <div className="match-form">
      <h2>Edit Match</h2>

      {message && (
        <div className={`message ${messageType}`}>
          {message}
        </div>
      )}

      <form onSubmit={handleSubmit}>
        <div className="input-group">
          <div>
            <label>Team 1</label>
            <input
              type="text"
              name="team1"
              value={formData.team1}
              onChange={handleChange}
              placeholder="Team 1 ID"
              required
            />
          </div>
          <div>
            <label>Team 2</label>
            <input
              type="text"
              name="team2"
              value={formData.team2}
              onChange={handleChange}
              placeholder="Team 2 ID"
              required
            />
          </div>
        </div>

        <div className="input-group">
          <div>
            <label>Date</label>
            <input
              type="date"
              name="date"
              value={formData.date}
              onChange={handleChange}
              required
            />
          </div>
          <div>
            <label>Tournament</label>
            <input
              type="text"
              name="tournament"
              value={formData.tournament}
              onChange={handleChange}
              placeholder="Tournament ID"
              required
            />
          </div>
        </div>

        <div className="input-group">
          <div>
            <label>Result</label>
            <input
              type="text"
              name="result"
              value={formData.result}
              onChange={handleChange}
              placeholder="Result"
              required
            />
          </div>
        </div>

        <div className="button-group">
          <button type="submit" disabled={loading}>Save Changes</button>
          <button type="button" onClick={handleBack} disabled={loading}>Back</button>
        </div>
      </form>
    </div>
  );
};

export default EditMatches;
