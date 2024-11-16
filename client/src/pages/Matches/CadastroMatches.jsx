import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import './Matches.css';
import axios from 'axios';

const CadastroMatches = () => {
  const [formData, setFormData] = useState({
    team1: '',
    team2: '',
    date: '',
    tournament: '',
    result: '',
  });

  const navigate = useNavigate();

  // Atualiza o estado conforme o usuário insere os dados
  const handleChange = (e) => {
    setFormData({ ...formData, [e.target.name]: e.target.value });
  };

  // Submete os dados do formulário
  const handleSubmit = async (e) => {
    e.preventDefault();

    // Estrutura os dados no formato esperado pela API
    const matchData = {
      date: formData.date,
      result: formData.result,
      idTeam1: parseInt(formData.team1, 10),
      idTeam2: parseInt(formData.team2, 10),
      idTournament: parseInt(formData.tournament, 10),
    };

    try {
      const response = await axios.post('http://localhost:5210/v1/api/v1/match', matchData);
      console.log('Partida cadastrada com sucesso:', response.data);
      alert('Partida cadastrada com sucesso!');
      // Limpa o formulário
      setFormData({
        team1: '',
        team2: '',
        date: '',
        tournament: '',
        result: '',
      });
      // Redireciona para a lista de partidas
      navigate('/matches');
    } catch (error) {
      console.error('Erro ao cadastrar partida:', error.response || error);
      alert('Erro ao cadastrar partida. Verifique os dados e tente novamente.');
    }
  };

  const handleBack = () => {
    navigate('/matches'); // Volta para a página de lista de partidas
  };

  return (
    <div className="match-form">
      <h2>Cadastro Matches</h2>
      <form onSubmit={handleSubmit}>
        <div className="input-group">
          <div>
            <label>Team 1</label>
            <input
              type="text"
              name="team1"
              value={formData.team1}
              onChange={handleChange}
              placeholder="ID do Time 1"
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
              placeholder="ID do Time 2"
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
              placeholder="ID do Torneio"
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
              placeholder="Resultado"
              required
            />
          </div>
        </div>

        <div className="button-group">
          <button type="submit">Cadastrar</button>
          <button type="button" className="new-button" onClick={handleBack}>Voltar</button>
        </div>
      </form>
    </div>
  );
};

export default CadastroMatches;
