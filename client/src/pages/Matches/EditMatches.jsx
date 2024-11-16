import React, { useState, useEffect } from 'react';
import { useNavigate, useParams } from 'react-router-dom';
import axios from 'axios';
import './Matches.css';

const EditMatches = () => {
  const { id } = useParams(); // Obtém o ID da partida da URL
  const navigate = useNavigate();

  const [formData, setFormData] = useState({
    team1: '',
    team2: '',
    date: '',
    tournament: '',
    result: '',
  });

  // Carrega os dados da partida para edição
  useEffect(() => {
    const fetchMatch = async () => {
      try {
        const response = await axios.get(`http://localhost:5210/v1/api/v1/match`);
        
        // Filtra a partida com o ID correspondente
        const match = response.data.data.find((match) => match.idMatch === parseInt(id));
        
        if (match) {
          setFormData({
            team1: match.idTeam1.toString(),
            team2: match.idTeam2.toString(),
            date: match.date,
            tournament: match.idTournament.toString(),
            result: match.result,
          });
        } else {
          alert('Partida não encontrada.');
          navigate('/matches');
        }
      } catch (error) {
        console.error('Erro ao carregar os dados da partida:', error.response || error);
        alert('Erro ao carregar os dados da partida. Tente novamente mais tarde.');
        navigate('/matches'); // Redireciona para a lista de partidas
      }
    };

    fetchMatch();
  }, [id, navigate]);

  const handleChange = (e) => {
    setFormData({ ...formData, [e.target.name]: e.target.value });
  };

  const handleSubmit = async (e) => {
    e.preventDefault();

    const updatedMatch = {
      date: formData.date,
      result: formData.result,
      idTeam1: parseInt(formData.team1, 10),
      idTeam2: parseInt(formData.team2, 10),
      idTournament: parseInt(formData.tournament, 10),
    };

    try {
      await axios.put(`http://localhost:5210/v1/api/v1/match/${id}`, updatedMatch);
      alert('Partida atualizada com sucesso!');
      navigate('/matches'); // Redireciona para a lista de partidas
    } catch (error) {
      console.error('Erro ao atualizar a partida:', error.response || error);
      alert('Erro ao atualizar a partida. Verifique os dados e tente novamente.');
    }
  };

  const handleBack = () => {
    navigate('/matches'); // Volta para a lista de partidas
  };

  return (
    <div className="match-form">
      <h2>Editar Partida</h2>
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
          <button type="submit">Salvar Alterações</button>
          <button type="button" className="new-button" onClick={handleBack}>Voltar</button>
        </div>
      </form>
    </div>
  );
};

export default EditMatches;
