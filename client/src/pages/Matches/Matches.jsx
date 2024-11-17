import React, { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import { Link } from 'react-router-dom';
import './Matches.css';
import MatchService from '../../api/services/MatchService';

function Matches() {
  const [search, setSearch] = useState(''); // Estado da busca
  const [matches, setMatches] = useState([]); // Estado das partidas

  const navigate = useNavigate();

  // Busca as partidas na montagem do componente
  useEffect(() => {
    const fetchMatches = async () => {
      try {
        const matchData = await MatchService.getMatches();
        setMatches(matchData);
      } catch (error) {
        console.error('Error fetching matches:', error);
      }
    };

    fetchMatches();
  }, []);

  // Função para excluir uma partida
  const deleteMatch = async (id) => {
    try {
      await MatchService.deleteMatch(id);
      const updatedMatches = await MatchService.getMatches();
      setMatches(updatedMatches);
    } catch (error) {
      console.error('Error deleting match:', error);
    }
  };

  // Atualiza o valor do campo de busca
  const handleSearchChange = (e) => {
    setSearch(e.target.value);
  };

  // Filtra as partidas com base na busca
  const filteredMatches = matches.filter((match) =>
    match.idTeam1.toString().includes(search) || match.idTeam2.toString().includes(search)
  );

  // Redireciona para a página de edição de partida
  const handleEdit = (idMatch) => {
    navigate(`/edit-match/${idMatch}`);
  };

  // Redireciona para a página de lista de partidas
  const handleBack = () => {
    navigate('/matches');
  };

  return (
    <div className="match-form">
      <h2>Matches</h2>

      <div className="input-group">
        <input
          type="text"
          placeholder="Search by team ID:"
          value={search}
          onChange={handleSearchChange}
        />
      </div>

      <div className="matches-table">
        <table>
          <thead>
            <tr>
              <th>Teams</th>
              <th>Result</th>
              <th>Actions</th>
            </tr>
          </thead>
          <tbody>
            {filteredMatches.map((match) => (
              <tr key={match.idMatch}>
                <td>{`Team ${match.idTeam1} X Team ${match.idTeam2}`}</td>
                <td>{match.result}</td>
                <td className="actions">
                  <button
                    className="edit-button"
                    onClick={() => handleEdit(match.idMatch)}
                  >
                    Edit
                  </button>
                  <button
                    className="delete-button"
                    onClick={() => deleteMatch(match.idMatch)}
                  >
                    Delete
                  </button>
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>

      <div className="button-group">
        <Link to="/register-match">
          <button className="new-button">New</button>
        </Link>
        <button className="new-button" onClick={handleBack}>Back</button>
      </div>
    </div>
  );
}

export default Matches;
