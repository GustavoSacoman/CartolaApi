import React, { useState, useEffect } from 'react';
import { useNavigate, useParams } from 'react-router-dom';
import MatchService from '../../api/services/MatchService';
import './Match.css';
import Sidebar from '../../components/Sidebar/Sidebar';

const EditMatch = () => {
  const { id } = useParams();
  const navigate = useNavigate();

  const [formData, setFormData] = useState({
    team1: '',
    team2: '',
    date: '',
    tournament: '',
    result: '',
  });

  const [loading, setLoading] = useState(false);
  const [message, setMessage] = useState('');
  const [messageType, setMessageType] = useState('');

  useEffect(() => {
    const fetchMatchData = async () => {
      const localMatch = JSON.parse(localStorage.getItem('selectedMatch'));

      if (localMatch && localMatch.idMatch === parseInt(id, 10)) {
        setFormData({
          team1: localMatch.idTeam1.toString(),
          team2: localMatch.idTeam2.toString(),
          date: localMatch.date,
          tournament: localMatch.idTournament?.toString() || '',
          result: localMatch.result,
        });
      } else {
        try {
          const matchData = await MatchService.getMatchById(id);
          if (matchData) {
            setFormData({
              team1: matchData.idTeam1.toString(),
              team2: matchData.idTeam2.toString(),
              date: matchData.date,
              tournament: matchData.idTournament?.toString() || '',
              result: matchData.result,
            });
          }
        } catch (error) {
          console.error('Erro ao carregar dados da partida:', error);
          setMessage('Erro ao carregar os dados da partida');
          setMessageType('error');
        }
      }
    };

    fetchMatchData();
  }, [id]);

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

    setLoading(true);
    try {
      await MatchService.updateMatch(id, updatedMatch);
      setMessage('Partida atualizada com sucesso!');
      setMessageType('success');
      navigate('/list-match');
    } catch (error) {
      console.error('Erro ao atualizar a partida:', error);
      setMessage('Erro ao atualizar a partida');
      setMessageType('error');
    } finally {
      setLoading(false);
    }
  };

  const handleBack = () => {
    navigate('/list-match');
  };

  return (
    <div>
      <Sidebar />
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
    </div>
  );
};

export default EditMatch;
