import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import './Matches.css';
import MatchService from '../../api/services/MatchService';
import { ToastContainer, toast } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';

const CadastroMatches = () => {
  const [formData, setFormData] = useState({
    team1: '',
    team2: '',
    date: '',
    tournament: '',
    result: '',
  });

  const navigate = useNavigate();

  const handleChange = (e) => {
    setFormData({ ...formData, [e.target.name]: e.target.value });
  };

  const showError = (message) => {
    toast.error(message, {
      position: "top-right",
      autoClose: 3000,
      hideProgressBar: false,
      closeOnClick: true,
      pauseOnHover: false,
      draggable: true,
      progress: undefined,
    });
  };

  const showSuccess = (message) => {
    toast.success(message, {
      position: "top-right",
      autoClose: 3000,
      hideProgressBar: false,
      closeOnClick: true,
      pauseOnHover: false,
      draggable: true,
      progress: undefined,
    });
  };

  const handleSubmit = async (e) => {
    e.preventDefault();

    const matchData = {
      date: formData.date,
      result: formData.result,
      idTeam1: parseInt(formData.team1, 10),
      idTeam2: parseInt(formData.team2, 10),
      idTournament: parseInt(formData.tournament, 10),
    };

    try {
      const response = await MatchService.addMatch(matchData);
      console.log('Partida cadastrada com sucesso:', response);
      showSuccess('Partida cadastrada com sucesso!');
      setFormData({
        team1: '',
        team2: '',
        date: '',
        tournament: '',
        result: '',
      });
      navigate('/matches');
    } catch (error) {
      console.error('Erro ao cadastrar partida:', error);
      showError('Erro ao cadastrar partida. Verifique os dados e tente novamente.');
    }
  };

  const handleBack = () => {
    navigate('/matches');
  };

  return (
    <div className="match-form">
      <ToastContainer />
      <h2>Cadastro de Partida</h2>
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