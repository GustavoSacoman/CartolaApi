import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { Link } from 'react-router-dom';
import './Matches.css';

function Matches() {
  const [search, setSearch] = useState('');
  const matches = [
    { id: 1, teams: 'Team 1 X Team 3' },
    { id: 2, teams: 'Team 2 X Team 1' },
    { id: 3, teams: 'Team 3 X Team 1' },
  ];

  const handleSearchChange = (e) => {
    setSearch(e.target.value);
  };

  const filteredMatches = matches.filter(match =>
    match.teams.toLowerCase().includes(search.toLowerCase())
  );
  const navigate = useNavigate();
  const handleBack = () => {
    navigate('/matches'); // Redireciona para a página de lista de matches
};

  return (
    <div className="match-form">
      <h2>Matches</h2>
      
      {/* Campo de busca */}
      <div className="input-group">
        <input
          type="text"
          placeholder="Search by teams:"
          value={search}
          onChange={handleSearchChange}
        />
      </div>

      {/* Lista de partidas */}
      <div className="matches-table">
        <table>
          <thead>
            <tr>
              <th>Teams</th>
              <th>Actions</th>
            </tr>
          </thead>
          <tbody>
            {filteredMatches.map(match => (
              <tr key={match.id}>
                <td>{match.teams}</td>
                <td className="actions">
                    <button className="edit-button" >Edit</button>
                    <button class Name="delete-button" >Delete </button>
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>

      {/* Botões de ação */}
      <div className="button-group">

      <Link to="/CadastroMatches">
          <button className="new-button">New</button>
        </Link>
        
        <button className="new-button" onClick={handleBack}>Voltar</button>
      </div>
    </div>
  );
}

export default Matches;
