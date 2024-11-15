import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom'
import './Matches.css';

const CadastroMatches = () => {
  const [formData, setFormData] = useState({
    team1: '',
    team2: '',
    date: '',
    tournament: '',
    result: '',
    id: '',
  });

  const handleChange = (e) => {
    setFormData({ ...formData, [e.target.name]: e.target.value });
  };

  const handleSubmit = (e) => {
    e.preventDefault();
    console.log('Cadastro:', formData);
  };
  const navigate = useNavigate();
  const handleBack = () => {
    navigate('/matches'); 
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
            />
          </div>
          <div>
            <label>Team 2</label>
            <input
              type="text"
              name="team2"
              value={formData.team2}
              onChange={handleChange}
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
            />
          </div>
          <div>
            <label>Tournament</label>
            <input
              type="text"
              name="tournament"
              value={formData.tournament}
              onChange={handleChange}
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
            />
          </div>
        </div>
        <div className="button-group">
          <button type="submit">Cadastrar</button>
          <button className="new-button" onClick={handleBack}>Voltar</button>
        </div>
      </form>
    </div>
  );
};

export default CadastroMatches;
